using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleRecord : MonoBehaviour
{
    //pub.
    public Text BattleCount;        //ソロ：マルチの回数記録.
    public Text BattleWin;          //勝利数・クリア回数記録.
    public Text TimeRecordText;         //敗北数・クリア失敗回数.
    public Text ScoreRecordText;         //敗北数・クリア失敗回数.

    //pri.

    //pub sta.
    public static int ScoreAttack_Score = 0;
    public static string RecordPlay, RecordWin;
    public static string RecordPlay_M, RecordWin_M;
    public static string RecordTime, RecordScore;    //各種記録保持用.
    //Local.
    ReadData readData;

    void Start()
    {
        readData = this.GetComponent<ReadData>();
        readData.ReadRecordData(ReadData.GetInternalStoragePath(), "/ReadCSV.csv");
        for (int i = 0; i < 6; i++)
        {
            Debug.Log(readData.ReadList[i]);
        }
        //初回起動時の読み込み.
        if (ReadData.ReadCheck == false)
        {
            RecordPlay = readData.ReadList[0];
            RecordPlay_M = readData.ReadList[1];
            RecordWin = readData.ReadList[2];
            RecordWin_M = readData.ReadList[3];

            RecordTime = readData.ReadList[4];
            RecordScore = readData.ReadList[5];
            ReadData.ReadCheck = true;
        }

    }

    void Update()
    {
        BattleCount.text = "出撃回数 ：" + RecordPlay;// + "\n　マルチ：" + RecordPlay_M;
        BattleWin.text = "勝利回数 ：" + RecordWin;// + "\n　マルチ：" + RecordWin_M;

        if(RecordTime == null)
        { TimeRecordText.text = "タイムアタックモード最速タイム：\n" + "　　　" + 0.0; }
        else
        { TimeRecordText.text = "タイムアタックモード最速タイム：\n" + "　　　" + RecordTime; }

        if (RecordScore == null)
        { ScoreRecordText.text = "スコアアタックモード最高スコア：\n" + "　　　" + 0 + " 点"; }
        else
        { ScoreRecordText.text = "スコアアタックモード最高スコア：\n" + "　　　" + RecordScore + " 点"; }

    }
}
