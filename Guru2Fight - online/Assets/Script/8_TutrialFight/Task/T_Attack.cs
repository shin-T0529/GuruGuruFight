using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Attack : MonoBehaviour, ITutorialTask
{
    [SerializeField] private GameObject Player;
    PlayerState playerState;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetTitle()
    {
        return "　Tips：基本操作 【攻撃】";
    }

    public string GetText()
    {
        return " 画面右側にある[Guru!!]ボタン\n を長押しで攻撃をします。\n スキル攻撃はゲーム内で入手後、\n 使うことができます。";
    }

    public void OnTaskSetting()
    {
        Player = GameObject.Find("Player");
        playerState = Player.GetComponent<PlayerState>();
    }

    public bool CheckTask()
    {
        if (playerState.Guru2Attack == true)
        { return true; }
        return false;
    }

    public float GetTransitionTime()
    {
        return 2f;
    }

}
