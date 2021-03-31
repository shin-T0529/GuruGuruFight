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

    void DisColorList(GameObject ChangeObj, int SetColorNo)
    {
        switch (SetColorNo)
        {
            case 0:
                ChangeObj.GetComponent<Renderer>().material.color = Color.black;
                break;
            case 1:
                ChangeObj.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case 2:
                ChangeObj.GetComponent<Renderer>().material.color = Color.cyan;
                break;
            case 3:
                ChangeObj.GetComponent<Renderer>().material.color = Color.green;
                break;
            case 4:
                ChangeObj.GetComponent<Renderer>().material.color = Color.magenta;
                break;
            case 5:
                ChangeObj.GetComponent<Renderer>().material.color = Color.red;
                break;
            case 6:
                ChangeObj.GetComponent<Renderer>().material.color = Color.white;
                break;
            case 7:
                ChangeObj.GetComponent<Renderer>().material.color = Color.yellow;
                break;

            default:
                break;
        }
    }
}
