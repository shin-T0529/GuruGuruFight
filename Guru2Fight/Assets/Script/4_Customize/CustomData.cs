using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CustomData : MonoBehaviour
{   //ここでは数値の読み込み、代入を行う.
    //数値の変更は別の部分.

    //public.

    //pub sta.
    public static TextAsset csv;

    //pri.

    //Local.
    ReadData readData;

    void Start()
    {
        readData = this.GetComponent<ReadData>();
        Custom_ReadPattern();                       //読込.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //プラットフォームに合わせて読み込み方を変える.
    void Custom_ReadPattern()
    {
#if UNITY_ANDROID
        string asset_name = "CustomDataFile"; //読み込みたいAsset Bundleのname
        string bundleUrl = Path.Combine(Application.streamingAssetsPath, asset_name); //Asset BundleのPath

        if (csv == null) //複数回読み込み回避
        {
            AssetBundle assetBundle = 
                AssetBundle.LoadFromFile(bundleUrl); //questiontextのAsset Bundleを読み込む 
            csv = assetBundle.LoadAsset<TextAsset>("bgmfilename"); //assetBundle内の外部ファイルを読み込む
        }
        string inData = csv.text;
        string[] values = inData.Split('\n');
        readData.ReadList.AddRange(values);

#else
        //readData.ReadRecordData("", @"Assets/BGMFileName.csv");
        //Debug.Log(readData.ReadList[0]);
        //Debug.Log(readData.ReadList[1]);

#endif //終了
    }

}
