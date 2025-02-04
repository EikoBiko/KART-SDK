using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "KART SDK/Filters/Group")]
public class FilterGroup : Filter
{
    public List<Filter> filters = new List<Filter>();
}
