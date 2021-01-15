using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardToInput : MonoBehaviour
{
    public KeyboardLogic keyboard;

    public void OnClick()
    {
        keyboard.reciveInput(GetComponent<InputField>());
    }


}
