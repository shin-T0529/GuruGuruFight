using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorDisPlay : MonoBehaviour
{
    /*タイトルやホーム画面でモデル飾る用*/
    //pub.
    public GameObject PlayerObject;
    public GameObject GuruArmX;
    public GameObject GuruArmY;

    //pri.

    //pub sta.

    //Local.



    void Start()
    {
        
    }

    void Update()
    {
        PlayerObject.transform.Rotate(new Vector3(0, 0.5f, 0));

        switch (CharaCustom.WeponNo)
        {
            case 0: //デフォルト：ぐるぐるパンチ.
                GuruArmX.SetActive(true);
                GuruArmY.SetActive(false);
                break;
            case 1: //ダブラリ.
                GuruArmX.SetActive(false);
                GuruArmY.SetActive(true);
                break;
            case 2:
                break;
            default:
                break;
        }

    }
}
