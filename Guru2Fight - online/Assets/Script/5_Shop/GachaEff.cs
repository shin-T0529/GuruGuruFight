using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaEff : MonoBehaviour
{
    //pub.
    public GameObject Back2Rote;
    public GameObject CloseButton;
    public Text ResultNo;
    //pub sta.
    public static bool RollImage;


    //pri.


    //Local.
    float Roll;
    bool display;
    string inText;


    void Start()
    {
        RollImage = false;
        Roll = 0.1f;
        display = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(RollImage == true)
        {
            CloseButton.SetActive(true);
            Back2Rote.transform.Rotate(new Vector3(0, 0, Roll));
            ResultSet(display);
        }
        else
        {
            CloseButton.SetActive(false);
            display = false;
        }
    }

    //ガチャリザルト表示処理.
    void ResultSet(bool disp)
    {
        if(disp == false)
        {
            ResultSetText(haveParts.GachaGacha);
            disp = true;
        }
    }

    void ResultSetText(int Gacha)
    {
        switch (Gacha)
        {
            case 0:
                //エラーメッセージ表示.
                inText = "このメッセージは\n表示されないはずです。";
                break;
            case 1:
                inText = "武器！\n【Wepon TypeX】";
                break;
            case 2:
                //エラーメッセージ表示.
                inText = "武器！\n【Wepon TypeY】";
                break;
            case 3:
                //エラーメッセージ表示.
                inText = "武器！\n【Wepon TypeZ】";
                break;
            case 4:
                //エラーメッセージ表示.
                inText = "カラー！\n【Blue】";
                break;
            case 5:
                //エラーメッセージ表示.
                inText = "カラー！\n【Cyan】";
                break;
            case 6:
                //エラーメッセージ表示.
                inText = "カラー！\n【Green】";
                break;
            case 7:
                //エラーメッセージ表示.
                inText = "カラー！\n【Magenta】";
                break;
            case 8:
                //エラーメッセージ表示.
                inText = "カラー！\n【Red】";
                break;
            case 9:
                //エラーメッセージ表示.
                inText = "カラー！\n【White】";
                break;
            case 10:
                //エラーメッセージ表示.
                inText = "カラー！\n【Yellow】";
                break;
            case 11:
                //エラーメッセージ表示.
                inText = "スキル！\n【Heal Skill】";
                break;
            case 12:
                //エラーメッセージ表示.
                inText = "スキル！\n【Defense UP Skill】";
                break;
            case 13:
                //エラーメッセージ表示.
                inText = "スキル！\n【Throwing Attack Skill】";
                break;
            default:
                break;

        }
        ResultNo.text = "獲得！：" + inText;
    }
    //ウィンドウ表示アニメ後.
    void GachaResult()
    {
        RollImage = true;
    }

}
