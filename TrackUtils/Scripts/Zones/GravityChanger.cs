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
    [Tooltip("The direction the entity will fall -- you can position this to change the direction.")]
    public DirectionSetter direction;
    [HideInInspector]
    public Vector3 fallDirection = Vector3.zero;

    [Tooltip("If true, effects are only applied when the entity leaves. Useful for resetting the status once the entity is out of the area.")]
    public bool onExit;

    public UnityEvent onKartGravityChange;

    public override void OnValidate() {
        base.OnValidate();
        if (direction == null){
            direction = Instantiate(new GameObject(), transform).AddComponent<DirectionSetter>();
            direction.gameObject.name = "Gravity Changer - Fall Direction";
        }else{
            fallDirection = direction.VectorToDirectionSetter().normalized;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + fallDirection.normalized);
        Gizmos.DrawSphere(transform.position + fallDirection.normalized, 0.1f);
        if (direction != null){
            fallDirection = direction.VectorToDirectionSetter().normalized;
        }
    }
}
