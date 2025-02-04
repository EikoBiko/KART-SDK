using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("KART/Track Elements/Respawn Override Zone")]
public class RespawnOverride : SceneElement
{
    public override string Info()
    {
        return "This component will override a racer's respawn point when passing through a trigger collider. This component can also be used to release the override.";
    }
    [Tooltip("If this is enabled, it will release the respawn lock rather than set it.")]
    public bool releaseRespawnOverride;
    [Tooltip("This transform represents the spawn location and rotation of the respawn point.")]
    public Transform referenceTransform;
    [Tooltip("The direction of gravity when respawning.")]
    public Vector3 gravityDirection = Vector3.down;

    private void OnDrawGizmos() {
        if(referenceTransform != null && !releaseRespawnOverride){
            Gizmos.DrawCube(referenceTransform.position - (referenceTransform.up * 0.1f), Vector3.one * 0.1f);
            Gizmos.DrawSphere(referenceTransform.position, 0.1f);
            Gizmos.DrawLine(transform.position, referenceTransform.position);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(referenceTransform.position, referenceTransform.position + (referenceTransform.up * 0.25f));
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(referenceTransform.position, referenceTransform.position + (referenceTransform.forward * 0.25f));
            Gizmos.color = Color.red;
            Gizmos.DrawLine(referenceTransform.position, referenceTransform.position + (referenceTransform.right * 0.25f));
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(referenceTransform.position, referenceTransform.position + gravityDirection.normalized);
            Gizmos.DrawSphere(referenceTransform.position + gravityDirection.normalized, 0.1f);
            
        }
    }
    public override void OnValidate() {
        base.OnValidate();
        if(referenceTransform == null){
            referenceTransform = Instantiate(new GameObject(), gameObject.transform).transform;
            referenceTransform.name = "Spawn Location";
        }
    }
    
}
