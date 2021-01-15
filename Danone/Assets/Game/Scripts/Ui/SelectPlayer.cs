using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlayer : MonoBehaviour
{
    [SerializeField]
    Sprite[] normalImages = new Sprite[3];
    [SerializeField]
    Sprite[] selectedImages = new Sprite[3];
    [SerializeField]
    Image[] charButtons = new Image[3];

    private void Start()
    {
        selectMale();
    }

    public void selectMale()
    {
        desSelect();
        PlayerPrefs.SetInt("selectedPlayer", 0);    
        charButtons[0].sprite = selectedImages[0];
    }

    public void selectFemale()
    {
        desSelect();
        PlayerPrefs.SetInt("selectedPlayer", 1);
        charButtons[1].sprite = selectedImages[1];
    }

    public void selectNeutral()
    {
        desSelect();
        PlayerPrefs.SetInt("selectedPlayer", 2);
        charButtons[2].sprite = selectedImages[2];
    }
    

    void desSelect()
    {
        for( int i=0;i<charButtons.Length;i++)
        {
            charButtons[i].sprite = normalImages[i];
        }
    }

}
