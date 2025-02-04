using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "KART SDK/Filters/Speed Percentile")]
public class SpeedPercentileFilter : NumericalFilter
{
    public override string Info()
    {
        return "Effects will be applied if they are going a percentage of their max speed -- entities that don't track their top speed (like traffic cones) will also be effected regardless.";
    }

    [Range(0f,1f)]
    public float percentile = 0.5f;
    public PreciseOperator operation;
}
