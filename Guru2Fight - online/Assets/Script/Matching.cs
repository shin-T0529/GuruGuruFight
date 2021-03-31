using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Matching : MonoBehaviourPunCallbacks
{
    public static int PlayerCount = 0;
    public static bool MatchEnd;
    public GameObject CreateInstance;
    public int testNo;

    Vector3 v;

    private void Start()
    {
        //PhotnServerSettingsに設定した内容を使ってマスターサーバーへ接続をする.
        PhotonNetwork.ConnectUsingSettings();

        PhotonNetwork.LeaveRoom(true);
        Debug.Log("抜けました");

        //参加プレイヤー数の初期化.
        PlayerCount = 0;

        //マッチング処理の終了フラグ.
        MatchEnd = false;
    }

    public override void OnConnectedToMaster()
    {
        //ランダムなルームに参加する.
        PhotonNetwork.JoinRandomRoom();

        //"room"という名前のルームに参加する（ルームがなければ作成してから参加する）.
        //PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
    }

    // ランダムで参加できるルームが存在しないなら、新規でルームを作成する
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // ルームの参加人数を2人に設定する
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    public override void OnJoinedRoom()
    {
        if(PlayerCount == 0)
        {
            v = new Vector3(Random.Range(-3f, 3f), 0f,5f);
        }

        if (PlayerCount == 1)
        {
            v = new Vector3(Random.Range(-3f, 3f), 0f, -5f);
        }
        ButtonProc.BattleType = 3;
        PhotonNetwork.Instantiate(this.CreateInstance.name, v, Quaternion.identity);
    }
}
