using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaCustom : MonoBehaviour
{
    //pub.
    public GameObject test;
    //pub sta.
    public static int WeponNo;      //攻撃用ウェポン.
    public static int ColorNo;      //キャラクターの色.
    public static int BodyNo;       //ステータス変更用の身体.
    public static int SkillNo;      //戦闘中に使える特殊能力.

    public static bool WriteData;
    public static bool ReadFirst;
    //pri.

    //local.
    ReadData readData;


    void Start()
    {
        if(ReadFirst == false)
        {
            readData = test.GetComponent<ReadData>();
            readData.ReadRecordData(ReadData.GetInternalStoragePath(), "/CharaCustomData.csv");
            WeponNo = int.Parse(readData.ReadList[0]);
            ColorNo = int.Parse(readData.ReadList[1]);
            BodyNo = int.Parse(readData.ReadList[2]);
            SkillNo = int.Parse(readData.ReadList[3]);
            ReadFirst = true;
        }

        WriteData = false;
    }

    void Update()
    {
        //readData = this.GetComponent<ReadData>();   //スクリプトの取得.
    }

    public void WeponList(GameObject Wepon1,GameObject Wepon2,
        GameObject Wepon3, int SetWeponNo)
    {
        switch (WeponNo)
        {
            case 0: //デフォルト：ぐるぐるパンチ(X軸).
                Wepon1.SetActive(true);
                Wepon2.SetActive(false);
                Wepon3.SetActive(false);
                break;
            case 1: //ダブラリ(Y軸).
                Wepon1.SetActive(false);
                Wepon2.SetActive(true);
                Wepon3.SetActive(false);
                break;
            case 2:
                Wepon1.SetActive(false);
                Wepon2.SetActive(false);
                Wepon3.SetActive(true);
                break;
            default:
                break;
        }
        WriteCustomData();
    }

    public void ColorList(GameObject ChangeObj,int SetColorNo)
    {
        switch(SetColorNo)
        {
            case 0:
                ChangeObj.GetComponent<Renderer>().material.color = Color.black;
                break;
            case 1:
                ChangeObj.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case 2:
                ChangeObj.GetComponent<Renderer>().material.color = Color.cyan;
                break;
            case 3:
                ChangeObj.GetComponent<Renderer>().material.color = Color.green;
                break;
            case 4:
                ChangeObj.GetComponent<Renderer>().material.color = Color.magenta;
                break;
            case 5:
                ChangeObj.GetComponent<Renderer>().material.color = Color.red;
                break;
            case 6:
                ChangeObj.GetComponent<Renderer>().material.color = Color.white;
                break;
            case 7:
                ChangeObj.GetComponent<Renderer>().material.color = Color.yellow;
                break;

            default:
                break;
        }
        WriteCustomData();
    }

    void BodyList()
    {

    }

    //オブジェクト：スキルのセットNo：スキルゲージ消費量.
    public void SkillList(GameObject ChangeObj,int SetSkillNo,ref float UsePoint)
    {
        //なし：回復：遠距離攻撃の予定
        switch (SetSkillNo)
        {
            case 0:
                //スキルセットなし.
                UsePoint = 0.0f;
                break;
            case 1:
                //回復スキル.
                UsePoint = 0.61f;
                break;
            case 2:
                //防御アップ.
                UsePoint = 0.61f;
                break;
            case 3:
                UsePoint = 0.81f;
                break;
            default:
                break;
        }
        WriteCustomData();
    }

    void WriteCustomData()
    {
        if (WriteData == true)
        {
            readData = test.GetComponent<ReadData>();
            readData.WriteCharaCustomData(ReadData.GetInternalStoragePath());
            WriteData = false;
        }
    }
}
