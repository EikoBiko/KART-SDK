using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Setting", menuName = "KART SDK/Item Setting")]
public class ItemSetting : ScriptableObject
{
    public enum ItemType{
        Boost,
        Missile,
        Oil,
        Cone,
        Blade
    }
    [Tooltip("The type of item that will spawn if picked.")]
    public ItemType itemType;
    [Tooltip("How many of said item will be given to the racer.")]
    public int count = 1;
    [Tooltip("The higher this number, the more likely it will spawn. This number will decrease the further the player is from the highestProbabilityPoint.")]
    public float maxProbabilityWeight = 1;
    [Range(0.0f, 1.0f)]
    [Tooltip("The lowest percentile you can be while still acquiring this item. For instance last place is 0th percentile, so that would be a value of 0.")]
    public float lowestPercentile; 
    [Range(0.0f, 1.0f)]
    [Tooltip("The percentile in which you are most likely to get this item.")]
    public float peakPercentile; 
    [Tooltip("The highest percentile you can be while still acquiring this item. For instance, 1st place is 100th percentile, so that would be a value of 1.")]
    [Range(0.0f, 1.0f)]
    public float highestPercentile; 

    private void OnValidate() {
        if(lowestPercentile > peakPercentile){
            lowestPercentile = peakPercentile;
        }
        if(peakPercentile > highestPercentile){
            highestPercentile = peakPercentile;
        }
        if(count < 1){
            count = 1;
        }
        if(maxProbabilityWeight < 1){
            maxProbabilityWeight = 1;
        }
    }
}
