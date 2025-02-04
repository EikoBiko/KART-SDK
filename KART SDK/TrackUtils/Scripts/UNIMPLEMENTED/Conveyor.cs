using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : SceneElement
{

    public override string Info()
    {
        return "This will move entities in a specified direction until the entity leaves the area. Works best when direction of movement is parallel to terrain.";
    }
    public Vector3 direction = Vector3.forward; // The direction in which the gizmo will move
    public float speed = 1f; // Speed at which the gizmo will move
    private float distanceTraveled = 0f; // Distance traveled by the gizmo
    private Vector3 initialPosition; // Initial position of the gizmo

    void OnDrawGizmosSelected()
    {
        // Store the initial position on the first run
        if (distanceTraveled == 0)
        {
            initialPosition = transform.position;
        }

        // Calculate the new position
        Vector3 newPosition = initialPosition + direction.normalized * distanceTraveled;

        // Draw the gizmo sphere at the new position
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(newPosition, 0.5f);

        // Update the distance traveled
        distanceTraveled += speed * Time.deltaTime;

        // Check if the gizmo has traveled 5 units
        if (distanceTraveled >= 5f)
        {
            distanceTraveled = 0f; // Reset the distance traveled
        }
    }

}
