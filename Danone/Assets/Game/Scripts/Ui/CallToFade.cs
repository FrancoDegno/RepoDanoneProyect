using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CallToFade : MonoBehaviour
{
    public static CallToFade callToFade;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {

        DontDestroyOnLoad(this.gameObject);
        if(callToFade==null)
        {
            callToFade = this;
            anim = GetComponent<Animator>();
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }
    

    public void fadeIn()
    {
        anim.SetBool("Fade", true);
    }

    public void fadeOut()
    {
        anim.SetBool("Fade", false);
    }

    private void OnLevelWasLoaded(int level)
    {
        fadeOut();
    }

}
