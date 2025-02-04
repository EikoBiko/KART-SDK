using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUIMarker : MonoBehaviour
{
    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(transform.localScale.x * 2, 0, transform.localScale.z * 2));
    }
}
