using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionSetter : MonoBehaviour
{
    public Vector3 VectorToDirectionSetter(){
        if (transform.parent != null)
        {
            return transform.position - transform.parent.position;
        }
        else
        {
            return Vector3.zero;
        }
    }

    private void OnValidate() {
        if (transform.parent != null)
        {
            transform.parent.position = transform.parent.position;
        }
    }

    private void OnDrawGizmosSelected() {
        if(transform.parent != null){
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.parent.position);
        }
    }
}
