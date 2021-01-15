using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAudioRdm : MonoBehaviour
{
    AudioSource audio;
    [SerializeField]
    AudioClip[] clips = new AudioClip[5];


    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        StartCoroutine(rdmSound());
    }


    IEnumerator rdmSound()
    {
        yield return new WaitForSeconds(Random.Range(10f, 15f));
        audio.PlayOneShot(clips[Random.Range(0, clips.Length)]);
        yield return rdmSound();
    }
    
}
