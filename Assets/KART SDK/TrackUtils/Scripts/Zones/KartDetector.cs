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
    public UnityEvent onKartArrive;
    public UnityEvent onKartStay;
    public UnityEvent onKartLeave;
}
