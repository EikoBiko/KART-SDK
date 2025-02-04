using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public bool loop;
    private bool stop = false;
    public bool startAutomatically;
    public float timerSeconds = 3f;
    public float randomizationRange = 0f;
    public UnityEvent onTimerEnd;
    public void Start(){
        if(startAutomatically){
            StartTimer();
        }
    }

    private float GetRandomTime(){
        float range;
        if(randomizationRange == 0){
            range = 0;
        }else{
            range = Random.Range(-randomizationRange, randomizationRange);
        }
        return timerSeconds + range;
    }

    public void StartTimer(){
        stop = false;
        if(currentTimer != null){
            StopCoroutine(currentTimer);
        }
        currentTimer = StartCoroutine(TriggerIn(GetRandomTime()));
    }
    public void EndTimer(){
        if(currentTimer != null){
            StopCoroutine(currentTimer);
        }
        onTimerEnd.Invoke();
        CheckLoop();
    }
    
    public void CancelTimer(){
        stop = true;
    }

    public void CheckLoop(){
        if(loop){
            currentTimer = StartCoroutine(TriggerIn(GetRandomTime()));
        }else{
            currentTimer = null;
        }
    }

    private Coroutine currentTimer;
    IEnumerator TriggerIn(float seconds){
        yield return new WaitForSeconds(seconds);
        if(!stop){
            EndTimer();
        }
    }

    private void OnValidate() {
        if(randomizationRange > timerSeconds){
            randomizationRange = timerSeconds;
        }
        if(randomizationRange < 0){
            randomizationRange = 0;
        }
    }
}
