using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("KART/Track Elements/Cling Zone (With Velocity)")]
public class ClingVelocityZone : SceneElement
{
    public override string Info()
    {
        return "This will allow an entity to cling to walls as long as the kart is within the bounds of this collider, and speed is above a certain threshold. Effect is only granted if the kart enters at the appropriate speed.";
    }
    public float minimumSpeed = 50f;
    public DirectionSetter direction;

    public override void OnValidate() {
        base.OnValidate();
        if (direction == null){
            direction = Instantiate(new GameObject(), transform).AddComponent<DirectionSetter>();
            direction.gameObject.name = "Cling Velocity - Fall Direction";
        }else{
            fallDirection = direction.VectorToDirectionSetter().normalized;
        }
    }

    [HideInInspector]
    public Vector3 fallDirection = Vector3.down;

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + fallDirection.normalized);
        Gizmos.DrawSphere(transform.position + fallDirection.normalized, 0.1f);
        if (direction != null){
            fallDirection = direction.VectorToDirectionSetter().normalized;
        }
    }
}
