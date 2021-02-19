using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeponState : MonoBehaviour
{
    //pub.
    public GameObject Wepon;                        //空.

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
        charaCustom.ColorList(Wepon, CharaCustom.ColorNo);
    }
}
