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
    public GameObject GuruArmZ;

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
                GuruArmZ.SetActive(false);

                break;
            case 1: //ダブラリ.
                GuruArmX.SetActive(false);
                GuruArmY.SetActive(true);
                GuruArmZ.SetActive(false);

                break;
            case 2:
                GuruArmX.SetActive(false);
                GuruArmY.SetActive(false);
                GuruArmZ.SetActive(true);
                break;
            case 3:
                //ガチャリザルト用.
                GuruArmX.SetActive(false);
                GuruArmY.SetActive(false);
                GuruArmZ.SetActive(false);
                break;
            default:
                break;
        }

    }
}
