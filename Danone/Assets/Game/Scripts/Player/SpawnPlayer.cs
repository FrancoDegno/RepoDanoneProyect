using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField]
    GameObject[] players = new GameObject[3];
    [SerializeField]
    Transform possiblePoints;

    // Start is called before the first frame update
    void Start()
    {
        int selectedPlayer = PlayerPrefs.GetInt("selectedPlayer");
        GameObject player = Instantiate(players[selectedPlayer], this.transform.position, this.transform.rotation);

        for(int i=0;i<possiblePoints.childCount;i++)
        {
            player.GetComponentInChildren<PlayerMovement>().possibleMovement[i] = possiblePoints.GetChild(i);
        }
    }
    
}
