using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "KART SDK/Effects/Heat")]
public class HeatEffect : Effect
{
    public enum HeatMode{
        PerSecond,
        Ambient
    }
    public HeatMode heatMode;
    public float heat = 10f;
    public override void DrawGizmos(Transform transform, DirectionSetter directionSetter)
    {
        return;
    }

    public override string ToString()
    {
        string message = name + " will ";
        if(heatMode == HeatMode.Ambient){
            message += "set racers' ambient heat to " + heat + ".";
        }else{
            message += "increase racers' heat by " + heat + " per second.";
        }
        return message;
    }
}