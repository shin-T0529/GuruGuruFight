﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Timer.T_GameEnd = false;
        Score.S_GameEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}