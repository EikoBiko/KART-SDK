using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingDistributor : MonoBehaviour
{
    public Music music;
    [Header("Don't touch this stuff below...")]
    public bool previewLoop = true;
    public void SetPreviewLoop(bool toggle){
        previewLoop = toggle;
    }
    public AudioSource audioSource;
    public BPMVisualizer[] bPMVisualizers;
    private void Awake() {
        if(music != null){
            foreach(BPMVisualizer bPMVisualizer in bPMVisualizers){
                bPMVisualizer.setting = music;
            }
            int samples = music.musicFile.samples;
            progressVisualizer.maxValue = samples;
            progressSetter.maxValue = samples;
            loopStart.maxValue = samples;
            loopEnd.maxValue = samples;
            SetBehavior();
            loopStart.value = loopStartSample;
            loopEnd.value = loopEndSample;

            progressSetter.onValueChanged.AddListener(delegate {SetMusicPlaybackPosition(); });
            loopStart.onValueChanged.AddListener(delegate {SetMusicLoopStart(); });
            loopEnd.onValueChanged.AddListener(delegate {SetMusicLoopEnd(); });

            for (int i = 0; i < bPMVisualizers.Length; i++)
            {
                GetNumberOfBeats(bPMVisualizers[i]);
            }

            beats.Sort();

            audioSource.Play();
        }
    }

    public List<int> beats = new List<int>();

    public void GetNumberOfBeats(BPMVisualizer bPMVisualizer){
        float time = bPMVisualizer.GetBeatLength(bPMVisualizer.beatTime);
        Debug.Log("At pace " + bPMVisualizer.beatTime + " there are " + (audioSource.clip.length / time) + " beats.");
        GetBeatPositions((int)Math.Floor(audioSource.clip.length / time));
    }

    public void GetBeatPositions(int numberOfBeats){
        int samplesPerBeat = audioSource.clip.samples / numberOfBeats;
        int currentSamples = 0;
        for (int i = 0; i < numberOfBeats; i++)
        {
            beats.Add(currentSamples);
            currentSamples += samplesPerBeat;
        }
    }

    public int FindClosestBeat(int target)
    {
        // Initialize the closest number to the first element
        int closest = beats[0];
        int minDifference = Math.Abs(target - closest);

        // Iterate through the list to find the closest number
        foreach (int number in beats)
        {
            int difference = Math.Abs(target - number);

            if (difference < minDifference)
            {
                closest = number;
                minDifference = difference;
            }
        }

        return closest;
    }

    public void SetEndToClosestBeat(){
        loopEnd.value = FindClosestBeat((int)loopEnd.value) - 1;
    }

    public void SetStartToClosestBeat(){
        loopStart.value = FindClosestBeat((int)loopStart.value);
    }

    public void SetBehavior(){
        if (music != null){
            audioSource.clip = music.musicFile;
            // Set loop points
            if (music.usesCustomLoop && (music.loopStart != music.loopEnd))
            {
                loopStartSample = music.loopStart;
                loopEndSample = music.loopEnd;
                if (loopEndSample <= loopStartSample)
                {
                    loopEndSample = audioSource.clip.samples;
                }
            }
            else if (!music.usesCustomLoop)
            {
                loopStartSample = 0;
                loopEndSample = audioSource.clip.samples;
            }
            nowPlayingText.text = music.songTitle + " - " + music.artist;
        }else{
            Debug.Log("No music set...");
        }
    }

    private int loopStartSample;
    private int loopEndSample;

    private void Update()
    {
        if (audioSource.clip != null)
        {
            if (audioSource.timeSamples >= loopEndSample && previewLoop)
            {
                audioSource.timeSamples = loopStartSample;
            }
            progressVisualizer.value = audioSource.timeSamples;
        }

    }

    public TMP_Text nowPlayingText;
    public Slider progressVisualizer;
    public Slider loopStart;
    public Slider loopEnd;
    public Slider progressSetter;

    public void SetMusicLoopStart(){
        music.loopStart = (int)loopStart.value;
        music.usesCustomLoop = true;
        SetBehavior();
    }

    public void SetMusicLoopStartNow(){
        loopStart.value = progressVisualizer.value;
        music.usesCustomLoop = true;
        SetBehavior();
    }

    public void SetMusicLoopEnd(){
        music.loopEnd = (int)loopEnd.value;
        music.usesCustomLoop = true;
        SetBehavior();
    }

    public void SetMusicLoopEndNow(){
        loopEnd.value = progressVisualizer.value;
        music.usesCustomLoop = true;
        SetBehavior();
    }

    public void SetMusicPlaybackPosition(){
        audioSource.timeSamples = (int)progressSetter.value;
    }
}
