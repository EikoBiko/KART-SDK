using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "KART SDK/Filters/Teleport")]
public class TeleportFilter : Filter
{
    [Tooltip("If true, this filter will effect entities that have just arrived from a teleport. If false, it will effect entities that have not.")]
    public bool incoming = true;
}
