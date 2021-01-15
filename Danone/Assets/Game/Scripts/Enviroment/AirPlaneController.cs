using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlaneController : MonoBehaviour
{

    AudioSource audioS;
    public AudioClip clipOut;
    public AudioClip clipIn;
    Animator anim;

    bool fly=false;

    // Start is called before the first frame update
    void Start()
    {
        audioS = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        StartCoroutine(fly_land());
    }

    IEnumerator fly_land()
    {
        yield return new WaitForSeconds(Random.Range(26, 28));
        fly = fly ? false : true;
        anim.SetBool("Fly", fly);
        yield return fly_land();
    }


    public void reproduceOut()
    {
        audioS.PlayOneShot(clipOut);
    }

    public void reproduceIn()
    {
        audioS.PlayOneShot(clipIn);
    }

}
