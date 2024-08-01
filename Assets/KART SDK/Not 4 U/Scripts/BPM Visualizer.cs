using UnityEngine;
using System.Collections.Generic;

public class BPMVisualizer : MonoBehaviour
{
    public Music setting;
    public AudioSource audioSource;
    public Animator animator;
    public float beatTime = 1f;
    private float lastBeatTime;

    public float GetBeatLength(float noteSpeed){
        return 60f / (setting.bpm * noteSpeed);
    }

    public bool DidBeatOccurAt(float beatTime){
        if(Mathf.FloorToInt(beatTime) != lastBeatTime){
            lastBeatTime = Mathf.FloorToInt(beatTime);
            return true;
        }else{
            return false;
        }
    }
    private void Start() {
        audioSource.clip = setting.musicFile;
        animator = gameObject.GetComponent<Animator>();
        if(gameObject.GetComponent<AudioSource>() != null)audioSource.Play();
    }

    private void Update() {
        float sampledTime = audioSource.timeSamples / (audioSource.clip.frequency * GetBeatLength(beatTime));
        if(DidBeatOccurAt(sampledTime)){
            BeatFunction();
        }
    }

    // Function to be called on every beat
    void BeatFunction()
    {
        animator.SetTrigger("Beat");
    }
}
