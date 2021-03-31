using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSetting : MonoBehaviour
{
    public GameObject Player,Wepon1, Wepon2, Wepon3;
    public GameObject XL,XR,YL,YR,ZL,ZR;
    void Start()
    {
        int w = Random.Range(0, 3);
        switch (w)
        {
            case 0: //デフォルト：ぐるぐるパンチ(X軸).
                Wepon1.SetActive(true);
                Wepon2.SetActive(false);
                Wepon3.SetActive(false);
                break;
            case 1: //ダブラリ(Y軸).
                Wepon1.SetActive(false);
                Wepon2.SetActive(true);
                Wepon3.SetActive(false);
                break;
            case 2:
                Wepon1.SetActive(false);
                Wepon2.SetActive(false);
                Wepon3.SetActive(true);
                break;
            default:
                break;
        }

        int i = Random.Range(0, 8);
        switch (i)
        {
            case 0:
                Player.GetComponent<Renderer>().material.color = Color.black;
                XL.GetComponent<Renderer>().material.color = Color.black;
                XR.GetComponent<Renderer>().material.color = Color.black;
                YL.GetComponent<Renderer>().material.color = Color.black;
                YR.GetComponent<Renderer>().material.color = Color.black;
                ZL.GetComponent<Renderer>().material.color = Color.black;
                ZR.GetComponent<Renderer>().material.color = Color.black;
                break;
            case 1:
                Player.GetComponent<Renderer>().material.color = Color.blue;
                XL.GetComponent<Renderer>().material.color = Color.blue;
                XR.GetComponent<Renderer>().material.color = Color.blue;
                YL.GetComponent<Renderer>().material.color = Color.blue;
                YR.GetComponent<Renderer>().material.color = Color.blue;
                ZL.GetComponent<Renderer>().material.color = Color.blue;
                ZR.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case 2:
                Player.GetComponent<Renderer>().material.color = Color.cyan;
                XL.GetComponent<Renderer>().material.color = Color.cyan;
                XR.GetComponent<Renderer>().material.color = Color.cyan;
                YL.GetComponent<Renderer>().material.color = Color.cyan;
                YR.GetComponent<Renderer>().material.color = Color.cyan;
                ZL.GetComponent<Renderer>().material.color = Color.cyan;
                ZR.GetComponent<Renderer>().material.color = Color.cyan;
                break;
            case 3:
                Player.GetComponent<Renderer>().material.color = Color.green;
                XL.GetComponent<Renderer>().material.color = Color.green;
                XR.GetComponent<Renderer>().material.color = Color.green;
                YL.GetComponent<Renderer>().material.color = Color.green;
                YR.GetComponent<Renderer>().material.color = Color.green;
                ZL.GetComponent<Renderer>().material.color = Color.green;
                ZR.GetComponent<Renderer>().material.color = Color.green;
                break;
            case 4:
                Player.GetComponent<Renderer>().material.color = Color.magenta;
                XL.GetComponent<Renderer>().material.color = Color.magenta;
                XR.GetComponent<Renderer>().material.color = Color.magenta;
                YL.GetComponent<Renderer>().material.color = Color.magenta;
                YR.GetComponent<Renderer>().material.color = Color.magenta;
                ZL.GetComponent<Renderer>().material.color = Color.magenta;
                ZR.GetComponent<Renderer>().material.color = Color.magenta;
                break;
            case 5:
                Player.GetComponent<Renderer>().material.color = Color.red;
                XL.GetComponent<Renderer>().material.color = Color.red;
                XR.GetComponent<Renderer>().material.color = Color.red;
                YL.GetComponent<Renderer>().material.color = Color.red;
                YR.GetComponent<Renderer>().material.color = Color.red;
                ZL.GetComponent<Renderer>().material.color = Color.red;
                ZR.GetComponent<Renderer>().material.color = Color.red;
                break;
            case 6:
                Player.GetComponent<Renderer>().material.color = Color.white;
                XL.GetComponent<Renderer>().material.color = Color.white;
                XR.GetComponent<Renderer>().material.color = Color.white;
                YL.GetComponent<Renderer>().material.color = Color.white;
                YR.GetComponent<Renderer>().material.color = Color.white;
                ZL.GetComponent<Renderer>().material.color = Color.white;
                ZR.GetComponent<Renderer>().material.color = Color.white;
                break;
            case 7:
                Player.GetComponent<Renderer>().material.color = Color.yellow;
                XL.GetComponent<Renderer>().material.color = Color.yellow;
                XR.GetComponent<Renderer>().material.color = Color.yellow;
                YL.GetComponent<Renderer>().material.color = Color.yellow;
                YR.GetComponent<Renderer>().material.color = Color.yellow;
                ZL.GetComponent<Renderer>().material.color = Color.yellow;
                ZR.GetComponent<Renderer>().material.color = Color.yellow;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
