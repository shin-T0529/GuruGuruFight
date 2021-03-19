using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Record : MonoBehaviour
{
    //pub.
    public Text BattleCount;        //ソロ：マルチの回数記録.
    public Text BattleWin;          //勝利数・クリア回数記録.
    public Text TimeRecordText;     //タイムアタックモード最速記録.
    public Text ScoreRecordText;    //スコアアタックモード最高記録.
    public Text haveGCoinText;          //所持金.


    //pri.
    //初期化記録用リスト.
    //記録内容の項目.
    private List<string> RecordNameList = new List<string>();
    //記録.
    private List<string> RecordCntList = new List<string>();

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
        readData.Read_Data(ReadData.GetInternalStoragePath(), "/Record.csv");

        //読み込めず空だった場合.
        if (readData.ReadList.Count == 0)
        {
            //内容の初期記録を作る.
            var combinedPath = ReadData.GetInternalStoragePath() + "/Record.csv";

            //初期記録の内容のセット.
            RecordNameList = new List<string>()
            { "Solo Play", "Multi Play", "Solo win","Multi win","TimeAttack MostRecord","ScoreAttack MostRecord","have G-Coins" };
            RecordCntList = new List<string>(){ "0", "0", "0","0","1.00","0","0" };

            using (var fs = new StreamWriter(combinedPath))
            {
                //fs.WriteLine("0\n0\n0\n0\n1.0\n0\n");
                for (int i = 0; i < 7; i++)
                {
                    //項目内容と記録する初期数を連結する.
                    fs.WriteLine(RecordNameList[i] + "," + RecordCntList[i]);
                }
            }

            //再読み込みを行い、データを正しく入れなおす.
            StreamReader sr = new StreamReader(combinedPath);
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] values = line.Split(',');
                    readData.ReadList.AddRange(values);
                }
                System.Console.ReadKey();
            }
        }

        //起動時の記録読み込み.
        if (ReadData.ReadCheck == false)
        {
            //マルチは未実装だが数値は確保しておく.
            RecordPlay = readData.ReadList[1];
            RecordPlay_M = readData.ReadList[3];
            RecordWin = readData.ReadList[5];
            RecordWin_M = readData.ReadList[7];

            RecordTime = readData.ReadList[9];
            RecordScore = readData.ReadList[11];
            GCoinProc.PossGCoin = int.Parse(readData.ReadList[13]);

            ReadData.ReadCheck = true;
        }

    }

    void Update()
    {
        BattleCount.text = "出撃回数 ：" + RecordPlay;// + "\n　マルチ：" + RecordPlay_M;
        BattleWin.text = "勝利回数 ：" + RecordWin;// + "\n　マルチ：" + RecordWin_M;
        TimeRecordText.text = "タイムアタックモード最速タイム：" +  RecordTime;
        ScoreRecordText.text = "スコアアタックモード最高スコア：" +  RecordScore + " 点";
        haveGCoinText.text = "所持G-Coin : " + GCoinProc.PossGCoin.ToString();
    }
}
