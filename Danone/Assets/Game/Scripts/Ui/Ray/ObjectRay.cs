using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRay : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer mySprite;
    [SerializeField]
    RayBar myRayBar;
    [SerializeField]
    float timeToDisapiar;
    [SerializeField]
    Animator a_animator;


    private void OnMouseDown()
    {
        desactiveObject();
        myRayBar.fullFill();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(desapiar());
    }

    IEnumerator desapiar()
    {
        yield return new WaitForSeconds(timeToDisapiar);
        a_animator.SetBool("fade", true);
    }


    public void desactiveObject()
    {
        mySprite.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

}
