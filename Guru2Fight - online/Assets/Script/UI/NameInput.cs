using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
    //オブジェクトと結びつける
    public InputField inputField;
    public GameObject NameChangeWindow;
    public GameObject SettingTitle;
    void Start()
    {
        inputField = inputField.GetComponent<InputField>();

        //名前が既に入力済みの場合、初期画面を閉じる.
        if (Record.UserNameInput == "")
        {
            NameChangeWindow.SetActive(true);
            SettingTitle.SetActive(true);
        }
        else
        {
            NameChangeWindow.SetActive(false);
        }
    }

    public void InputText()
    {
        //テキストにinputFieldの内容を反映
        Record.UserNameInput = inputField.text;
        //readData.WriteRecordData(ReadData.GetInternalStoragePath());
    }
}
