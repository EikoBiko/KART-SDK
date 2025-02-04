using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switcher : SceneElement
{
    public override string Info()
    {
        return "Each time Flip() on this component is triggered, it sequentially progresses through the list of operations. It starts with Element 0 in the list.";
    }

    public List<UnityEvent> operations = new(){new()};

    private int currentOperation = 0;

    public void Flip(){
        if(operations.Count == 0){
            return;
        }
        if(currentOperation >= operations.Count){
            currentOperation = 0;
        }
        operations[currentOperation].Invoke();
        currentOperation++;
    }
}
