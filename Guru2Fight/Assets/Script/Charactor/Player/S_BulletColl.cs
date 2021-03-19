using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BulletColl : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "StageWall")
        {
            Destroy(this.gameObject);
        }
    }
}
