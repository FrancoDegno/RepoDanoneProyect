using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    GameObject gamePlayObject;
    [SerializeField]
    GameObject okButton;
    [SerializeField]
    GameObject nextButon;
    [SerializeField]
    GameObject[] go_imgsTuto = new GameObject[6];
    int selector = 0;

    private void Start()
    {
        if(PlayerPrefs.HasKey("toggle"))
        {
            if(PlayerPrefs.GetInt("toggle")==1)
            {
                gamePlayObject.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
    }


    public void checkToggle(bool toggleBool)
    {
        if(toggleBool)
        {
            PlayerPrefs.SetInt("toggle", 1);
        }
        else
        {
            PlayerPrefs.SetInt("toggle", 0);
        }
    }



    public void nextSelector()
    {
        if (selector < 5)
        {
            go_imgsTuto[selector].SetActive(false);
            selector++;
            go_imgsTuto[selector].SetActive(true);
        }
        else
        {
            nextButon.SetActive(false);
            okButton.SetActive(true);
        }
            

    }

  
}
