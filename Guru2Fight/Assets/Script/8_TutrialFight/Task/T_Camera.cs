using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Camera : MonoBehaviour, ITutorialTask
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
        return "　Tips：基本操作 【視点移動】";
    }

    public string GetText()
    {
        return " 画面右側を左右にスライドを\nするとフリック方向と逆に\n視点が動きます。";
    }

    public void OnTaskSetting()
    {
    }

    public bool CheckTask()
    {
        if (CameraSetting.CameraT_Check == true)
        { return true; }
        return false;
    }

    public float GetTransitionTime()
    {
        return 2f;
    }
}
