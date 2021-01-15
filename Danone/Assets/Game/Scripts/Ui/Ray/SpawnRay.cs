using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRay : MonoBehaviour
{

    [SerializeField]
    GameObject go_RayObject;

    float f_TimingCOunt = 0;
    [SerializeField]
    float f_delay=0;

    // Update is called once per frame
    void Update()
    {
        f_TimingCOunt += Time.deltaTime;

        if(f_TimingCOunt>f_delay)
        {
            spawn();
            f_TimingCOunt = 0;
            f_delay += (f_delay / 2);
        }
    }


    void spawn()
    {
        go_RayObject.transform.position = this.transform.position;
        go_RayObject.GetComponent<SpriteRenderer>().color = Color.white;
        go_RayObject.GetComponent<SpriteRenderer>().enabled = true;
        go_RayObject.GetComponent<Collider2D>().enabled = true;
        go_RayObject.GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
