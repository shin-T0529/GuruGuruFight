using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetHaveCoin : MonoBehaviour
{
    [SerializeField]
    private Text CoinText;   


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CoinText.text = "G-Coin : " + GCoinProc.PossGCoin;
    }
}
