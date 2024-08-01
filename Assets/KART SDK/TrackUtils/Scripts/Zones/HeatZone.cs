using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("KART/Track Elements/Heat Zone")]
public class HeatZone : SceneElement
{
    public override string Info()
    {
        return "This can lower or increase kart racers' heat value. Negative values decrease heat, positive values increase it.";
    }
    public float heatPerSecond = 10f;
}
