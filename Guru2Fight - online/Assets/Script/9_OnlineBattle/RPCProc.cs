using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class RPCProc : MonoBehaviourPunCallbacks
{
    private void Update()
    {
        // マウスクリック毎に、ルーム内のプレイヤー全員にメッセージを送信する
        if (Input.GetMouseButtonDown(0))
        {
            photonView.RPC(nameof(RpcSendMessage), RpcTarget.All, "こんにちは");
        }
    }

    [PunRPC]
    private void RpcSendMessage(string message, PhotonMessageInfo info)
    {
        // メッセージを送信したプレイヤー名も表示する
        Debug.Log($"{info.Sender.NickName}: {message}");
    }
}

/*
メモ：
RPCの仕様として実行したいメソッドを実装したスクリプトはPhotonViewコンポーネントと同じオブジェクトに追加されている必要がある。
子→親というRPC呼び出しはできない。

RPCを実行する対象の指定について
RPCを実行する対象：送信者自身の処理：他プレイヤーの処理：途中参加者の処理
①RpcTarget.All       即座に実行       通信を介して実行    実行されない
②.Others             実行されない      同上                  同上
③.AllBuffered         即座に実行                           実行される
④.OthersBuffered     実行されない                          同上
⑤.AllViaServer       通信を介して実行                    実行されない
⑥.AllBufferedViaServer  同上                               実行される

⑦.MasterClient       即座に実行       通信を介して実行    実行されない

通常は①で良い。RPCを送信するプレイヤー自身は通信を介さずにメソッドが即座に実行されるため、
実行される順番はプレイヤーごとに変わることがある。

RpcTarget.AllViaServerを指定することで、RPCを送信するプレイヤー自身も通信を介してメソッドを実行できる。
ルーム内のプレイヤー全員のRPCが、送信された（サーバーが受信した）順番で実行されることが保証されるようになる。
例えば、レースゲームの先着順位を決めたり、早押しクイズゲームの回答権を与えたりする処理に活用可能。

【特別な引数】
RPCで実行するメソッドの引数の最後に「PhotonMessageInfo」を追加すると、RPCの通信で内部的に使われる、送信者のIDやプレイヤー名などを取得できます。
*/
