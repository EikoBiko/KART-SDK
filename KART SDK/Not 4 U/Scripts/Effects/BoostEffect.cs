using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "KART SDK/Effects/Boost")]
public class BoosterEffect : Effect
{
    /// <summary>
    /// How many in-game units to speed the kart up by initially.
    /// </summary>
    public float initialBoostAmount = 20;

    /// <summary>
    /// How long the racer will retain "boosting" status.
    /// </summary>
    public float boostDuration = 2;

    public override string ToString()
    {
        return name + " will give racers a speed boost of " + initialBoostAmount + ". Then they'll maintain a boost for " + boostDuration + " seconds.";
    }
}
