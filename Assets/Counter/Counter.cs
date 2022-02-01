using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Text CounterText;
    private GameObject box;

    private int count = 0;

    private void Start()
    {
        box = GameObject.Find("Box");
        count = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        count += 1;
        CounterText.text = "Count : " + count;
        box.GetComponent<MoveBox>().CalculateTarget();
        GameObject.Find("Cannon").GetComponent<CannonController>().NextLevel(count);
    }
}
