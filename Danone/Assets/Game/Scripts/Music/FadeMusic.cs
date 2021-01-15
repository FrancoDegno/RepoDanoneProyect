using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FadeMusic : MonoBehaviour
{

    public static FadeMusic fadeMusic;


    Animator anim;
    [SerializeField]
    AudioClip clipMenu;
    [SerializeField]
    AudioClip clipGameplay;
    [SerializeField]
    AudioClip clipOver;

    AudioSource source;

    private void Start()
    {
        if (fadeMusic != null)
            Destroy(this.gameObject);


        GameObject.DontDestroyOnLoad(this.gameObject);
        fadeMusic = this;
        source = GetComponent<AudioSource>();
        anim = this.GetComponent<Animator>();

        source.clip = clipMenu;
        source.Play();
    }


    public void pauseMusic()
    {
        source.Pause();
    }

    public void unPauseMusic()
    {
        source.UnPause();
    }

    public void gameOverMusic()
    {
        source.clip = clipOver;
        source.Play();
    }


    public void fadeIn()
    {
        anim.SetBool("fade", true);
    }


    public void fadeOut()
    {
        anim.SetBool("fade", false);
    }

    private void OnLevelWasLoaded(int level)
    {

        switch(level)
        {
            case 0:
                source.clip = clipMenu;
                source.Play();
                break;
            case 1:
                source.clip = clipMenu;
                source.Play();
                break;
            case 2:
                source.clip = clipGameplay;
                source.Play();
                break;
        }
        anim.SetBool("fade", false);

    }



}
