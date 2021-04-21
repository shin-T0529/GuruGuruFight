using UnityEngine;
using UnityEngine.UI;

public class Battery : MonoBehaviour
{
    //pub.

    //pri.
    [SerializeField]
    private Image BatteryImage;
    [SerializeField]
    private GameObject BatteryCharge;
    [SerializeField]
    private Text BatteryParsent;

    //pub sta.

    //local.
    float level;
    string BatteryState;
    BatteryStatus status;

    void Update()
    {
        // バッテリーの残量 0f ～ 1f.
        level = SystemInfo.batteryLevel * 100;

        // バッテリーの状態
        // Charging    : 充電中
        // Discharging : 放電中
        // NotCharging : 充電していない
        // Full        : 満タン
        status = SystemInfo.batteryStatus;
        BatteryState = $"{status}";

        //充電中かそれ以外か.
        if (BatteryState == "Charging")
        {
            //充電中のマークを表示.
            BatteryCharge.SetActive(true);
        }
        else
        {
            BatteryCharge.SetActive(false);
        }

        //％の適応.
        BatteryImage.fillAmount = SystemInfo.batteryLevel;
        //％に合わせた量を表示する.
        BatteryParsent.text = level + "%";
    }
}