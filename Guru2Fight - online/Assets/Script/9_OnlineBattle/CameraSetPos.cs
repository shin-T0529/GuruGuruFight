using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetPos : MonoBehaviour
{
    public GameObject cam;
    public GameObject TargetObject;
    public string GetObjectName;

    void Start()
    {
        
    }

    void Update()
    {
        if(TargetObject == null)
        {
            TargetObject = GameObject.Find(GetObjectName);

            cam.transform.position = new Vector3(
                 TargetObject.transform.position.x,
                 cam.transform.position.y - TargetObject.transform.position.y,
                 -20f - TargetObject.transform.position.z);
        }
    }
}
