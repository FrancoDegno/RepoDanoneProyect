using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    
    Animator animator;
    PlayerMovement pMov;
    bool hadClient = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        pMov = GetComponent<PlayerMovement>();
    }


    private void OnEnable()
    {
        PlayerMovement.OnChangeState += walkAnim;
        PlayerMovement.OnChangeState += idleAnim;
    }


    private void OnDisable()
    {
        PlayerMovement.OnChangeState -= walkAnim;
        PlayerMovement.OnChangeState -= idleAnim;
    }


    void walkAnim()
    {
        try
        {
            if (pMov.statePlayer == PlayerMovement.StatePlayer.Walk)
            {
                allAnimFalse();
                animator.SetBool("walk", true);
            }
        }
        catch
        {
            catchError();
        }
        
    }

    IEnumerator catchError()
    {
        yield return new WaitForSeconds(0.3f);
        pMov = GetComponent<PlayerMovement>();
        walkAnim();
        
    }

    void idleAnim()
    {
        if(pMov.statePlayer==PlayerMovement.StatePlayer.Idle)
        {
            allAnimFalse();
            animator.SetBool("idle", true);
        }
        
    }

    void interactiveAnim()
    {
        if (hadClient && pMov.statePlayer == PlayerMovement.StatePlayer.Idle)
        {
            allAnimFalse();
            animator.SetBool("interactive", true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Client")
        {
            hadClient = true;
            interactiveAnim();
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag=="Client")
        {
            hadClient = false;
            idleAnim();
        }
    }


    void allAnimFalse()
    {
        animator.SetBool("walk", false);
        animator.SetBool("idle", false);
        animator.SetBool("interactive", false);
    }

    void changeFace()
    {

    }

}
