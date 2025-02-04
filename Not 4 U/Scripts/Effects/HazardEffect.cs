using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "KART SDK/Effects/Hazard")]
public class HazardEffect : Effect
{
    public string damageTag = "world";
    public bool effectNonRacers = false;
    public float maximumSpinoutTime = 3f;
    public int power = 1;

    public override string ToString()
    {
        string message = name + " will cause racers to spin out for a maximum of " + maximumSpinoutTime + " seconds.";
        if( effectNonRacers ){
            message += " It will also destroy items that pass through it.";
        }
        return message;
    }
}
