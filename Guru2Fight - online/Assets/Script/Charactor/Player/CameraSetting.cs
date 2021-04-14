using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    //public
    public GameObject targetObj;
    public VariableJoystick joystick;
    public string GetObjectName;

    //private

    //public static
    public static bool CameraT_Check;
    //local
    float x;
    bool test;
    Vector3 targetPos;

    //移動するオブジェクトに追従し、回転するカメラの設定.
    void Start()
    {
        CameraT_Check = false;
    }

    void Update()
    {
        if(GetObjectName == "" && Matching.CliantChack == 0)
        {
            GetObjectName = "Player(Clone)";
        }
        else if(GetObjectName != "")
        {
            Matching.CliantChack = 0;
        }

        if (targetObj == null && Matching.CliantChack == 0)
        {
            targetObj = GameObject.Find(GetObjectName);
            targetPos = targetObj.transform.position;
            Debug.Log("CameraSetting：取得しました！");

        }
        x = joystick.Horizontal;
        // targetの移動量分、自分（カメラ）も移動.
        Camera.main.transform.position += targetObj.transform.position - targetPos;
        targetPos = targetObj.transform.position;

        //回転だけチュートリアルシステムのため制限ををかける.
        if (x != 0.0f)
        {
            CameraT_Check = true;

            // マウスの移動量
            float mouseInputX = x;
            // targetの位置のY軸を中心に、回転（公転）.
            Camera.main.transform.RotateAround(targetPos, Vector3.up, mouseInputX * Time.deltaTime * 100f);
        }
        else
        {
            CameraT_Check = false;
        }

    }
}