using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "KART SDK/Effects/Bonk")]
public class BonkEffect : Effect
{
    public float bonkPower;

    public Vector3 direction;
    public enum Relativity{
        World,
        Self,
        Away,
        OppositeIncoming
    }
    [Tooltip("The relative direction for bonking. 'World' refers to the absolute direction supplied. 'Self' refers to the direction relative to the way this object is facing. 'Away' will simply bonk the racer away according to which direction they entered from -- direction will not be used.")]
    public Relativity relativeTo = Relativity.OppositeIncoming;
    public override void DrawGizmos(Transform transform, DirectionSetter directionSetter)
    {
        string label = " Direction";
        Gizmos.color = new Color(0, 0.75f, 0f);
        
        Vector3 endPoint;

        if (relativeTo == Relativity.World) {
            endPoint = transform.position + (direction.normalized);
        } else if(relativeTo == Relativity.Self) {
            endPoint = transform.position + transform.TransformDirection(direction.normalized);
        }else{
            endPoint = transform.position + (transform.up * bonkPower);
            label = " Power";
        }
        Gizmos.DrawLine(transform.position, endPoint);

        DrawLabel(endPoint, name + label, Gizmos.color);
    }

    public override string ToString()
    {
        string message = name + " will bonk racers with a force of " + bonkPower + " ";
        if(relativeTo == Relativity.World){
            message += "in the direction" + direction + ", relative to the world axis.";
        }else if (relativeTo == Relativity.Self){
            message += "in the direction" + direction + ", relative to this object's local axis.";
        }else if (relativeTo == Relativity.Away){
            message += "away from the collision point.";
        }else if (relativeTo == Relativity.OppositeIncoming){
            message += "away from the incoming direction if both parties have a rigidbody. Otherwise, it will bonk them away from point of collision.";
        }
        return message;
    }
}