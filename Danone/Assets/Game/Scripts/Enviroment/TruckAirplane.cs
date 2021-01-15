using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckAirplane : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(delayTruckPass());
    }

    IEnumerator delayTruckPass()
    {
        yield return new WaitForSeconds(Random.Range(35, 40));
        if(Random.Range(0,2)==0)
        {
            anim.SetBool("passL", true);
        }
        else
        {
            anim.SetBool("passR", true);
        }

        yield return delayTruckPass();
    }


    public void offAnim()
    {
        anim.SetBool("passL", false);
        anim.SetBool("passR", false);
    }
}
