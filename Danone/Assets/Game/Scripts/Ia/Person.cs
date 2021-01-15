using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Person : MonoBehaviour
{


    [SerializeField]
    GameObject disapiarEffect;
    [SerializeField]
    SpriteRenderer normalFace;
    [SerializeField]
    SpriteRenderer fixAngryFace;

    public AudioClip[] clipsAnxious = new AudioClip[3];
    public AudioClip[] clipsAngry = new AudioClip[3];

    AudioSource source;

    [SerializeField]
    public int points;
    [Header("Icons")]
    [SerializeField]
    GameObject spriteObject;
    [SerializeField]
    Sprite angryFace;
    [SerializeField]
    Sprite happyFace;
    [SerializeField]
    Sprite akwardFace;
    [SerializeField]
    Image heart;
    [SerializeField]
    public float slownessHearthless;

    public float timingAttend = 1;
    float actualTiming;
    bool attending = false;

    public float speed;
    public Vector3 pointToMov;

    public delegate void PersonDestroy();
    public static event PersonDestroy OnPersonDestroy;

    Vector3 velocity = Vector3.zero;
    Animator anim;
    public bool attended = false;
    int animState = 0;
    float boostAttending = 1;

    enum StateIa {walk,idle,angry }
    StateIa stateIa = new StateIa();

    private void Start()
    {
        stateIa = StateIa.angry;
        if (GetComponent<Animator>())
            anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        actualTiming = timingAttend;
        StartCoroutine(quitEmoticon(5));
        StartCoroutine(fixFaceBadMode());
    }


    public virtual IEnumerator fixFaceBadMode()
    {
        yield return new WaitForSeconds(1);
        if (fixAngryFace != null)
            fixAngryFace.sortingOrder = normalFace.sortingOrder;
    }

    // Update is called once per frame
    void Update()
    {
        if (pointToMov != null)
            mov(pointToMov);
        if (attending)
        {
            actualTiming -= Time.deltaTime * boostAttending;
            if (actualTiming <= 0)
            {
                attendedFinish();
            }
        }


        cheerState();

    }

    public void mov(Vector3 positionToMov)
    {
        if (Mathf.Abs(transform.position.x - positionToMov.x) > 0.1f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, positionToMov, ref velocity, 1, speed);
            if(stateIa!=StateIa.walk)
            {
                stateIa = StateIa.walk;
                walkAnim();
            }

        }
        else
        {
            if(stateIa!=StateIa.idle)
            {
                stateIa = StateIa.idle;
                idleAnim();
            }
            
        }

    }

    public void cheerState()
    {
        heart.fillAmount -= Time.deltaTime / slownessHearthless;
        if (heart.fillAmount < .6f && animState == 0)
        {
            animState = 1;
            spriteObject.GetComponent<SpriteRenderer>().sprite = akwardFace;
            points /= 2;
            source.PlayOneShot(clipsAnxious[Random.Range(0, 3)]);
            spriteObject.SetActive(true);
            StartCoroutine(quitEmoticon(3));
        }

        if (heart.fillAmount < .3f && animState == 1)
        {
            animState = 2;
            spriteObject.GetComponent<SpriteRenderer>().sprite = angryFace;
            source.PlayOneShot(clipsAngry[Random.Range(0, 3)]);
            points = 0;
            spriteObject.SetActive(true);
            StartCoroutine(quitEmoticon(3));
            madAnim();
        }

    }


    IEnumerator quitEmoticon(float timing)
    {
        yield return new WaitForSeconds(timing);
        spriteObject.SetActive(false);
    }

    public virtual void walkAnim()
    {
        animDesactive();
        anim.SetBool("walk", true);
    }

    public virtual void idleAnim()
    {
        animDesactive();
        anim.SetBool("idle", true);
    }

    public virtual void madAnim()
    {
        animDesactive();
        anim.SetBool("mad", true);
    }

    public virtual void animDesactive()
    {
        anim.SetBool("walk", false);
        anim.SetBool("idle", false);
        anim.SetBool("mad", false);
    }


    public void startAttend()
    {
        attending = true;
    }

    public void stopAttend()
    {
        attending = false;
        actualTiming = timingAttend;
    }

    public void boostAttended(float boost)
    {
        boostAttending = boost;
    }

    public void stopBoost()
    {
        boostAttending = 1;
    }


    public void attendedFinish()
    {
        SaveScore.myScore += points;
        attended = true;
        Instantiate(disapiarEffect, this.transform.position+Vector3.up*1.5f, this.transform.rotation);
        OnPersonDestroy();

    }
}
