﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        MainManager.Instance.AddToScore();
    }
}
