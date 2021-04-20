using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (GetObjectName == "" && Matching.CliantChack == 0)
        {
            GetObjectName = "Player(Clone)";
        }
        else if (GetObjectName != "")
        {
            Matching.CliantChack = 0;
        }


        if (TargetObject == null && Matching.CliantChack == 0)
        {
            TargetObject = GameObject.Find(GetObjectName);
            int i = SceneManager.GetActiveScene().buildIndex;

            if(i == 8)
            {
                if (Matching.MasterCliant == true)
                {
                    Camera.main.transform.position = new Vector3(2f, 11f, -30f);
                    Camera.main.transform.rotation = Quaternion.Euler(34.684f, 0f, 0f);
                }
                else
                {
                    Camera.main.transform.position = new Vector3(2f, 11f, 27.5f);
                    Camera.main.transform.rotation = Quaternion.Euler(34.684f, 180f, 0f);
                }
            }
            else
            {
                cam.transform.position = new Vector3(
                     TargetObject.transform.position.x,
                     cam.transform.position.y,
                     TargetObject.transform.position.z - 18f);

                Quaternion quaternion = TargetObject.transform.rotation;
                float y = quaternion.eulerAngles.y;
                float z = quaternion.eulerAngles.z;
                cam.transform.Rotate(0f, y, z);
            }


        }

    }
}