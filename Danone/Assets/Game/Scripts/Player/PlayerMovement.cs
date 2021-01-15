using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public delegate void ChangeState();
    public static ChangeState OnChangeState;

    public enum StatePlayer { Idle,Walk}
    public StatePlayer statePlayer = new StatePlayer();
    [SerializeField]
    float speed;
    float f_normalSpeed;

    public Transform[] possibleMovement = new Transform[3];
    [SerializeField]
    int indexPosition;
    Vector3 velocity = Vector3.zero;
    bool walking = false;

    float timingMovment = 0;
    float timingIdle = 0;
    public GameObject tierdFace;
    public bool tierd = false;

    private void Start()
    {
        statePlayer = StatePlayer.Idle;
        indexPosition = 0;
        f_normalSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        moveToPosition();
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            upPosition();
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            downPosition();
        }

    }

    void moveToPosition()
    {
        if(Vector2.Distance(transform.position,possibleMovement[indexPosition].position)>0.1f)
        {
            timingMovment += Time.deltaTime;
            timingIdle = 0;
            walking = true;
            transform.position = Vector3.SmoothDamp(transform.position, possibleMovement[indexPosition].position, ref velocity, .3f, speed);
        }
        else
        {
            timingMovment = 0;
            timingIdle += Time.deltaTime;
            if(timingIdle>=2 && tierd)
            {
                tierdFace.SetActive(false);
                tierd = false;
                speed = f_normalSpeed;
            }

            walking = false;
        }
        changeState();

        if(timingMovment>5 && !tierd)
        {
            tierdFace.SetActive(true);
            tierd = true;
            speed = speed * .8f;
        }
    }






    void changeState()
    {
        if(walking && statePlayer!=StatePlayer.Walk)
        {
            statePlayer = StatePlayer.Walk;
            OnChangeState();
        }
        else
        {
            if(!walking && statePlayer!=StatePlayer.Idle)
            {
                statePlayer = StatePlayer.Idle;
                OnChangeState();
            }
        }

    }


    public void speedBost(float multiply)
    {
        speed *= multiply;
    }

    public void normalSpeed()
    {
        speed = f_normalSpeed;
    }

    public void upPosition()
    {
        Debug.Log("Up");
        indexPosition = indexPosition > 0 ? indexPosition-=1 : 0;
    }

    public void downPosition()
    {
        Debug.Log("Down");
        indexPosition = indexPosition < 2 ? indexPosition+=1 : 2;
    }
}
