using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{

    public static PauseController pause;

    // Start is called before the first frame update
    void Start()
    {
        if (pause != null)
            Destroy(this.gameObject);
        else
            pause = this;


    }

    public void pauseGame()
    {
        Time.timeScale = 0;
    }

    public void unPaused()
    {
        Time.timeScale = 1;
    }
}
