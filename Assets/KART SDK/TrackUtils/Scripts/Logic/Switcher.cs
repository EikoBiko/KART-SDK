using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switcher : SceneElement
{
    public override string Info()
    {
        return "Each time Flip() on this component is triggered, it will flip back and forth \"tick\" and \"tock\" events. The first time it triggers will be \"tick\".";
    }
    public UnityEvent tick;
    public UnityEvent tock;

    public delegate void Switch();
    public Switch flipSwitch;

    public void Flip(){
        if(flipSwitch != null){
            flipSwitch();
        }
    }

}
