using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RowController : MonoBehaviour
{

    public List<Person> lPersons = new List<Person>();
    public Transform[] pointsRow = new Transform[5];

    // Start is called before the first frame update
    void Start()
    {
        Person.OnPersonDestroy += clearList;
    }

    private void OnDisable()
    {
        Person.OnPersonDestroy -= clearList;
    }

    public void assignPerson(Person person,int rowSelected)
    {
        int increaseLayer = 0;

        if (rowSelected == 0)
            increaseLayer = 19;

        if(rowSelected==1)
        {
            increaseLayer = 60;
        }
        if(rowSelected==2)
        {
            increaseLayer = 100;
        }

        if(person.gameObject.layer==8)
        {
            for (int j = 0; j < person.transform.childCount; j++)
            {
                SpriteRenderer[] spritesPerson = person.transform.GetChild(j).GetComponentsInChildren<SpriteRenderer>();
                for (int i = 0; i < spritesPerson.Length; i++)
                {
                    spritesPerson[i].sortingOrder += increaseLayer;
                }
            }

        }
        else
        {
            SpriteRenderer[] spritesPerson = person.GetComponentsInChildren<SpriteRenderer>();
            for (int i = 0; i < spritesPerson.Length; i++)
            {
                spritesPerson[i].sortingOrder += increaseLayer;
            }
        }


        lPersons.Add(person);
        person.pointToMov = pointsRow[pointsRow.Length-1].position;
        StartCoroutine(assignPoints());

    }

    void clearList()
    {

        for(int i=0;i<lPersons.Count;i++)
        {
            if(lPersons[i].attended)
            {
                GameObject personToDestroy = lPersons[i].gameObject;
                lPersons.Remove(lPersons[i]);
                Destroy(personToDestroy);
                break;
            }
        }


        StartCoroutine(assignPoints());
    }


    IEnumerator assignPoints()
    {

        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < lPersons.Count; i++)
        {
            lPersons[i].pointToMov = pointsRow[i].position;
        }
    }


}
