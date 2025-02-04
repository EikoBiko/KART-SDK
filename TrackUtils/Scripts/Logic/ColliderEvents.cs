using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderEvents : MonoBehaviour
{
    public bool active = true;
    public UnityEvent onCollisionEnter;
    public UnityEvent onCollisionExit;
    public void CollisionActive(bool state){
        active = state;
    }
    private void OnTriggerEnter(Collider other) {
        if(!active){
            return;
        }
        onCollisionEnter.Invoke();
    }
    private void OnTriggerExit(Collider other){
        if(!active){
            return;
        }
        onCollisionExit.Invoke();
    }
    private void OnCollisionEnter(Collision other){
        if(!active){
            return;
        }
        onCollisionEnter.Invoke();
    }
    private void OnCollisionExit(Collision other) {
        if(!active){
            return;
        }
        onCollisionExit.Invoke();
    }
}
