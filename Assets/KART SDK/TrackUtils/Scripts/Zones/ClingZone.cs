using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("KART/Track Elements/Cling Zone")]
public class ClingZone : SceneElement
{
    public override string Info()
    {
        return "This continually modifies a kart's gravity to match the terrain below it. Be careful; if the kart falls off the track, the gravity won't reset automatically.";
    }
    [Tooltip("If true, disables cling gravity. If false, enables cling gravity.")]
    public bool releaseCling;
    [Tooltip("If true, effects are only applied when the entity leaves. Useful for resetting the status once the entity is out of the area.")]
    public bool onExit;
}
