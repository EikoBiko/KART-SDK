using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("KART/Track Elements/Velocity Zone")]
public class VelocityChanger : SceneElement
{
    public override string Info()
    {
        return "This component will set a track entity's velocity when it passes through a trigger collider.";
    }
    public Vector3 velocity;
    public enum VelocityMode{
        Set,
        Add
    }
    public enum RelativeTo{
        World,
        Self,
        Kart
    }
    [Tooltip("How the velocity is applied. If \"Set\", its velocity will be equal to what you've set until it leaves the collider. If \"Add\", then the velocity will be added every frame until it exits the collider.")]
    public VelocityMode velocityMode;
    [Tooltip("How the direction is determined. If \"World\", velocity will be applied in the supplied direction. If \"Self\", it will be applied in the direction relative to how this object is faced. If \"Kart\", it will be applied in the direction relative to the target enitity.")]
    public RelativeTo velocityRelativeTo;
    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0.5f, 0f);
        
        Vector3 endPoint;

        if (velocityRelativeTo == RelativeTo.Self) {
            endPoint = transform.position + transform.TransformDirection(velocity);
        } else {
            endPoint = transform.position + velocity;
        }

        // Draw the line
        Gizmos.DrawLine(transform.position, endPoint);
    }
}
