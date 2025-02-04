using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMarker : MonoBehaviour
{
    private void OnValidate() {
        gameObject.tag = "Item Marker";
    }
    public ItemBoxPreset boxContents;
}
