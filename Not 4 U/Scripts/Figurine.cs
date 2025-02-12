using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Figurine", menuName = "KART SDK/Figurine")]
public class Figurine : Collectable
{
    public string figurineName;
    public string figurineOrigin;
    [TextArea(3,9)]
    public string figurineDescription;
    public GameObject figurinePrefab;
}
