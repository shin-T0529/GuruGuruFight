using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    /************キャラクター関連のUIの操作************/
    //pub.
    public string FindName;
    //pri.
    [SerializeField] private GameObject Player;

    //pub sta.

    //Local.
    PlayerState playerState;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /*************攻撃*************/
    public void OnN_Attack()
    {
        Player = GameObject.Find(FindName);
        playerState = Player.GetComponent<PlayerState>();
        playerState.Guru2Attack = true;
    }

    public void OnN_Attack_End()
    {
        Player = GameObject.Find(FindName);
        playerState = Player.GetComponent<PlayerState>();
        playerState.Guru2Attack = false;
    }

    /*************スキル使用*************/
    public void OnS_Use()
    {
        Player = GameObject.Find(FindName);
        playerState = Player.GetComponent<PlayerState>();
        playerState.UseSkill = true;
    }

}
