﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultProc : MonoBehaviour
{
    //pub.
    public GameObject PlayerObject;
    public GameObject TextObjects;
    public GameObject Text_Win;
    public GameObject Text_Lose;

    public GameObject Controll_C, CameraCont_C;     //リザルト表示時に操作されないようにする.

    public Text ScoreCount;
    public Text TimeCount, TimeRecord;
    public Text GetCoin;

public AnimationClip WindowAnim_W;
    public AnimationClip WindowAnim_L;
    public int EnemyDefeat;

    //pri.
    private Animation UIAnim;

    //pub sta.

    //Local.
    int PlayCount,WinCount;                         //ソロ記録用.
    CharaStateProc charaState;
    ReadData readData;

    void Start()
    {
        charaState = PlayerObject.GetComponent<CharaStateProc>();   //スクリプトの取得.
        UIAnim = this.gameObject.GetComponent<Animation>();

        PlayCount = int.Parse(Record.RecordPlay);
        WinCount = int.Parse(Record.RecordWin);
    }

    void Update()
    {
        //バトルモードに応じてリザルトを変更する.
        if (ButtonProc.BattleType == 1)
        {
            ScoreAttackModeResult(ref Score.S_GameEnd);
        }
        else if (ButtonProc.BattleType == 2)
        {
            TimeAttackModeResult(ref Timer.T_GameEnd);
        }
    }

    /*スコアアタック管理用*/
    void ScoreAttackModeResult(ref bool End)
    {
        readData = this.GetComponent<ReadData>();

        if (charaState.Dead == true && End == false)
        {
            Controll_C.SetActive(false);
            CameraCont_C.SetActive(false);
            UIAnim.clip = WindowAnim_W;
            UIAnim.Play();

            //レコード用の記録開始.
            if(int.Parse(Record.RecordScore) < Score.ScoreCount)
            {
                Record.RecordScore = Score.ScoreCount.ToString();
            }
            ScoreCount.text = "Clear Score : " + Score.ScoreCount;

            //ゲーム内通貨の加算.
            GCoinProc.GetGCoin = Score.ScoreCount / 10;
            GCoinProc.PossGCoin = GCoinProc.PossGCoin + GCoinProc.GetGCoin;
            GetCoin.text = "Get G-Coins : "  + GCoinProc.GetGCoin.ToString();

            //プレイ回数の記録.
            PlayCount++;
            Record.RecordPlay = PlayCount.ToString();
            WinCount++;
            Record.RecordWin = WinCount.ToString();
            readData.WriteRecordData(ReadData.GetInternalStoragePath());
            End = true;
        }
    }

    /*タイムアタック管理用*/
    void TimeAttackModeResult(ref bool End)
    {
        readData = GetComponent<ReadData>();

        EnemyDefeat = PlayerState.EnemyKillCount;
        if (5 <= EnemyDefeat && End == false)
        {//クリア時(決められた数を倒せたとき).
            Controll_C.SetActive(false);
            CameraCont_C.SetActive(false);
            UIAnim.clip = WindowAnim_W;
            UIAnim.Play();

            //ゲーム内通貨の加算.
            GCoinProc.GetGCoin = 50;
            GCoinProc.PossGCoin = GCoinProc.PossGCoin + GCoinProc.GetGCoin;
            GetCoin.text = "Get G-Coins : " + GCoinProc.GetGCoin.ToString();

            /****レコード用の記録開始****/
            //プレイ回数の記録.
            PlayCount++;
            Record.RecordPlay = PlayCount.ToString();
            WinCount++;
            Record.RecordWin = WinCount.ToString();
            readData.WriteRecordData(ReadData.GetInternalStoragePath());               //一度回数だけ記録する.

            //クリアタイム更新するかの判定.
            TimeRecord.text = "Clear Time" + TimeCount.text;
            float LastRecord = float.Parse(Record.RecordTime);      //記録比較用.
            float NowTime = float.Parse(TimeCount.text);        //今回の記録.
            //最高記録と比較.
            if (NowTime < LastRecord)
            {
                Record.RecordTime = NowTime.ToString();      //今回の記録を入れる.
                readData.WriteRecordData(ReadData.GetInternalStoragePath());
            }
            //Record.RecordTime = TimeCount.text;

            End = true;
        }
        else if (charaState.Dead == true && End == false)
        {//クリア失敗時(死亡時).
            Controll_C.SetActive(false);
            CameraCont_C.SetActive(false);
            UIAnim.clip = WindowAnim_L;
            UIAnim.Play();
            //プレイ回数の記録.
            PlayCount++;
            Record.RecordPlay = PlayCount.ToString();
            readData.WriteRecordData(ReadData.GetInternalStoragePath());

            End = true;
        }
    }

    /*ウィンドウアニメ操作*/
    void WinAnimEnd()
    {
        TextObjects.SetActive(true);
        Text_Win.SetActive(true);
    }

    void LoseAnimEnd()
    {
        TextObjects.SetActive(true);
        Text_Lose.SetActive(true);
    }

}
