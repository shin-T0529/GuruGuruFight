using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_End : MonoBehaviour, ITutorialTask
{
    public static bool T_EndCheck;
    GameObject CreEnemy;
    CharaStateProc charaState;
    int RebornCnt;
    void Start()
    {
        T_EndCheck = false;
        RebornCnt = 0;
    }

    void Update()
    {

    }

    /*
     ここからタスク内容の記録.
     */

    public string GetTitle()
    {
        return "　Tips：自由に動かしてみよう";
    }

    public string GetText()
    {
        return "  今までの操作を確認しましょう。\n 終了するにはボタンを押してください。";
    }


    public void OnTaskSetting()
    {
        //チュートリアル用にオブジェクトを取得する.
        GameObject PracEnemy = GameObject.Find("TutorialEnemy");
        GameObject EnemyHP = GameObject.Find("TutorialEnemyHP");
        GameObject canvas = GameObject.Find("WorldCanvas");

        GameObject EndButton = GameObject.Find("EndButton");
        EndButton.SetActive(true);


        //追尾させるHPをプレハブから自動生成.
        GameObject CreEnemy_HP = (GameObject)Instantiate(EnemyHP);
        CreEnemy_HP.transform.SetParent(canvas.transform, false);

        //敵の情報を取得して作りたいため上限を決めて敵を生成する(座標は今適当).
        CreEnemy = Instantiate(PracEnemy, new Vector3(2.5f, 1.0f, 4.68f), Quaternion.Euler(0f, 180f, 0f));
        //元を動かしたくないため非表示にしていたのを元に戻す.
        CreEnemy.SetActive(true);

        //HPの減少・追尾を行うためプレハブ内の画像を参照。代入させる.
        charaState = CreEnemy.GetComponent<CharaStateProc>();            //追尾用.
        EnemyState state = CreEnemy.GetComponent<EnemyState>();                         //追尾用.
        CharaStateProc inHPBar = CreEnemy_HP.GetComponentInChildren<CharaStateProc>();  //減少用.
                                                                                        //HPはプレハブで生成したのを個別に割り当てる.
        state.Enemy_HP = CreEnemy_HP;                //追尾用.
        charaState.HitPoint = inHPBar.HitPoint;      //減少用.

    }

    public bool CheckTask()
    {
        //敵が倒されたら.
        if (charaState.Dead == true)
        {
            RebornCnt++;
            if(500 < RebornCnt)
            {
                OnTaskSetting();
                RebornCnt = 0;
            }
        }
        return false;
    }

    public float GetTransitionTime()
    {
        return 2f;
    }
}
