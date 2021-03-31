using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaCreate : MonoBehaviour
{
    [SerializeField]
    private GameObject RollBar;
    [SerializeField]
    private GameObject CapsleSample;
    int test;
    void Start()
    {
        test = 0;
    }

    // Update is called once per frame
    void Update()
    {
        RollBar.transform.Rotate(new Vector3(0f, 1f, 0f));
        if(test < 18)
        {
            GameObject Gachacapsle = Instantiate(CapsleSample, new Vector3(
                CapsleSample.transform.position.x, CapsleSample.transform.position.y, CapsleSample.transform.position.z), Quaternion.identity);
            test++;
        }

    }
}
