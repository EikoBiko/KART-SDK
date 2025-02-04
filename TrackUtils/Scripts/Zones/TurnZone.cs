using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("KART/Track Elements/Turn Zone")]
public class TurnZone : SceneElement
{
    public override string Info()
    {
        return "This will force racers to turn a specific direction when passing through it.";
    }

    [Tooltip("How close to the target direction the racer must be facing before the turn is forced. Look at the ring around this object; if the racer's rotation is green then their rotation will not be changed. If it's red, it will be.")]
    [Range(-1f, 1f)]
    public float correctionForgiveness = 0.5f;

    [HideInInspector]
    public Vector3 targetDirection = Vector3.forward;
    public float angle = 0f;

    public override void OnValidate()
    {
        base.OnValidate();
        Quaternion rotation = Quaternion.AngleAxis(angle, transform.up);
        targetDirection = rotation * transform.forward;
    }

private void OnDrawGizmosSelected()
{

    Gizmos.color = Color.cyan;
    Gizmos.DrawLine(transform.position, transform.position + targetDirection.normalized);
    Gizmos.DrawSphere(transform.position + targetDirection.normalized, 0.1f);

    Vector3 position = transform.position;

    // Draw the correction threshold arc
    Gizmos.color = Color.green;
    float angle = Mathf.Acos(correctionForgiveness) * Mathf.Rad2Deg;
    DrawArc(position, targetDirection, 1, angle);

    // Draw the non-covered range arc
    Gizmos.color = Color.red;
    float nonCoveredAngle = 180 - angle; // Non-covered range
    DrawArc(position, -targetDirection, 1, nonCoveredAngle);
}

private void DrawArc(Vector3 position, Vector3 direction, float radius, float angle)
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

        Gizmos.DrawLine(prevPoint, nextPoint);
        prevPoint = nextPoint;
    }
}







}
