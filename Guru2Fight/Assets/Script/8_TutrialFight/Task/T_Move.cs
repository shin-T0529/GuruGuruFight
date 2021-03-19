using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Move : MonoBehaviour, ITutorialTask
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     ここからタスク内容の記録.
     */

    public string GetTitle()
    {
        return "　Tips：基本操作 【移動】";
    }

    public string GetText()
    {
        return " 画面左にあるスティックを\n 動かしたい方向に倒すことで\n キャラクターを動かします";
    }


    public void OnTaskSetting()
    {

    }

    public bool CheckTask()
    {
        if(JoyStick.TutorialCheck == true)
        {
            return true;
        }
        return false;
    }

    public float GetTransitionTime()
    {
        return 2f;
    }
}
