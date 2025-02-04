using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mover : MonoBehaviour
{
    public void CopyPosition(Transform transform){
        if(transform == null){
            return;
        }
        this.transform.position = transform.position;
    }
    public void CopyRotation(Transform transform){
        if(transform == null){
            return;
        }
        this.transform.rotation = transform.rotation;
    }
    public void CopyPositionAndRotation(Transform transform){
        CopyPosition(transform);
        CopyRotation(transform);
    }
    private Vector3 startPosition;
    private Quaternion startRotation;
    public void Awake() {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    public void ResetPosition(){
        transform.position = startPosition;
    }

    public void ResetRotation(){
        transform.rotation = startRotation;
    }

    public void Reset(){
        ResetPosition();
        ResetRotation();
    }
}
