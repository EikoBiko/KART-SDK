using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornTest : MonoBehaviour
{
    public Horn horn;
    [Header("Don't touch this stuff below...")]
    public AudioSource audioSource;
    public Animator animator;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space) && horn != null && horn.audio != null){
            animator.SetTrigger("Beat");
            audioSource.Stop();
            audioSource.PlayOneShot(horn.audio);
        }
    }
}
