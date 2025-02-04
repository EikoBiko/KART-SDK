using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionSetter : MonoBehaviour
{
    /// <returns>The vector from the DirectionSetter's parent to the DirectionSetter.</returns>
    public Vector3 VectorToDirectionSetter(){
        if (transform.parent != null)
        {
            return VectorToDirectionSetter(transform.parent.position);
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 VectorToDirectionSetter(Vector3 origin){
        return transform.position - origin;
    }

    public Vector3 currentVelocity = Vector3.zero;
    private Vector3 previousPosition = Vector3.zero;

    private void Awake() {
        previousPosition = transform.position;
    }

    private void FixedUpdate() {
        currentVelocity = (transform.position - previousPosition) / Time.fixedDeltaTime;
        previousPosition = transform.position;
    }
}
