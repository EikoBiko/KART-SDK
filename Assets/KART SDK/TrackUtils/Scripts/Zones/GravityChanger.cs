using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("KART/Track Elements/Gravity Change Zone")]
public class GravityChanger : SceneElement
{
    public override string Info()
    {
        return "This component causes a track entity's gravity to change when entering a trigger collider.";
    }
    [Tooltip("The direction the entity will fall.")]
    public Vector3 gravityDirection = Vector3.down;

    [Tooltip("If true, effects are only applied when the entity leaves. Useful for resetting the status once the entity is out of the area.")]
    public bool onExit;

    public UnityEvent onKartGravityChange;

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + gravityDirection.normalized);
        Gizmos.DrawSphere(transform.position + gravityDirection.normalized, 0.1f);
    }
}
