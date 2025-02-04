using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Counter : MonoBehaviour
{
    public int currentCount = 0;
    public int maxCount = 10;
    public int minCount = -10;
    public UnityEvent onMaxReached;
    public UnityEvent onMinReached;

    public void AddOne(){
        currentCount++;
        if(currentCount == maxCount){
            onMaxReached.Invoke();
        }
    }
    public void MinusOne(){
        currentCount--;
        if(currentCount == minCount){
            onMinReached.Invoke();
        }
    }
    public void ResetCountToZero(){
        currentCount = 0;
    }
    public void SetCount(int count){
        currentCount = count;
    }
    public void AdjustCount(int amount){
        currentCount += amount;
    }

}
