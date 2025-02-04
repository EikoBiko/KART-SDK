using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "KART SDK/Filters/Speed")]
public class SpeedFilter : NumericalFilter
{
    public override string Info(){
        return "This checks the entity's speed using in-game units.";
    }
    public float speed = 30f;
    public ImpreciseOperator operation;
}
