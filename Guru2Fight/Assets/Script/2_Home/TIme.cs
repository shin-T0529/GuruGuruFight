using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TIme : MonoBehaviour
{
    //pub.

    //pri.
    private Text ClockText;

    //pub sta.

    //Local.


    void Start()
    {
        ClockText = GetComponentInChildren<Text>();
    }


    void Update()
    {
        ClockText.text = DateTime.Now.ToShortTimeString();
    }
}
