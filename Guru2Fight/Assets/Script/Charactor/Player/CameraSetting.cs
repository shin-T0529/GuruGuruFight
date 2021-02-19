using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    //public
    public GameObject targetObj;
    public VariableJoystick joystick;

    //private

    //public static

    //local
    float x;
    Vector3 targetPos;

    //移動するオブジェクトに追従し、回転するカメラの設定.
    void Start()
    {
        targetPos = targetObj.transform.position;
    }

    void Update()
    {
        x = joystick.Horizontal;
        // targetの移動量分、自分（カメラ）も移動.
        Camera.main.transform.position += targetObj.transform.position - targetPos;
        targetPos = targetObj.transform.position;

        // マウスの移動量
        float mouseInputX = x;
        // targetの位置のY軸を中心に、回転（公転）.
        Camera.main.transform.RotateAround(targetPos, Vector3.up, mouseInputX * Time.deltaTime * 100f);
    }
}