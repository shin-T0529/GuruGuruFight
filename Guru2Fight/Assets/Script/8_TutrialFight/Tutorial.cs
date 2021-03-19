using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
}

public interface ITutorialTask
{
    //チュートリアルのタイトルを取得する.
    string GetTitle();

    //説明文を取得する.
    string GetText();

    //チュートリアルタスクが設定された際に実行される.
    void OnTaskSetting();

    //タスクが達成されたか判定する.
    bool CheckTask();

    //達成後に次のタスクへ遷移するまでの時間(秒).
    float GetTransitionTime();
}

