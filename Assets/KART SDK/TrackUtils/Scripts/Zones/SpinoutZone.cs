using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("KART/Track Elements/Spinout Zone")]
public class SpinoutZone : SceneElement
{
    public override string Info()
    {
        return "This component will cause a kart to spin out when passing through a trigger collider. May also trigger set events when it happens.";
    }
    public float maxSpinoutTime = 3f;
    public UnityEvent onSpinOut;

}
