using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class UISetting : MonoBehaviourPunCallbacks
{
    private GameObject namePlate;       //名前を表示しているプレート
    private GameObject HpPlate;         //HPを表示しているプレート


    public Text nameText;               //名前を表示するテキスト.
    public Image HPImage;
    public Image RaderMaker;            //クライアント（入室順）で色を変える.

    public float test;
    CharaStateProc charaStateProc;
    void Start()
    {
        test = 1f;
        charaStateProc = this.GetComponent<CharaStateProc>();
        namePlate = nameText.transform.parent.gameObject;
        HpPlate = HPImage.transform.parent.gameObject;
    }

    void Update()
    {

    }
    void LateUpdate()
    {
        namePlate.transform.rotation = Camera.main.transform.rotation;
        HpPlate.transform.rotation = Camera.main.transform.rotation;
    }

    [PunRPC]
    void SetName(string name)
    {
        nameText.text = name;
    }

    [PunRPC]
    void SetHP(float HP)
    {
        //受けたダメージの更新を自分以外行う.
        HPImage.fillAmount = HP;
    }

    [PunRPC]
    void SetMakerColor(int No)
    {
        if(No == 1)
        {
            RaderMaker.color = new Color(0f, 0f, 1f, 1f);
        }
        else
        {
            RaderMaker.color = new Color(0f, 1f, 0f, 1f);
        }
    }
}
