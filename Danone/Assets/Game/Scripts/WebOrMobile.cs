using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class WebOrMobile : MonoBehaviour
{
    public bool web = true;
    public Text textLabel;
    public static bool mobile = true;

    [DllImport("__Internal")]
    private static extern bool CheckDevice();
    [DllImport("__Internal")]
    private static extern bool CheckOrientation();

    public GameObject webOrMobileObject;

    void Start()
    {
        if (web)
        {
            mobile = CheckDevice();
        }
          else
            mobile = false;
        
        if(mobile)
        {
            webOrMobileObject.SetActive(true);
        }


       // WebOrMobile.desktop = web;
       // print(WebOrMobile.desktop);
    }
}
