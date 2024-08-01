using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingDistributor : MonoBehaviour
{
    public Music musicSetting;
    public AudioSource audioSource;
    public BPMVisualizer[] bPMVisualizers;
    private void Awake() {
        foreach(BPMVisualizer bPMVisualizer in bPMVisualizers){
            bPMVisualizer.setting = musicSetting;
        }
        audioSource.clip = musicSetting.musicFile;
        audioSource.Play();
    }
}
