using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceFullScreen : MonoBehaviour
{
    public Text textRes;
    public GameObject objectLandScape;

    // Start is called before the first frame update
    void Start()
    {
        Screen.fullScreen = true;
    }


    public void maximize()
    {
        Screen.fullScreen = true;
    }

    private void Update()
    {
        textRes.text = "Resolution is " + Screen.width+" "+Screen.height;
        if(Screen.width<Screen.height)
        {
            objectLandScape.SetActive(true);
        }
        else
        {
            objectLandScape.SetActive(false);
        }
    }

}
