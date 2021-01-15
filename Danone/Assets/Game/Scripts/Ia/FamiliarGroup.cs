using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamiliarGroup : Person
{
    public SpriteRenderer[] angryFaces = new SpriteRenderer[8];
    public SpriteRenderer[] normalFaces = new SpriteRenderer[8];
    int statePlayer = 0;

    public override IEnumerator fixFaceBadMode()
    {
        yield return new WaitForSeconds(1);
        
        for(int i=0;i<angryFaces.Length;i++)
        {
            angryFaces[i].sortingOrder = normalFaces[i].sortingOrder;
        }

    }





    public override void idleAnim()
    {
        animDesactive();
        List<Animator> familyAnimators = new List<Animator>();
        for (int i=0;i<transform.childCount;i++)
        {
            if(transform.GetChild(i).tag=="Client" &&
                transform.GetChild(i).gameObject.activeInHierarchy)
            {
                familyAnimators.Add(transform.GetChild(i).GetComponent<Animator>());
            }
        }

         StartCoroutine(delayAnim(familyAnimators, "idle"));

        
    }


   IEnumerator delayAnim(List<Animator> lanims,string animString)
    {
        yield return new WaitForSeconds(0.1f);
        lanims[0].SetBool(animString, true);
        lanims.RemoveAt(0);

        if (lanims.Count > 0)
            yield return delayAnim(lanims,animString);
    }



    public override void walkAnim()
    {
        animDesactive();
        List<Animator> familyAnimators = new List<Animator>();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "Client" &&
                transform.GetChild(i).gameObject.activeInHierarchy)
            {
                familyAnimators.Add(transform.GetChild(i).GetComponent<Animator>());
            }
        }

        StartCoroutine(delayAnim(familyAnimators, "walk"));
    }

    public override void madAnim()
    {
        animDesactive();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "Client" &&
                transform.GetChild(i).gameObject.activeInHierarchy)
            {
                transform.GetChild(i).GetComponent<Animator>().SetBool("mad", true);
            }
        }
    }

    public override void animDesactive()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "Client" &&
                transform.GetChild(i).gameObject.activeInHierarchy)
            {
                transform.GetChild(i).GetComponent<Animator>().SetBool("mad", false);
                transform.GetChild(i).GetComponent<Animator>().SetBool("walk", false);
                transform.GetChild(i).GetComponent<Animator>().SetBool("idle", false);
            }
        }
    }
}
