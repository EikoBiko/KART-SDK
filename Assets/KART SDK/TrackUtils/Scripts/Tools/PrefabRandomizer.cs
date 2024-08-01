using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PrefabRandomizer : SceneElement
{
    public override string Info()
    {
        return "This component is a tool used to generate random prefabs from a list. If prefabs are not spawned with the button below, they will be spawned when the track is loaded.";
    }
    [Tooltip("The list of potentially spawned prefabs.")]
    public List<GameObject> prefabs = new List<GameObject>();
    public enum Placement{
        CurrentPosition,
        ScaleIsArea,
        AlongMesh
    }
    public enum Rotation{
        DoNotRotate,
        MatchPlacementSource,
        Randomize,
        RandomizeX,
        RandomizeY,
        RandomizeZ
    }
    [Tooltip("CurrentPosition spawns objects at this GameObject's position. ScaleIsArea uses this GameObject's \"scale\" parameter as the spawn area. AlongMesh spawns the objects on a random point along a provided Mesh (using a MeshRenderer component).")]
    public Placement placementPoint;
    [Tooltip("DoNotRotate spawns with (0,0,0) rotation. MatchPlacementSource matches the target placement, in the case of a Mesh, it will match the surface of the mesh. The Randomize options should be self-explanatory.")]
    public Rotation rotation;
    public MeshFilter meshFilter;
    public int numberToPlace = 1;
    public List<GameObject> generatedObjects;
    public void CreateRandom(){
        if(generatedObjects.Count > 0){
            foreach(GameObject thing in generatedObjects){
                DestroyImmediate(thing);
            }
        }
        generatedObjects.Clear();
        Vector3 placementPosition = Vector3.zero;
        Vector3 placementRotation = Vector3.zero;
        for (int i = 0; i < numberToPlace; i++){
            if(placementPoint == Placement.CurrentPosition){
                placementPosition = transform.position;
            }else if(placementPoint == Placement.ScaleIsArea){
                placementPosition = GetRandomScalePosition();
            }else{
                if(meshFilter != null){
                    (placementPosition, placementRotation) = GetRandomPointAndNormalOnMesh(meshFilter);
                }
            }
            float rotationX = 0;
            float rotationY = 0;
            float rotationZ = 0;
            if(rotation != Rotation.MatchPlacementSource && rotation != Rotation.DoNotRotate){
                if(rotation == Rotation.RandomizeX || rotation == Rotation.Randomize){
                    rotationX = Random.Range(0f, 360f);
                }
                if(rotation == Rotation.RandomizeY || rotation == Rotation.Randomize){
                    rotationY = Random.Range(0f, 360f);
                }
                if(rotation == Rotation.RandomizeZ || rotation == Rotation.Randomize){
                    rotationZ = Random.Range(0f, 360f);
                }
            }else if(rotation == Rotation.MatchPlacementSource){
                if(placementPoint == Placement.ScaleIsArea || placementPoint == Placement.CurrentPosition){
                    rotationX = transform.rotation.x;
                    rotationY = transform.rotation.y;
                    rotationZ = transform.rotation.z;
                }
            }
            if(rotation == Rotation.MatchPlacementSource && placementPoint == Placement.AlongMesh){

                // Get an arbitrary perpendicular vector to represent "forward". This actually makes them all sorta look upward, so that's nice.
                Vector3 arbitraryVector = Vector3.up;
                if (Vector3.Dot(placementRotation, arbitraryVector) > 0.99f)
                {
                    arbitraryVector = Vector3.forward;
                }
                Vector3 forward = Vector3.Cross(placementRotation, arbitraryVector).normalized;

                // Make it!
                generatedObjects.Add(Instantiate(prefabs[Random.Range(0, prefabs.Count)], placementPosition, Quaternion.LookRotation(forward, placementRotation)));
            }else{
                placementRotation = new Vector3(rotationX, rotationY, rotationZ);
                generatedObjects.Add(Instantiate(prefabs[Random.Range(0, prefabs.Count)], placementPosition, Quaternion.Euler(placementRotation)));
            }
        }
    }
    public void DeleteObjects(){
        if(generatedObjects.Count > 0){
            foreach(GameObject thing in generatedObjects){
                DestroyImmediate(thing);
            }
        }
        generatedObjects.Clear();
    }

    public Vector3 GetRandomScalePosition()
    {
        Vector3 scale = transform.localScale;
        float randomX = Random.Range(-scale.x / 2, scale.x / 2);
        float randomY = Random.Range(-scale.y / 2, scale.y / 2);
        float randomZ = Random.Range(-scale.z / 2, scale.z / 2);
        return transform.position + new Vector3(randomX, randomY, randomZ);
    }

    public static (Vector3 position, Vector3 normal) GetRandomPointAndNormalOnMesh(MeshFilter meshFilter)
    {
        Mesh mesh = meshFilter.sharedMesh;

        if (mesh == null || mesh.triangles.Length == 0 || mesh.vertices.Length == 0 || mesh.normals.Length == 0)
        {
            Debug.LogError("Mesh is not properly set up.");
            return (Vector3.zero, Vector3.up);
        }

        // Get the triangles, vertices, and normals from the mesh
        int[] triangles = mesh.triangles;
        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;

        // Calculate the cumulative area of each triangle
        float[] triangleAreas = new float[triangles.Length / 3];
        float totalArea = 0f;

        for (int i = 0; i < triangles.Length; i += 3)
        {
            Vector3 v0 = vertices[triangles[i]];
            Vector3 v1 = vertices[triangles[i + 1]];
            Vector3 v2 = vertices[triangles[i + 2]];

            // Calculate the area using the cross product
            float area = Vector3.Cross(v1 - v0, v2 - v0).magnitude * 0.5f;
            triangleAreas[i / 3] = area;
            totalArea += area;
        }

        // Choose a random triangle weighted by area
        float randomWeight = Random.Range(0, totalArea);
        int selectedTriangleIndex = -1;
        float accumulatedArea = 0f;

        for (int i = 0; i < triangleAreas.Length; i++)
        {
            accumulatedArea += triangleAreas[i];
            if (randomWeight <= accumulatedArea)
            {
                selectedTriangleIndex = i * 3;
                break;
            }
        }

        if (selectedTriangleIndex == -1)
        {
            Debug.LogError("Failed to select a triangle.");
            return (Vector3.zero, Vector3.up);
        }

        // Get the vertices and normals of the selected triangle
        Vector3 vertex0 = vertices[triangles[selectedTriangleIndex]];
        Vector3 vertex1 = vertices[triangles[selectedTriangleIndex + 1]];
        Vector3 vertex2 = vertices[triangles[selectedTriangleIndex + 2]];

        Vector3 normal0 = normals[triangles[selectedTriangleIndex]];
        Vector3 normal1 = normals[triangles[selectedTriangleIndex + 1]];
        Vector3 normal2 = normals[triangles[selectedTriangleIndex + 2]];

        // Convert local space vertices and normals to world space
        Transform meshTransform = meshFilter.transform;
        vertex0 = meshTransform.TransformPoint(vertex0);
        vertex1 = meshTransform.TransformPoint(vertex1);
        vertex2 = meshTransform.TransformPoint(vertex2);

        normal0 = meshTransform.TransformDirection(normal0);
        normal1 = meshTransform.TransformDirection(normal1);
        normal2 = meshTransform.TransformDirection(normal2);

        // Generate a random point within the selected triangle and interpolate the normal
        (Vector3 randomPoint, Vector3 randomNormal) = RandomPointAndNormalInTriangle(vertex0, vertex1, vertex2, normal0, normal1, normal2);

        return (randomPoint, randomNormal);
    }

    private static (Vector3 point, Vector3 normal) RandomPointAndNormalInTriangle(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 n0, Vector3 n1, Vector3 n2)
    {
        // Generate random barycentric coordinates
        float r1 = Mathf.Sqrt(Random.value);
        float r2 = Random.value;

        // Calculate the random point within the triangle
        Vector3 randomPoint = (1 - r1) * v0 + (r1 * (1 - r2)) * v1 + (r1 * r2) * v2;

        // Interpolate the normals using the same barycentric coordinates
        Vector3 randomNormal = (1 - r1) * n0 + (r1 * (1 - r2)) * n1 + (r1 * r2) * n2;

        return (randomPoint, randomNormal.normalized);
    }

    private void OnDrawGizmosSelected() {
        if(placementPoint == Placement.ScaleIsArea){
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, transform.localScale);
            Gizmos.color = Color.green * new Color(1,1,1,0.25f);
            Gizmos.DrawCube(transform.position, transform.localScale);
        }
    }
}