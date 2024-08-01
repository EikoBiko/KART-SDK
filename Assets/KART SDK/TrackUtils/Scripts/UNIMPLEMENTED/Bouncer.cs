using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : SceneElement
{
    public override string Info(){
        return "This component will cause an entity on the track to bounce in a given direction. Only triggers when driven over.";
    }

    [Tooltip("Which direction to bounce in.")]
    public Vector3 direction;
    [Tooltip("How hard to bounce in that direction. A value of 1 is equivilent to an average racer's jump.")]
    public float power;
    public enum RelativeTo{
        World,
        Kart
    }
    [Tooltip("If this is set to World, it will bounce the entity in the direction you set. If it's set to Kart, the direction will be based on the direction the entity is facing.")]
    public RelativeTo bounceRelativeTo;

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0.9f, 0.9f);
        // Draw the line
        Gizmos.DrawLine(transform.position, transform.position + direction.normalized);
    }
}
