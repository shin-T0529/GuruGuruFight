using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SetCustom : MonoBehaviour
{
    CharaCustom charaCustom;
    void Start()
    {
    }

    [PunRPC]
    void SetWepon(int Set)
    {
        charaCustom = this.GetComponent<CharaCustom>();
        CharaCustom.ColorNo = Set;
    }

    [PunRPC]
    void SetColor(int Set)
    {
        charaCustom = this.GetComponent<CharaCustom>();
        charaCustom.SendColor = Set;
    }

    [PunRPC]
    void SetSkill(int Set)
    {
        charaCustom = this.GetComponent<CharaCustom>();
        charaCustom.SendSkill = Set;
    }

}
