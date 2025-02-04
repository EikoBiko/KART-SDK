using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "KART SDK/Effects/Turn")]
public class TurnEffect : Effect
{
    [Range(-1f, 1f)]
    public float correctionStrength = 0.5f;

    [HideInInspector]
    public Vector3 targetDirection = Vector3.forward;
    public enum RotationMode{
        LookDirection,
        Rotate,
        RotateWithVelocity
    }
    public RotationMode mode;
    public float turnDirection = 0f;

    public override void DrawGizmos(Transform transform, DirectionSetter directionSetter)
    {
        if(mode == RotationMode.LookDirection){
            Quaternion rotation = Quaternion.AngleAxis(turnDirection, transform.up);
            targetDirection = rotation * transform.forward;

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, transform.position + targetDirection.normalized);
            Gizmos.DrawSphere(transform.position + targetDirection.normalized, 0.1f);

            Vector3 position = transform.position;

            // Draw the correction threshold arc
            Gizmos.color = Color.green;
            float angle = Mathf.Acos(correctionStrength) * Mathf.Rad2Deg;
            DrawArc(transform, position, targetDirection, 1.5f, angle, "allowed angles");

            // Draw the non-covered range arc
            Gizmos.color = Color.red;
            float nonCoveredAngle = 180 - angle; // Non-covered range
            DrawArc(transform, position, -targetDirection, 1.5f, nonCoveredAngle, "disallowed angles");
        }
    }

    private void DrawArc(Transform transform, Vector3 position, Vector3 direction, float radius, float angle, string label)
    {
        // Create a rotation that aligns the forward direction with targetDirection
        Quaternion rotation = Quaternion.LookRotation(direction, transform.up);

        Vector3 startDirection = rotation * Quaternion.Euler(0, -angle, 0) * Vector3.forward;
        float step = (angle * 2) / 20;

        Vector3 prevPoint = position + startDirection.normalized * radius;
        for (int i = 1; i <= 20; i++)
        {
            Vector3 nextDirection = rotation * Quaternion.Euler(0, -angle + step * i, 0) * Vector3.forward;
            Vector3 nextPoint = position + nextDirection.normalized * radius;
            if(i == 11){
                DrawLabel(prevPoint, name + " " + label, Gizmos.color);
            }
            Gizmos.DrawLine(prevPoint, nextPoint);
            prevPoint = nextPoint;

        }
    }

    public override string ToString()
    {
        float degrees = Unity.Mathematics.math.remap(-1f, 1f, 180f, 0f, correctionStrength);

        string message = name;

        if(degrees == 0f){
            message += " will force all racers to look " + turnDirection + " degrees relative to this object.";
        }else if(degrees == 180f){
            message += " won't do anything since Correction Strength is too low.";
        }else{
            message += " will force racers to look " + turnDirection + " degrees relative to this object. Racers who are looking up to ~" + Math.Floor(degrees) + " degrees to the left or right will be forgiven.";
        }
        return message;
    }
}
