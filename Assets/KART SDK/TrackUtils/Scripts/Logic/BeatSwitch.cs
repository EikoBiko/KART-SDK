using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeatSwitch : SceneElement
{
    public override string Info()
    {
        return "This will be triggered according to the BPM of the track's music settings.";
    }
    public enum BeatTiming{
        Normal,
        Half,
        Double
    }
    [Tooltip("How fast the trigger occurs. Normal is once per beat, half is twice per beat, double is once every two beats.")]
    public BeatTiming timing;
    public UnityEvent onBeat;
    public void HitBeat(){
        onBeat.Invoke();
    }
}
