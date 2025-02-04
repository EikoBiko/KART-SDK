using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "KART SDK/Effects/Conveyor")]
public class ConveyorEffect : Effect
{
    public enum Mode{
        Conveyor,
        MovingPlatform
    }
    [Tooltip("This is how the direction is dictated.\n\n'Conveyor' will move entities in the direction toward the DirectionSetter in the supplied speed.\n\n'MovingPlatform' will use the DirectionSetter's movement and speed. In other words, if the DirectionSetter moves left, the direction will be left. If it moves forward, the direction will be forward.")]
    public Mode mode = Mode.Conveyor;
    [HideInInspector]
    public Vector3 direction = Vector3.forward;
    [Tooltip("How rapidly in the direction of the DirectionSetter the entities will move.\n\nIf Mode is set to 'MovingPlatform', this isn't used.")]
    public float speed = 1f;

    private Vector3 gizmoPosition = Vector3.zero;
    public override void DrawGizmos(Transform transform, DirectionSetter directionSetter)
    {
        // Visualize direction...
    }

    public Vector3 ConveyorSpeed(){
        if(mode == Mode.Conveyor){
            return direction.normalized * speed;                
        }else{
            return direction;
        }
    }
}
