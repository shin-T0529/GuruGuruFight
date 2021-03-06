﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class FadeProc : MonoBehaviourPunCallbacks
{
    //pub.
    public Image FadeImage;                             //操作するフェード用暗幕.
    public GameObject FadeCanvas;                       //ON/OFFを切り替える.
    public float FadeSpeed = 0.08f;                     //フェードのデフォルト速度、インスペクターから変更可.

    //pri.
    [SerializeField] public GameObject matchTextObj;
    [SerializeField] public Text matchText;

    //pub sta.
    public static int TimingCheck;
    public static int StartTimingCheck;
    public static int GameStartCnt;

    public static int FadeJumpScene;
    public static bool BattleStart;
    public static bool FadeControll;

    //Local.
    float alfa;                     //A値を操作するための変数.
    bool FadeOnOff;                 //ON(暗→明:true)OFF(明→暗:false).
    bool Fade;
    bool TimingCynk;
    int MatchWaitCount;
    void Start()
    {
        BattleStart = false;
        FadeControll = true;
        Fade = true;        //最初からかけておく.
        FadeOnOff = true;
        alfa = 1.0f;
        FadeImage.color = FadeImage.GetComponent<Image>().color;
        FadeJumpScene = SceneManager.GetActiveScene().buildIndex;
        StartTimingCheck = 0;
        if (FadeJumpScene == 8)
        {
            Matching.MatchEnd = false;
            matchTextObj.SetActive(true);
        }
        else
        {
            Matching.MatchEnd = true;
            matchTextObj.SetActive(false);
        }
    }

    void Update()
    {
        if(FadeControll == true && Matching.MatchEnd == true)
        {
            //暗→明.
            if (Fade == true && FadeOnOff == true)
            {
                FadeImage.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, alfa);
                alfa -= FadeSpeed;
                if(alfa < 0.0f)
                {
                    int i = SceneManager.GetActiveScene().buildIndex;

                    //マルチプレイかそれ以外か.
                    if (i == 8)
                    {
                        //プレイヤーのタイミング同期が終わったらfalseにする.
                        if (TimingCheck == 2)
                        {
                            if(100 < StartTimingCheck)
                            {
                                photonView.RPC(nameof(gameStartTiming), RpcTarget.AllBuffered);
                                if(1 < GameStartCnt)
                                {
                                    FadeCanvas.SetActive(false);
                                    FadeControll = false;
                                    Fade = false;
                                    FadeOnOff = false;
                                    photonView.RPC(nameof(ResetTiming), RpcTarget.AllBuffered);
                                }
                            }
                        }
                        else
                        {
                            if (TimingCynk == false)
                            {
                                //プレイヤーのフェード終了加算（２になったら開始）.
                                photonView.RPC(nameof(StartTiming), RpcTarget.AllBuffered);
                                TimingCynk = true;
                            }
                        }
                    }
                    else
                    {
                        //それ以外.
                        FadeCanvas.SetActive(false);
                        FadeControll = false;
                        Fade = false;
                        FadeOnOff = false;
                    }
                }
            }
            //明→暗.
            else if (Fade == false && FadeOnOff == false)
            {
                matchTextObj.SetActive(false);
                FadeCanvas.SetActive(true);
                FadeImage.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, alfa);
                alfa += FadeSpeed;
                FadeChange(FadeJumpScene);
            }
        }

        //マッチング中にゲームが止まってないことをBGM以外で表す.
        MatchingText();
    }

    void FadeChange(int JumpScene)
    {
        int i = SceneManager.GetActiveScene().buildIndex;
        if (1.1f < alfa)
        {
            alfa = 1.0f;
            if (i == 8)
            {
                ResetRooms();
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.Disconnect();
            }
            SceneManager.LoadScene(JumpScene);
        }
    }

    void MatchingText()
    {
        if(TimingCheck < 2)
        {
            if (Matching.MatchEnd == false)
            {
                MatchWaitCount++;
                if (MatchWaitCount < 30)
                {
                    matchText.text = "マッチングちゅう...";
                }
                else if (31 < MatchWaitCount && MatchWaitCount < 60)
                {
                    matchText.text = "マッチングちゅう....";
                }
                else if (61 < MatchWaitCount && MatchWaitCount < 120)
                {
                    matchText.text = "マッチングちゅう.....";
                }
                else if (121 < MatchWaitCount)
                {
                    MatchWaitCount = 0;
                }
            }

            if (0 < TimingCheck)
            {
                matchText.text = "同期中....";
            }
        }
        else
        {
            matchText.GetComponent<RectTransform>().anchoredPosition = new Vector2(1.5266f,4f);
            matchText.text = "Battle Start！";
            photonView.RPC(nameof(testGameStart), RpcTarget.AllBuffered);
        }
    }

    void ResetRooms()
    {
        Matching.PlayerCount = 0;
        Matching.MatchEnd = false;
        PlayerState.MultiWLCheck = false;
    }

    [PunRPC]
    void gameStartTiming()
    {
        GameStartCnt++;
    }

    [PunRPC]
    void StartTiming()
    {
        TimingCheck++;
    }

    [PunRPC]
    void ResetTiming()
    {
        TimingCheck = 0;
        StartTimingCheck = 0;
    }

    [PunRPC]
    void testGameStart()
    {
        StartTimingCheck++;
    }
}
