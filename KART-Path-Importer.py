import bpy
import json
from mathutils import Vector, Matrix

bl_info = {
    "name": "KART Path Importer from Clipboard",
    "blender": (2, 80, 0),
    "category": "Import-Export",
    "description": "Import KART Path from clipboard to create a curve",
    "author": "Eiko",
    "version": (1, 6),
    "location": "File > Import > KART Path from Clipboard"
}

class ImportKARTPathFromClipboard(bpy.types.Operator):
    """Import KART Path from clipboard"""
    bl_idname = "import_scene.kart_path_clipboard"
    bl_label = "Import KART Path from Clipboard"
    bl_options = {'REGISTER', 'UNDO'}

    def execute(self, context):
        try:
            json_data = bpy.context.window_manager.clipboard.strip()
            if not json_data:
                raise ValueError("Clipboard is empty or does not contain valid JSON data.")
            
            data = json.loads(json_data)
            self.create_curve_from_data(context, data)
            self.create_keyframes_from_drivedata(context, data)
            return {'FINISHED'}
        except json.JSONDecodeError:
            self.report({'ERROR'}, "Invalid JSON format in clipboard.")
            return {'CANCELLED'}
        except Exception as e:
            self.report({'ERROR'}, f"Failed to import KART Path from clipboard: {e}")
            return {'CANCELLED'}

    def create_curve_from_data(self, context, data):
        if "path" not in data or not isinstance(data["path"], list):
            self.report({'ERROR'}, "JSON data does not contain a valid 'path' array.")
            return {'CANCELLED'}

        curve_data = bpy.data.curves.new(name="KARTPathCurve", type='CURVE')
        curve_data.dimensions = '3D'
        curve_data.twist_mode = 'MINIMUM'
        curve_data.use_radius = True
        curve_data.use_stretch = True
        curve_data.use_deform_bounds = True

        path_points = data["path"]
        current_spline = None
        segment_points = []

        for i, point_data in enumerate(path_points):
            position = point_data["position"]
            connect_to_previous = point_data.get("connectToPrevious", True)

            if not all(k in position for k in ("x", "y", "z")):
                continue

            # If a new segment starts or this is the first point, set up a new spline
            if current_spline is None or not connect_to_previous:
                if segment_points:
                    # Set points for the previous segment
                    self.add_points_to_spline(current_spline, segment_points)
                    segment_points = []

                current_spline = curve_data.splines.new(type='POLY')
                current_spline.use_cyclic_u = False

            # Append this point to the current segment
            blender_position = (-position["x"], -position["z"], position["y"])
            segment_points.append(blender_position)

        # Add remaining points to the last segment if any
        if segment_points:
            self.add_points_to_spline(current_spline, segment_points)

        # Link the curve object in the scene
        curve_obj = bpy.data.objects.new("KARTPathCurveObj", curve_data)
        context.collection.objects.link(curve_obj)
        context.view_layer.objects.active = curve_obj
        curve_obj.select_set(True)

        self.report({'INFO'}, "KART Path imported successfully from clipboard.")

    def add_points_to_spline(self, spline, points):
        spline.points.add(len(points) - 1)  # Add points minus the one already present
        for i, position in enumerate(points):
            spline.points[i].co = (*position, 1)

    def create_keyframes_from_drivedata(self, context, data):
        if "data" not in data or not isinstance(data["data"], list):
            self.report({'WARNING'}, "JSON data does not contain a valid 'data' array for DriveData.")
            return

        # Create an empty object to represent the kart for keyframing
        kart_obj = bpy.data.objects.new("KARTDrivePathObj", None)
        context.collection.objects.link(kart_obj)

        # Loop through each frame in DriveData and create keyframes
        for frame_index, drive_data in enumerate(data["data"]):
            position = drive_data["position"]
            forward = drive_data["forward"]
            upward = drive_data["upward"]

            # Set position
            kart_obj.location = (-position["x"], -position["z"], position["y"])

            # Create rotation matrix from forward and upward vectors
            forward_vec = Vector((-forward["x"], -forward["z"], forward["y"]))
            upward_vec = Vector((-upward["x"], -upward["z"], upward["y"]))
            right_vec = forward_vec.cross(upward_vec)

            # Construct orientation matrix and assign it to the kart
            rot_matrix = Matrix((right_vec, forward_vec, upward_vec)).transposed()
            kart_obj.matrix_world = Matrix.Translation(kart_obj.location) @ rot_matrix.to_4x4()

            # Insert keyframes for location and rotation
            kart_obj.keyframe_insert(data_path="location", frame=frame_index + 1)
            kart_obj.keyframe_insert(data_path="rotation_euler", frame=frame_index + 1)

        self.report({'INFO'}, "DriveData keyframes added successfully.")

def menu_func_import(self, context):
    self.layout.operator(ImportKARTPathFromClipboard.bl_idname, text="KART Path from Clipboard")

def register():
    bpy.utils.register_class(ImportKARTPathFromClipboard)
    bpy.types.TOPBAR_MT_file_import.append(menu_func_import)

def unregister():
    bpy.utils.unregister_class(ImportKARTPathFromClipboard)
    bpy.types.TOPBAR_MT_file_import.remove(menu_func_import)

if __name__ == "__main__":
    register()
