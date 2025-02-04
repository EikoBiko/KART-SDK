using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "KART SDK/Effects/Teleport")]
public class TeleportEffect : Effect
{

    public Color transitionColor = Color.white;
    public override void DrawGizmos(Transform transform, DirectionSetter directionSetter)
    {
        DrawLabel(transform.position - transform.forward, "Teleporter In", Color.red);
        DrawLabel(directionSetter.transform.position + directionSetter.transform.forward, "Teleporter Out", Color.blue);
        DrawArrow(transform.position - transform.forward, transform.position, Color.red);
        DrawArrow(transform.position, directionSetter.transform.position, Color.magenta);
        DrawArrow(directionSetter.transform.position, directionSetter.transform.position + directionSetter.transform.forward, Color.blue);
    }

    void DrawArrow(Vector3 start, Vector3 end, Color color)
    {
        Gizmos.color = color;

        // Draw the line from start to end
        Gizmos.DrawLine(start, end);

        // Calculate the direction and length
        Vector3 direction = (end - start).normalized;
        float length = Vector3.Distance(start, end);

        // Arrowhead properties
        float arrowHeadLength = 0.5f;
        float arrowHeadAngle = 45.0f;

        // Calculate the arrowhead points
        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, -arrowHeadAngle, 0) * Vector3.back * arrowHeadLength;
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, arrowHeadAngle, 0) * Vector3.back * arrowHeadLength;

        // Draw the arrowhead
        Gizmos.DrawLine(end, end + right);
        Gizmos.DrawLine(end, end + left);
    }

}
