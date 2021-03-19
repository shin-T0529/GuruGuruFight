using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    //pub.
    public Text ScoreCount_T;

    //pri.

    //pub sta.
    public static int ScoreCount;
    public static bool S_GameEnd;
    //Local.

    void Start()
    {
        ScoreCount = 0;     //切り替わる前にスコアを記録するようにする.
        ButtonProc.BattleType = 1;
        S_GameEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreCount_T.text = "Score： " + ScoreCount;
    }
}
