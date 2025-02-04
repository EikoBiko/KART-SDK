using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("KART/Track Elements/Kart Detection Zone")]
public class KartDetector : SceneElement
{
    public override string Info()
    {
        return "This component allows you to cause things to happen when a kart enters a trigger collider.";
    }
    [Tooltip("If this is enabled, onKartArrive will only trigger for the first kart in the zone, and onKartLeave will only trigger once all karts have left.")]
    public bool onlyOne;
    public CollisionEvent onKartArrive;
    public UnityEvent onKartStay;
    public CollisionEvent onKartLeave;
    public List<Filter> filters = new List<Filter>();
}

/// <summary>
/// First Vector3 represents position, second represents velocity.
/// </summary>
[Serializable] 
public class CollisionEvent : UnityEvent<CollisionData> { }

public class CollisionData {
    public Vector3 position;
    public Vector3 velocity;
    public Vector3 incomingVector;
    public Transform originTransform;

    public CollisionData(Vector3 positionOfRacer, Vector3 velocityOfRacer, Vector3 incomingVectorOfRacer, Transform origin){
        position = positionOfRacer;
        velocity = velocityOfRacer;
        incomingVector = incomingVectorOfRacer;
        originTransform = origin;
    }
}