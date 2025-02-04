using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteAlways]
public class Roamer : SceneElement
{
    public float moveSpeed = 1f;
    public Rigidbody mainRigidbody;
    [Range(0, 1)]
    public float gravityForce = 1f;
    public bool terrainCollisionsOnly = true;
    public enum NavigationMode{
        Ground,
        Air
    }
    public NavigationMode navigationMode = NavigationMode.Ground;

    public enum RotationMode{
        MovementDirectionAndUpright,
        MovementDirection,
        Upright,
        None
    }
    public RotationMode rotationMode = RotationMode.MovementDirectionAndUpright;
    public bool rotateTowardMovement = true;
    public float allowedRadius = 5f;

    public delegate void TriggerHit();
    public TriggerHit triggerDirectHit;
    public void DirectHit(){
        triggerDirectHit?.Invoke();
    }

    private void Update() {
        if(!Application.isPlaying){
            origin = transform.position;
        }
    }

    private Vector3 origin;
    private void OnDrawGizmosSelected() {
        if(allowedRadius > 0 && !Application.isPlaying){
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(origin, allowedRadius);
        }
    }
}
