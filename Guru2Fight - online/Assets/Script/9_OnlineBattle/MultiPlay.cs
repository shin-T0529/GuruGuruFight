using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiPlay : MonoBehaviour
{
    //pub.
    public Text MultiStatus;

    //pri.

    //pub sta.
    public static bool M_GameEnd;
    //Local.

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(M_GameEnd == false)
        {
            MultiStatus.text = "対戦中... ";
        }
        else
        {
            MultiStatus.text = "対戦終了！！ ";
        }
    }
}
