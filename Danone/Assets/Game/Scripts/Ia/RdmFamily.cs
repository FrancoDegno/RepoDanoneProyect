using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColumsPerson
{
    public GameObject[] optionsFamily= new GameObject[0];
}

public class RdmFamily : MonoBehaviour
{
    public ColumsPerson[] rowsPersons = new ColumsPerson[0];



    // Start is called before the first frame update
    void Start()
    {
        
        for(int i=0;i<rowsPersons.Length;i++)
        {
            rowsPersons[i].optionsFamily[Random.Range(0, rowsPersons[i].optionsFamily.Length)].SetActive(true);
        }
        

    }
    


}
