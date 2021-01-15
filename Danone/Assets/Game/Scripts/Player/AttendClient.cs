using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttendClient : MonoBehaviour
{
    [SerializeField]
    Image engRotImg;
    public bool boosting = false;
    bool attending = false;
    float timmingAttendClient = 0;
    float countTime = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Client")
        {
            if (boosting)
            {
                collision.GetComponent<Person>().boostAttended(1.5f);
            }
            attending = true;
            collision.GetComponent<Person>().startAttend();
            timmingAttendClient = collision.GetComponent<Person>().timingAttend;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Client")
        {
            if (boosting)
            {
                collision.GetComponent<Person>().stopBoost();
            }
            engRotImg.fillAmount = 1;
            attending = false;
            countTime = 0;
            collision.GetComponent<Person>().stopAttend();
        }
    }

    private void Update()
    {
        if (attending)
        {
            if (boosting)
                countTime += Time.deltaTime * 1.5f;
            else
                countTime += Time.deltaTime;
            engRotImg.fillAmount = 1-(countTime / timmingAttendClient);
        }

    }


}
