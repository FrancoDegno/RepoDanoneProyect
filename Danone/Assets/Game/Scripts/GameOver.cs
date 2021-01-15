using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    SaveScore myScoreManager;
    public RowController[] rows = new RowController[3];
    [SerializeField]
    AudioClip warng1;
    [SerializeField]
    AudioClip warng2;
    [SerializeField]
    GameObject Sprite_warning;
    [SerializeField]
    GameObject popUp_GameOver;
    [SerializeField]
    int spots = 0;
    AudioSource audioS;
    int timesSound = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < rows.Length; i++)
        {
            spots += rows[i].pointsRow.Length;
        }
        spots -= rows.Length;
        audioS = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        GeneratePerson.whenSpawnPerson_Event += detectEnd;
    }

    private void OnDisable()
    {
        GeneratePerson.whenSpawnPerson_Event -= detectEnd;
    }


    public void detectEnd()
    {

        int persons = 0;
        for(int i=0;i<rows.Length;i++)
        {
            persons += rows[i].lPersons.Count;
        }

        if(persons>spots/1.3f && persons<spots)
        {
            Sprite_warning.SetActive(true);
        }
        else
        if(persons < spots / 1.3f)
        {
            Sprite_warning.SetActive(false);
            timesSound = 0;
        }

        if(persons>spots-3 && timesSound==1)
        {
            timesSound = 2;
            Sprite_warning.GetComponent<Animator>().SetBool("WarningRed", true);
            audioS.PlayOneShot(warng2);
        }
        else
        {
            if(Sprite_warning.activeInHierarchy && timesSound==0)
            {
                timesSound = 1;
                Sprite_warning.GetComponent<Animator>().SetBool("WarningRed", false);
                audioS.PlayOneShot(warng1);
            }

        }

        if(persons==spots)
        {
            myScoreManager.saveData();
            GeneratePerson.whenSpawnPerson_Event -= detectEnd;
        }

    }

}
