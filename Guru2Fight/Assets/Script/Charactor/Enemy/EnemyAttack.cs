using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //pub.
    public GameObject E_RollArm;
    public GameObject E_Empty;

    //pri.

    //pub sta.

    //Local.
    bool AtkStart;
    float Rollx;
    CharaStateProc charaState;

    void Start()
    {
        AtkStart = false;
        charaState = this.GetComponent<CharaStateProc>();   //スクリプトの取得.
    }

    void Update()
    {
        charaState.Attack(AtkStart, E_RollArm, E_Empty,0);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAtStart"))
        {//敵に攻撃が当たった時.
            Debug.Log("攻撃開始！！");
            AtkStart = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EnemyAtStart"))
        {//敵に攻撃が当たった時.
            Debug.Log("攻撃やめます");
            AtkStart = false;
        }
    }

}
