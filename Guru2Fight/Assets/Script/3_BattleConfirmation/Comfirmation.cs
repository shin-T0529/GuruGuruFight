using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Comfirmation : MonoBehaviour
{
    public Text WeponName, Coloring, WeponSkill;
    void Start()
    {
        //ホームで外部データを読み込んでおく.
        //ここで装備・色・スキル情報を読み取り代入する.
    }

    void Update()
    {
        WeponName.text = "ぐるぐる";
        Coloring.text = "デフォルト";
        WeponSkill.text = "None";
    }
}
