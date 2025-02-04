using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flingable : Mover
{
    public Rigidbody mainRigidbody;
    public bool flingOnStart = false;
    public float flingStrength = 20f;
    public enum RandomFlingMode{
        Up,
        Any
    }
    public RandomFlingMode flingMode = RandomFlingMode.Up;

    private Vector3 up;
    private void Awake() {
        base.Awake();
        up = transform.up;
        if(flingOnStart){
            Fling();
        }
    }

    public void Fling(){
        if(flingMode == RandomFlingMode.Up){
            Fling(up);
        }else{
            Fling(Vector3.zero);
        }
    }

    public void Fling(Vector3 upward){
        Debug.Log("Normal fling.");
        Vector3 direction = Random.onUnitSphere;
        if(upward != Vector3.zero){
            if(Vector3.Dot(direction, upward) < 0){
                direction *= -1;
            }
        }
        direction *= flingStrength;
        mainRigidbody.linearVelocity = direction;
    }


    public void FlingFromCollision(CollisionData collisionData){
        if(collisionData == null){
            Fling();
            return;
        }
        transform.position = collisionData.position;
        Vector3 direction = ScatterDirection(collisionData.velocity, 25f);
        direction = direction.normalized * flingStrength;
        mainRigidbody.linearVelocity = direction * 1.5f;
    }

    public void FlingFromCollisionUsingVelocity(CollisionData collisionData){
        if(collisionData == null){
            Fling();
            return;
        }
        transform.position = collisionData.position;
        Vector3 direction = ScatterDirection(collisionData.velocity, 25f);
        direction = direction.normalized * collisionData.velocity.magnitude;
        mainRigidbody.linearVelocity = direction * 1.5f;
    }

    // Scatter the given direction vector within a certain angle range
    public Vector3 ScatterDirection(Vector3 direction, float maxAngle)
    {
        // Normalize the direction to make sure it's a unit vector
        direction.Normalize();
        
        // Generate a random rotation angle within the specified range
        float angle = Random.Range(-maxAngle, maxAngle);
        
        // Get a random rotation axis that is perpendicular to the direction
        Vector3 rotationAxis = Vector3.Cross(direction, Random.onUnitSphere);
        
        // Create the rotation quaternion
        Quaternion randomRotation = Quaternion.AngleAxis(angle, rotationAxis);
        
        // Apply the rotation to the direction vector
        return randomRotation * direction;
    }
}
