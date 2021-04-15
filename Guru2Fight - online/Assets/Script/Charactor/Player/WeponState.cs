using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeponState : MonoBehaviour
{
    //pub.
    public GameObject Wepon;                       

    //pri.

    //pub sta.

    //Local.
    CharaCustom charaCustom;


    void Start()
    {
        charaCustom = this.GetComponent<CharaCustom>();   //スクリプトの取得.
    }

    // Update is called once per frame
    void Update()
    {
        int i = SceneManager.GetActiveScene().buildIndex;

        switch(i)
        {
            case 0:     //タイトル.
                break;
            case 1:     //ホーム.
                charaCustom.SendColor = CharaCustom.ColorNo;
                break;
            case 2:     //ソロプレイ出撃前画面.
                charaCustom.SendColor = CharaCustom.ColorNo;
                break;
            case 3:     //カスタマイズ.
                charaCustom.SendColor = CharaCustom.ColorNo;
                break;
            case 4:     //ショップ.
                charaCustom.SendColor = 0;
                break;
            case 5:     //スコアアタックモード.
                charaCustom.SendColor = CharaCustom.ColorNo;
                break;
            case 6:     //タイムアタックモード.
                charaCustom.SendColor = CharaCustom.ColorNo;
                break;
            case 7:     //チュートリアル.
                charaCustom.SendColor = CharaCustom.ColorNo;
                break;
            case 8:     //ランダムマッチングバトルモード.
                charaCustom.SendColor = PlayerState.SetColor;
                break;

            default:
                break;
        }
        
        //各シーンに応じた色の設定.
        charaCustom.ColorList(Wepon, charaCustom.SendColor);
    }
}
