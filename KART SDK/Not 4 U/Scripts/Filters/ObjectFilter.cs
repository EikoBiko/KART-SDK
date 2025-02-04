using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "KART SDK/Filters/Object")]
public class ObjectFilter : Filter
{
    [Tooltip("If this is true, only racers will be effected. If false, racers will not be effected.")]
    public bool racers = false;
}
