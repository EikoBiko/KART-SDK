using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingLine : SceneElement
{
    public List<Transform> positions = new List<Transform>();
    new void OnValidate()
    {
        base.OnValidate();
        positions.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.name = "Racer Start Position " + (i + 1);
            positions.Add(child);
        }
    }
    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            // Change the color based on the position in the list
            Gizmos.color = Color.Lerp(Color.red, Color.blue, i / (float)transform.childCount);

            Gizmos.DrawSphere(child.position, 0.5f);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(child.position, child.position + child.forward);
        }
    }

    public void GroundPositions()
    {
        foreach (Transform child in transform)
        {
            RaycastHit hit;
            if (Physics.Raycast(child.position, -Vector3.up, out hit))
            {
                child.position = hit.point;
            }
        }
    }
}
