using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "KART SDK/Filters/Heat")]
public class HeatFilter : NumericalFilter
{
    public float heat = 50f;
    public ImpreciseOperator operation;
}
