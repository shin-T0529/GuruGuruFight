using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    /************キャラクター関連のUIの操作************/
    //pub.

    //pri.

    //pub sta.

    //Local.


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /*************攻撃*************/
    public void OnN_Attack()
    {
        PlayerState.Guru2Attack = true;
    }

    public void OnN_Attack_End()
    {
        PlayerState.Guru2Attack = false;
    }

    /*************スキル使用*************/
    public void OnS_Attack()
    {
        //Attack.Guru2Skill = true;
    }

    public void OnS_Attack_End()
    {
        //Attack.Guru2Skill = false;
    }

    /*************ガード*************/
    public void OnGurad()
    {
        PlayerState.Gurad = true;
    }

    public void OnGurad_End()
    {
        PlayerState.Gurad = false;
    }

}
