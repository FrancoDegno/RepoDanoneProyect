﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speedRot;

    void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, speedRot *Time.time);
    }
}
