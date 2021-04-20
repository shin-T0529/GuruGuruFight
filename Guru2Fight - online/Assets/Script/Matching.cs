using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class Matching : MonoBehaviourPunCallbacks
{
    public static int PlayerCount = 0;
    public static bool MatchEnd;

    public static int CliantChack = 1;
    public static bool MasterCliant;
    public static bool otherPlayerLeave;

    public int testNo;
    public string GameVersion;

    private GameObject CreateInstance;
    [SerializeField]
    private InputField playerName;

    int SetRaderColorNo;
    Vector3 v = Vector3.zero;           //Position.
    Vector3 rr;
    Quaternion r;                       //Rotation.

    GetUserName getUserName;
    CharaStateProc charaStateProc;
    CharaCustom charaCustom;


    private void Start()
    {
        PhotonNetwork.GameVersion = GameVersion;

        //PhotnServerSettingsに設定した内容を使ってマスターサーバーへ接続をする.
        PhotonNetwork.ConnectUsingSettings();

        //部屋に残っていると二周目ができないため退室.
        PhotonNetwork.LeaveRoom(true);
        Debug.Log("前の部屋を抜けました");

        //参加プレイヤー数の初期化.
        PlayerCount = 0;

        //モデル参照文字列用.
        CliantChack = 1;

        //切断とかされたとき.
        otherPlayerLeave = false;

        //マッチング処理の終了フラグ.
        MatchEnd = false;
    }

    public override void OnConnectedToMaster()
    {
        //ランダムなルームに参加する.
        PhotonNetwork.JoinRandomRoom();
    }

    // ランダムで参加できるルームが存在しないなら、新規でルームを作成する.
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // ルームの参加人数を2人に設定する
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    //部屋に入った時.
    public override void OnJoinedRoom()
    {
        getUserName = this.GetComponent<GetUserName>();

        playerName.text = getUserName.SetName;
        ButtonProc.BattleType = 3;

        //　InputFieldに入力した名前を設定
        if (playerName.text != "")
        {
            PhotonNetwork.NickName = playerName.text;
        }
        else
        {
            PhotonNetwork.NickName = "DefaultPlayer";
        }

        if (PhotonNetwork.IsMasterClient)
        {
            MasterCliant = true;
            v = new Vector3(2f, 1.02f, -12f);
            rr = new Vector3(0f, 0f, 0f);
            SetRaderColorNo = 1;
        }
        else
        {
            MasterCliant = false;
            v = new Vector3(2f, 1.02f, 8f);
            rr = new Vector3(0f, 180f, 0f);
            SetRaderColorNo = 2;
        }

        r = Quaternion.Euler(rr);

        CreateInstance = PhotonNetwork.Instantiate("Player", v, r);

        CreateInstance.GetPhotonView().RPC("SetMakerColor", RpcTarget.AllBuffered, SetRaderColorNo);

        CliantChack = 0;
        CreateInstance.GetPhotonView().RPC("SetName", RpcTarget.AllBuffered, PhotonNetwork.NickName);

        charaCustom = CreateInstance.GetComponent<CharaCustom>();
        charaCustom.SendWepon = CharaCustom.WeponNo;
        CreateInstance.GetPhotonView().RPC("SetChangeWepon", RpcTarget.AllBuffered, charaCustom.SendWepon);

        charaCustom.SendColor = CharaCustom.ColorNo;
        CreateInstance.GetPhotonView().RPC("SetChangeColor", RpcTarget.AllBuffered, charaCustom.SendColor);

        charaCustom.SendSkill = CharaCustom.SkillNo;
        CreateInstance.GetPhotonView().RPC("SetChangeSkill", RpcTarget.AllBuffered, charaCustom.SendSkill);

        //自分のやつは表示させない.
        charaStateProc = CreateInstance.GetComponent<CharaStateProc>();
        charaStateProc.OnlineHP.color = new Color(0f, 1f, 0f, 0f);
        charaStateProc.OnlineHPBack.color = new Color(0f, 1f, 0f, 0f);
        CreateInstance.GetPhotonView().RPC("SetHP", RpcTarget.AllBuffered, charaStateProc.HitPoint.fillAmount);

    }

    // 部屋から退室した時.
    public override void OnLeftRoom()
    {
        PhotonNetwork.OfflineMode = false;
        Destroy(CreateInstance);
    }

    // 他のプレイヤーが退室した時.
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //勝敗が付いていない状態で退室されたとき.
        if(MultiPlay.M_GameEnd == false)
        {
            MultiPlay.M_GameEnd = true;
            otherPlayerLeave = true;
        }
    }
}
