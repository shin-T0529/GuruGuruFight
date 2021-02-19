using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeProc : MonoBehaviour
{
    //pub.
    public Image FadeImage;              //操作するフェード用暗幕.
    public GameObject FadeCanvas;   //ON/OFFを切り替える.

    //pri.

    //pub sta.
    public static int FadeJumpScene;
    public static bool BattleStart;
    public static bool test;
    //Local.
    float alfa;                     //A値を操作するための変数.
    bool FadeOnOff;                 //ON(暗→明:true)OFF(明→暗:false).
    bool Fade;
    void Start()
    {
        BattleStart = false;
        test = true;
        Fade = true;        //最初からかけておく.
        FadeOnOff = true;
        alfa = 1.0f;
        FadeImage.color = FadeImage.GetComponent<Image>().color;
        FadeJumpScene = SceneManager.GetActiveScene().buildIndex;

    }

    void Update()
    {
        if(test == true)
        {
            //暗→明.
            if (Fade == true && FadeOnOff == true)
            {
                FadeImage.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, alfa);
                alfa -= 0.05f;
                if(alfa < 0.0f)
                {
                    FadeCanvas.SetActive(false);
                    test = false;
                    Fade = false;
                    FadeOnOff = false;
                }
            }
            //明→暗.
            else if (Fade == false && FadeOnOff == false)
            {
                FadeCanvas.SetActive(true);
                FadeImage.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, alfa);
                alfa += 0.01f;
                FadeChange(FadeJumpScene);
            }
        }

    }

    void FadeChange(int JumpScene)
    {
        if (1.1f < alfa)
        {
            alfa = 1.0f;
            SceneManager.LoadScene(JumpScene);
        }
    }

}
