using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayBar : MonoBehaviour
{
    public GameObject barObject;
    public Image barImage;
    public float duration;

    public PlayerMovement character;
    public AttendClient characterAttend;
    bool b_alreadyRestaured = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitEnd());
    }

    IEnumerator waitEnd()
    {
        yield return new WaitForEndOfFrame();
        character = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        characterAttend = GameObject.FindWithTag("Player").GetComponent<AttendClient>();
    }


    public void fullFill()
    {
        barObject.SetActive(true);
        barImage.fillAmount = 1;
        character.speedBost(2f);
        characterAttend.boosting = true;
    }

    private void Update()
    {
        if (barImage.fillAmount > 0)
        {
            b_alreadyRestaured = false;
            barImage.fillAmount -= (Time.deltaTime / duration);
        }
        else
            if (!b_alreadyRestaured && character)
        {
            b_alreadyRestaured = true;
            barObject.SetActive(false);
            character.normalSpeed();
            characterAttend.boosting = false;
        }


    }


}
