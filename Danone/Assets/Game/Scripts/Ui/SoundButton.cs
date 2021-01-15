using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : MonoBehaviour
{
    [SerializeField]
    AudioClip clipClick;
    AudioSource audioS;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    public void OnClickSound()
    {
        audioS.PlayOneShot(clipClick);
    }

}
