using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyCreate : MonoBehaviour
{
    //pub.
    public GameObject Enemy;
    public GameObject EnemyHP;
    public GameObject canvas;   //HP表示用キャンバス.

    //pri.

    //pub sta.
    public static int EnemyCreateSetNo;
    //Local.
    int EnemyCnt;
    int CreateDelay;

    void Start()
    {
        EnemyCreateSetNo = 0;
        EnemyCnt = 0;
        CreateDelay = 0;
    }
    //-5.88 ～ 10.83
    void Update()
    {
        switch (ButtonProc.BattleType)
        {
            case 0:
                break;
            case 1:     //スコアアタック時には死ぬまで無限に生成する.
                if (EnemyCreateSetNo < 5 && 300 < CreateDelay && Score.S_GameEnd == false)
                {
                    //追尾させるHPをプレハブから自動生成.
                    GameObject CreEnemy_HP = (GameObject)Instantiate(EnemyHP);
                    CreEnemy_HP.transform.SetParent(canvas.transform, false);

                    //敵の情報を取得して作りたいため上限を決めて敵を生成する(座標は今適当).
                    GameObject CreEnemy = Instantiate(Enemy, new Vector3(0.08999991f, 1.3f, 10.01f), Quaternion.identity);
                    //元を動かしたくないため非表示にしていたのを元に戻す.
                    CreEnemy.SetActive(true);

                    //HPの減少・追尾を行うためプレハブ内の画像を参照。代入させる.
                    CharaStateProc charaState = CreEnemy.GetComponent<CharaStateProc>();            //追尾用.
                    EnemyState state = CreEnemy.GetComponent<EnemyState>();                         //追尾用.
                    CharaStateProc inHPBar = CreEnemy_HP.GetComponentInChildren<CharaStateProc>();  //減少用.
                                                                                                    //HPはプレハブで生成したのを個別に割り当てる.
                    state.Enemy_HP = CreEnemy_HP;                //追尾用.
                    charaState.HitPoint = inHPBar.HitPoint;      //減少用.

                    //各種制御関数をリセットする.
                    EnemyCreateSetNo++;
                    CreateDelay = 0;
                }
                break;
            case 2:
                //10体を倒す速さを競う.
                if (EnemyCnt < 5 && 300 < CreateDelay && Timer.T_GameEnd == false)
                {
                    //追尾させるHPをプレハブから自動生成.
                    GameObject CreEnemy_HP = (GameObject)Instantiate(EnemyHP);
                    CreEnemy_HP.transform.SetParent(canvas.transform, false);

                    //敵の情報を取得して作りたいため上限を決めて敵を生成する(座標は今適当).
                    GameObject CreEnemy = Instantiate(Enemy, new Vector3(0.08999991f, 1.3f, 10.01f), Quaternion.identity);
                    //元を動かしたくないため非表示にしていたのを元に戻す.
                    CreEnemy.SetActive(true);

                    //HPの減少・追尾を行うためプレハブ内の画像を参照。代入させる.
                    CharaStateProc charaState = CreEnemy.GetComponent<CharaStateProc>();            //追尾用.
                    EnemyState state = CreEnemy.GetComponent<EnemyState>();                         //追尾用.
                    CharaStateProc inHPBar = CreEnemy_HP.GetComponentInChildren<CharaStateProc>();  //減少用.
                                                                                                    //HPはプレハブで生成したのを個別に割り当てる.
                    state.Enemy_HP = CreEnemy_HP;                //追尾用.
                    charaState.HitPoint = inHPBar.HitPoint;      //減少用.

                    //各種制御関数をリセットする.
                    EnemyCnt++;
                    CreateDelay = 0;
                }
                break;
            default:
                break;
        }

        CreateDelay++;
    }

    void EnemyCreateSetting(int CreateNo)
    {
        switch(CreateNo)
        {
            //生成地点の調査・調整を行う.
            case 0:
                //左から生成.
                break;
            case 1:
                //右から生成.
                break;
            default:
                break;
        }
    }
}
