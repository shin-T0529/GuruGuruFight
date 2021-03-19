using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲーム上のチュートリアルを管理するマネージャクラス
/// </summary>
public class T_Controll : MonoBehaviour
{

    public GameObject T_TextArea;

    public Text T_Title,T_Text,WindowSize;

    Vector3 testst,t_stst;

    //チュートリアル関連の処理を行うための判定フラグ.
    public static bool TutorialCheck;

    // チュートリアル用UI
    protected RectTransform tutorialTextArea;
    protected Text TutorialTitle;
    protected Text TutorialText;

    // チュートリアルタスク
    protected ITutorialTask currentTask;
    protected List<ITutorialTask> tutorialTask;

    // チュートリアル表示フラグ
    private bool isEnabled;

    // チュートリアルタスクの条件を満たした際の遷移用フラグ
    private bool task_executed = false;

    // チュートリアル表示時のUI移動距離
    //エディター上では４００、実機ではバラバラになる.
    private float fade_pos_x = 400;

    void Start()
    {
        testst = T_TextArea.transform.position;
        WindowSize.text = testst.x.ToString();

        TutorialCheck = false;
        // チュートリアル表示用UIのインスタンス取得
        tutorialTextArea = T_TextArea.GetComponent<RectTransform>();
        TutorialTitle = T_Title.GetComponent<Text>();
        TutorialText = T_Text.GetComponentInChildren<Text>();

        // チュートリアルの一覧
        tutorialTask = new List<ITutorialTask>()
        {
            new T_Move(),
            new T_Attack(),
            new T_Camera(),
            new T_Battle(),
            new T_End()
        };

        // 最初のチュートリアルを設定
        StartCoroutine(SetCurrentTask(tutorialTask.First()));

        TutorialCheck = true;
        isEnabled = true;

        if (1020f > testst.x)
        {
            testst.x = testst.x - 1020f;
            fade_pos_x = fade_pos_x + testst.x / 3;
        }
        else if (1020f < testst.x)
        {
            testst.x = 1020f - testst.x;
            fade_pos_x = fade_pos_x - testst.x / 3;
        }

    }

    void Update()
    {
        testst = T_TextArea.transform.position;
        WindowSize.text = testst.x.ToString();

        // チュートリアルが存在し実行されていない場合に処理
        if (currentTask != null && !task_executed)
        {
            // 現在のチュートリアルが実行されたか判定
            if (currentTask.CheckTask())
            {
                task_executed = true;

                DOVirtual.DelayedCall(currentTask.GetTransitionTime(), () => {
                    iTween.MoveTo(tutorialTextArea.gameObject, iTween.Hash(
                        "position", tutorialTextArea.transform.position + new Vector3(fade_pos_x, 0, 0),
                        "time", 1f
                    ));

                    tutorialTask.RemoveAt(0);

                    var nextTask = tutorialTask.FirstOrDefault();
                    if (nextTask != null)
                    {
                        StartCoroutine(SetCurrentTask(nextTask, 1f));
                    }
                });
            }
        }
    }

    /// 新しいチュートリアルタスクを設定する
    protected IEnumerator SetCurrentTask(ITutorialTask task, float time = 0)
    {
        // timeが指定されている場合は待機
        yield return new WaitForSeconds(time);

        currentTask = task;
        task_executed = false;

        // UIにタイトルと説明文を設定
        TutorialTitle.text = task.GetTitle();
        TutorialText.text = task.GetText();

        // チュートリアルタスク設定時用の関数を実行
        task.OnTaskSetting();

        iTween.MoveTo(tutorialTextArea.gameObject, iTween.Hash(
            "position", tutorialTextArea.transform.position - new Vector3(fade_pos_x, 0, 0),
            "time", 1f
        ));
    }

    /// チュートリアルの有効・無効の切り替え
    protected void SwitchEnabled()
    {
        isEnabled = !isEnabled;

        // UIの表示切り替え
        float alpha = isEnabled ? 1f : 0;
        tutorialTextArea.GetComponent<CanvasGroup>().alpha = alpha;
    }
}