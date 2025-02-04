using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneElement : MonoBehaviour
{
    [HideInInspector]
    public bool processed = false;
    [HideInInspector]
    public string info;
    public virtual void OnValidate() {
        gameObject.tag = "Loader Element";
        if(info == ""){
            info = Info();
        }
    }
    public virtual string Info(){
        return "";
    }
    public virtual string Warning(){
        return "";
    }
}
