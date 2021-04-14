using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeponState : MonoBehaviour
{
    //pub.
    public GameObject Wepon;                       

    //pri.

    //pub sta.

    //Local.
    CharaCustom charaCustom;


    void Start()
    {
        charaCustom = this.GetComponent<CharaCustom>();   //スクリプトの取得.
    }

    // Update is called once per frame
    void Update()
    {
        if(ButtonProc.BattleType != 3)
        {
            if (ButtonProc.BattleType == 4)
            {
                charaCustom.SendColor = 0;
            }
            else
            {
                charaCustom.SendColor = CharaCustom.ColorNo;
            }
        }
        else
        {
            charaCustom.SendColor = PlayerState.SetColor;
        }

        charaCustom.ColorList(Wepon, charaCustom.SendColor);
    }
}
