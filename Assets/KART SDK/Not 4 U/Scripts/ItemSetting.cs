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
        Cone
    }
    [Tooltip("The type of item that will spawn if picked.")]
    public ItemType itemType;
    [Tooltip("How many of said item will be given to the racer.")]
    public int count = 1;
    [Tooltip("The higher this number, the more likely it will spawn. This number will decrease the further the player is from the highestProbabilityPoint.")]
    public float maxProbabilityWeight = 1;
    [Range(0.0f, 1.0f)]
    [Tooltip("How well the player must be doing before this item will no longer spawn. 0 represents '0% of the opponents are in front of me', 1 represents ' 100% of the opponents are in front of me'.")]
    public float lowPositionCutoff; 
    [Range(0.0f, 1.0f)]
    [Tooltip("The point of highest probability for this item. 0 represents '0% of the opponents are in front of me', 1 represents ' 100% of the opponents are in front of me'.")]
    public float highestProbabilityPoint; 
    [Tooltip("How poorly the player must be doing before this item will no longer spawn. 0 represents '0% of the opponents are in front of me', 1 represents ' 100% of the opponents are in front of me'.")]
    [Range(0.0f, 1.0f)]
    public float highPositionCutoff; 

    private void OnValidate() {
        if(lowPositionCutoff > highestProbabilityPoint){
            lowPositionCutoff = highestProbabilityPoint;
        }
        if(highestProbabilityPoint > highPositionCutoff){
            highPositionCutoff = highestProbabilityPoint;
        }
        if(count < 1){
            count = 1;
        }
        if(maxProbabilityWeight < 1){
            maxProbabilityWeight = 1;
        }
    }
}
