using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InforMation : MonoBehaviour
{
    //pub.
    public Text WeponName, ColorName, SkillName;

    //pri.

    //pub sta.

    //Local.


    void Start()
    {
    }

    void Update()
    {
        SetCustomData(CharaCustom.WeponNo, CharaCustom.ColorNo, CharaCustom.SkillNo);
    }

    void SetCustomData(int WNo,int CNo,int SNo)
    {
        switch(WNo)
        {
            case 0:
                WeponName.text = "X";
                break;
            case 1:
                WeponName.text = "Y";
                break;
            case 2:
                WeponName.text = "Z";
                break;
            default:
                break;
        }

        switch (CNo)
        {
            case 0:
                ColorName.text = "Black";
                break;
            case 1:
                ColorName.text = "Blue";
                break;
            case 2:
                ColorName.text = "Cyan";
                break;
            case 3:
                ColorName.text = "Green";
                break;
            case 4:
                ColorName.text = "Magenta";
                break;
            case 5:
                ColorName.text = "Red";
                break;
            case 6:
                ColorName.text = "White";
                break;
            case 7:
                ColorName.text = "Yellow";
                break;
            default:
                break;
        }

        switch (SNo)
        {
            case 0:
                SkillName.text = "None";
                break;
            case 1:
                SkillName.text = "Heal";
                break;
            case 2:
                SkillName.text = "DefUP";
                break;
            case 3:
                SkillName.text = "Throwing Attack";
                break;
            default:
                break;
        }

    }
}
