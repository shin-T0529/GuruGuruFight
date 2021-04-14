using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUserName : MonoBehaviour
{
    public string SetName;
    void Start()
    {
        SetName = Record.UserNameInput;
    }

    // Update is called once per frame
    void Update()
    {
        SetName = Record.UserNameInput;
    }
}
