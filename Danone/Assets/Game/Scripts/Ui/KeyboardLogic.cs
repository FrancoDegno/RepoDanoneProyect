using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class KeyboardLogic : MonoBehaviour
{

    public InputField textInput;
    public string word;

    public void reciveInput(InputField iField)
    {
        GetComponent<Animator>().SetBool("keyboardOn", true);
        textInput = iField;
    }


    public void reciveInputKey(string letter)
    {
        word = textInput.text;

        if (letter != "del")
            word += letter;
        else
        {
            word=word.Remove(word.Length - 1);
        }

        textInput.text = word;
    }


}
