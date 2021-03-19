using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopProc : MonoBehaviour
{
    //pub.
    public int GachaNo;
    public int[] ResultCnt;

    //pri.
    [SerializeField]
    private GameObject ParentObject;
    private GameObject[] ChildObject;

    //Local.
    CustomPartRead customPartRead;
    ReadData readData;



    void Start()
    {
        GetAllChildObject();
        readData = this.GetComponent<ReadData>();

        //別のリストで読込を行う.
        customPartRead = this.GetComponent<CustomPartRead>();
        customPartRead.GachaDataRead();

        //獲得回数の初期化・代入.
        ResultCnt = new int[customPartRead.GachaList.Count];
        for (int i = 3; i < customPartRead.GachaList.Count; i += 2)
        { ResultCnt[i] = int.Parse(customPartRead.GachaList[i]); }
    }

    void Update()
    {
        //抽選処理と演出処理.
        if (CustomPartRead.OnceGC == true && GachaEff.RollImage == true)
        {
            lottery(ref GachaNo);
            ResultDisPlay(GachaNo);
            GCoinProc.PossGCoin -= 500;
            if (GCoinProc.PossGCoin <= 0)
            {
                GCoinProc.PossGCoin = 0;
            }
            readData.WriteRecordData(ReadData.GetInternalStoragePath());
            CustomPartRead.OnceGC = false;
        }

        if (GachaEff.RollImage == false)
        {
            ParentObject.SetActive(false);
            for (int i = 2; i <= 3; i++)
            {
                ChildObject[i].SetActive(false);
            }
        }
    }

    void lottery(ref int SetNo)
    {
        //パーツ抽選.
        SetNo = Random.Range(1, 14);
        haveParts.GachaGacha = SetNo;
        //獲得パーツの割り当て、取得管理.
        customPartRead.GachaList[SetNo * 2] = "1";      //所持状態.
        ResultCnt[SetNo * 2 + 1]++;                     //出た回数.
        customPartRead.GachaList[SetNo * 2 + 1] = ResultCnt[SetNo * 2 + 1].ToString();

        //所持状態の記録.
        customPartRead.GachaDataWrite();

        CustomPartRead.OnceGC = false;
    }

    private void WeponResultCountSet()
    {
        switch(CharaCustom.WeponNo)
        {
            case 0:
                break;
            case 1:
                CharaCustom.WeponResultCnt = ResultCnt[3];
                break;
            case 2:
                CharaCustom.WeponResultCnt = ResultCnt[5];
                break;
            default:
                break;
        }
    }
    private void ResultDisPlay(int SetNo)
    {
        ParentObject.SetActive(true);
        ParentObject.GetComponent<Renderer>().material.color = Color.black;
        switch (SetNo)
        {
            case 0:
                /*
                 1 2 Y Z の武器表示.
                 3 4 5 6 7 8 9 黒以外のカラーリング 変更して表示.
                 10 11 12 回復　防御アップ　攻撃スキル    テキスト表示のみ.
                 */

                break;
            case 1:
                ChildObject[1].SetActive(true);
                break;
            case 2:
                ChildObject[2].SetActive(true);
                break;
            case 3:
                ChildObject[3].SetActive(true);
                break;
            case 4:
                ParentObject.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case 5:
                ParentObject.GetComponent<Renderer>().material.color = Color.cyan;
                break;
            case 6:
                ParentObject.GetComponent<Renderer>().material.color = Color.green;
                break;
            case 7:
                ParentObject.GetComponent<Renderer>().material.color = Color.magenta;
                break;
            case 8:
                ParentObject.GetComponent<Renderer>().material.color = Color.red;
                break;
            case 9:
                ParentObject.GetComponent<Renderer>().material.color = Color.white;
                break;
            case 10:
                ParentObject.GetComponent<Renderer>().material.color = Color.yellow;
                break;
            //スキルは全部テキストのみ.
            case 11:
                ParentObject.SetActive(false);
                break;
            case 12:
                ParentObject.SetActive(false);
                break;
            case 13:
                ParentObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    private void GetAllChildObject()
    {
        ChildObject = new GameObject[ParentObject.transform.childCount];

        for (int i = 0; i < ParentObject.transform.childCount; i++)
        {
            ChildObject[i] = ParentObject.transform.GetChild(i).gameObject;
        }
    }
}
