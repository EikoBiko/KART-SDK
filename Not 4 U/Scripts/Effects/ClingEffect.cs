using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "KART SDK/Effects/Cling")]
public class ClingEffect : Effect
{
    public enum ClingMode{
        Activate,
        Deactivate,
        DeactivateOnExit,
        Velocity
    }
    public ClingMode clingMode = ClingMode.Activate;
    [HideInInspector]
    public Vector3 fallDirection = Vector3.down;
    public float minimumSpeed = 50f;

    public override void DrawGizmos(Transform transform, DirectionSetter directionSetter){
        if (clingMode == ClingMode.Velocity)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, transform.position + fallDirection.normalized);
            Gizmos.DrawSphere(transform.position + fallDirection.normalized, 0.1f);
            if (directionSetter != null)
            {
                fallDirection = directionSetter.VectorToDirectionSetter().normalized;
                DrawLabel(transform.position + fallDirection, name + " fall direction", Gizmos.color);
            }
        }
    }
}
