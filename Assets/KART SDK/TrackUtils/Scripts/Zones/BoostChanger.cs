using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("KART/Track Elements/Boost Zone")]
public class BoostChanger : SceneElement
{
    public override string Info()
    {
        return "This will cause a kart to boost when passing through a trigger collider.";
    }
    /// <summary>
    /// How many in-game units to speed the kart up by initially.
    /// </summary>
    public float initialBoostAmount = 20;

    /// <summary>
    /// How long the racer will retain "boosting" status.
    /// </summary>
    public float boostDuration = 2;

    public UnityEvent onKartBoost;

}