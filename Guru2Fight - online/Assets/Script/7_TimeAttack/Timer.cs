using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //pub.
    public Text TimeCount;
    public Text KillCount;
    //pri.

    //pub sta.
    public static bool T_GameEnd;
    public static bool KillCnt;
    public static int EnemyCount;

    //Local.
    int TimeCounter,TimeM,TimeS;

    void Start()
    {
        TimeCounter = 0;
        TimeM = 0;
        TimeS = 0;
        T_GameEnd = false;
        EnemyCount = 5;
        ButtonProc.BattleType = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (T_GameEnd == false)
        {
            TimeCounter++;
            if (100 <= TimeCounter)
            {
                TimeS++;
                TimeCounter = 0;
            }
            if (60 <= TimeS)
            {
                TimeM++;
                TimeS = 0;
            }

            TimeCount.text = TimeM + "." + TimeS;
        }
        if(KillCnt == true)
        {
            EnemyCount = EnemyCount - 1;
            KillCnt = false;
        }
        KillCount.text = "目的:\n　　あと" + EnemyCount.ToString() + "体倒せ！";
    }
}
