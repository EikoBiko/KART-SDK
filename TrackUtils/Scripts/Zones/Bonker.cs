using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("KART/Track Elements/Bonk Zone")]
public class Bonker : SceneElement
{
    public override string Info()
    {
        return "This component will cause a track entity to bonk against a trigger collider.";
    }
    public float bonkPower;

    public Vector3 direction;
    public enum Relativity{
        World,
        Self,
        Away
    }
    [Tooltip("The relative direction for bonking. 'World' refers to the absolute direction supplied. 'Self' refers to the direction relative to the way this object is facing. 'Away' will simply bonk the racer away according to which direction they entered from -- direction will not be used.")]
    public Relativity relativeTo;

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(0, 0.75f, 0f);
        
        Vector3 endPoint;

        if (relativeTo == Relativity.World) {
            endPoint = transform.position + (direction.normalized * bonkPower);
        } else if(relativeTo == Relativity.Self) {
            endPoint = transform.position + transform.TransformDirection(direction.normalized * bonkPower);
        }else{
            endPoint = transform.position + (transform.up * bonkPower);
        }

        // Draw the line
        Gizmos.DrawLine(transform.position, endPoint);
    }
}
