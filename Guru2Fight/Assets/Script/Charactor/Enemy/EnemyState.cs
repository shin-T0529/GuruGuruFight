using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState : MonoBehaviour
{
    //pub.
    public GameObject SetEnemy;         //判定処理を行う敵オブジェクト(複数対応).
                                        //共通項のスクリプトもアタッチしておくこと.
    public GameObject Enemy_HP;

    public AnimationClip DeadAnim;

    //pri.
    private Animation EnemyAnim;
    private NavMeshAgent agent;

    //pub sta.

    //Local.
    int StopCheckCnt;
    Vector3 defaultAgentVelocity;
    SEMusic seMusic;
    CharaStateProc charaState;

    void Start()
    {
        StopCheckCnt = 0;
        charaState = SetEnemy.GetComponent<CharaStateProc>();   //スクリプトの取得.
        seMusic = SetEnemy.GetComponent<SEMusic>();   //スクリプトの取得.

        EnemyAnim = this.gameObject.GetComponent<Animation>();

        // 変数"agent"に NavMesh Agent コンポーネントを格納
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        charaState.HPProc(ref charaState.AtkHit);
        charaState.WorldC_HP(ref Enemy_HP,SetEnemy);
        charaState.CharaDeadAnim(EnemyAnim,DeadAnim);

        charaState.CharaNockBack(ref charaState.DamageCnt);
        seMusic.SEPlay(ref charaState.AtkHitSE, 2);
        //seMusic.SEPlay(ref charaState.Dead, 1);

        //ヒットストップ.
        if(agent.isStopped == true)
        {
            StopCheckCnt++;
            if (50 < StopCheckCnt)
            {
                agent.isStopped = false;
                StopCheckCnt = 0;
            }
        }
    }

    void Dead()
    {
        Score.ScoreCount += 200;
        charaState.CharaDead(charaState.Dead, SetEnemy, Enemy_HP);
        if (charaState.Dead == true)
        {
            PlayerState.EnemyKillCount++;
            Timer.KillCnt = true;
            if (ButtonProc.BattleType == 1)
            {
                EnemyCreate.EnemyCreateSetNo--;
            }
        }
    }
    //当たられたら.
    void OnCollisionEnter(Collision collision)
    {
        //プレイヤーの非攻撃時の当て逃げを回避する.
        if (PlayerState.Guru2Attack == true)
        {
            if (collision.gameObject.tag == "Attack")
            {//敵に攻撃が当たった時.
                Debug.Log("当たられた！");
                charaState.AtkHit = true;
                charaState.AtkHitSE = true;
                charaState.MaxDamage = 0.6f;
                //追跡を止める
                agent.isStopped = true;
            }
        }
    }
}
