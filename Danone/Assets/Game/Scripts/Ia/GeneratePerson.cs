using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePerson : MonoBehaviour
{
    public delegate void whenSpawnPerson();
    public static whenSpawnPerson whenSpawnPerson_Event;

    [SerializeField]
    RowController[] rows = new RowController[3];
    [SerializeField]
    GameObject[] prefabsPersons = new GameObject[0];
    [SerializeField]
    float delaySpawn;
    int spots = 0;
    int persons = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < rows.Length; i++)
        {
            spots += rows[i].pointsRow.Length;
        }
        spots -= rows.Length;
        StartCoroutine(spawnPerson());
    }


    IEnumerator spawnPerson()
    {
        //if(whenSpawnPerson_Event!=null)
        
        

        if (persons < spots)
        {

            int rowSelected = Random.Range(0, 3);
            int prefabSelect = Random.Range(0, prefabsPersons.Length);
            if (rows[rowSelected].lPersons.Count < 6)
                rows[rowSelected].assignPerson(Instantiate(prefabsPersons[prefabSelect], transform.position, transform.rotation).GetComponent<Person>(),rowSelected);
            else
                yield return spawnPerson();

            if(whenSpawnPerson_Event!=null)
            {
                whenSpawnPerson_Event();
            }
            else
            {
                yield return null;
            }
            persons = 0;
            for (int i = 0; i < rows.Length; i++)
            {
                persons += rows[i].lPersons.Count;
            }

            yield return new WaitForSeconds(delaySpawn);
            if (delaySpawn > 3)
                delaySpawn -= 0.1f;
            yield return spawnPerson();
        }
        else
        {
            Debug.Log("GAME OVER");
        }
    }
}
