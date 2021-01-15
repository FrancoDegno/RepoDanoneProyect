using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFireworks : MonoBehaviour
{
    public GameObject fireWorks;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(generateFireworks());
    }

    IEnumerator generateFireworks()
    {
        yield return new WaitForSeconds(Random.Range(1, 3));

        Instantiate(fireWorks, (Vector2)transform.position + (Random.insideUnitCircle * 1.5f), transform.rotation);
        yield return generateFireworks();
    }
}
