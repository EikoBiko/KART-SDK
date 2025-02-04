using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapPoint : SceneElement
{
    public override string Info()
    {
        return "This component is used for race tracks to determine the current placement of the racers. When passing through a trigger collider, it will log the racer's progress.";
    }
    public override string Warning(){
        return "These must be placed in the order of which you want racers travelling. They must also all be children of the same object.";
    }
    
    [Tooltip("If this is true, the racer MUST pass through here before further progress is tracked. This prevents massive skips in the track, like driving backwards. When racers spawn, they all mark the first mandatory point as already passed.")]
    public bool mandatory = false;
    [Tooltip("If this is true, it will advance a racer's lap by 1. Only does this when the mandatory point before this has been crossed.")]
    public bool lapMarker = false;

    private void OnDrawGizmosSelected() {

        Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
        Gizmos.matrix = rotationMatrix; 
        if(lapMarker){
            Gizmos.color = new(1f, 0, 1f, 0.25f);
        }else if(mandatory){
            Gizmos.color = new(1, 1, 0, 0.25f);
        }else{
            Gizmos.color = new(0f, 0.5f, 0f, 0.25f);
        }
        
        Gizmos.DrawCube(transform.up * 0.5f, Vector3.one);
    }
}
