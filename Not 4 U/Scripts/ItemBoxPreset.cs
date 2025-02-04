using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Box Preset", menuName = "KART SDK/Item Box Preset")]
public class ItemBoxPreset : ScriptableObject
{
    public List<ItemSetting> itemPool; 
}
