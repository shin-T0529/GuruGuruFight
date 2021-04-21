using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFade : MonoBehaviour
{
    //pub.
    public Image FadeImage;
    //pri.

    //pub sta.

    //local.
    int FadeCnt;
    float alpha;

    void Start()
    {
        FadeCnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(FadeCnt != 1)
        {
            alpha -= 0.01f;
            FadeImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, alpha);
            if(alpha < 0f)
            {
                FadeCnt++;
            }
        }
        else
        {
            alpha += 0.01f;
            FadeImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, alpha);
            if (1f < alpha)
            {
                FadeCnt--;
            }
        }
    }
}
