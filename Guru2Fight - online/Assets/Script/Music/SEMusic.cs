using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SEMusic : MonoBehaviour
{
    //public.
    public GameObject BGMObject;
    public int PlaySENo;
    //pub sta.
    public static int BGMSetNo;
    public static TextAsset SEcsv;
    //public static List<string> testList = new List<string>();
    public static bool orensyu;

    //pri.

    //Local.
    int PlayCnt;
    string SetSEPath = "Sound/SE/";       //リソースフォルダの読込先.
    ReadData readData;
    AudioSource source;

    void Start()
    {
        PlayCnt = 0;
        source = BGMObject.GetComponent<AudioSource>();
        readData = this.GetComponent<ReadData>();
        SE_ReadPattern();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SEPlay(ref bool Flag,int PlayNo)
    {
        if (Flag == true && PlayCnt ==0)
        {
            AudioClip[] clips = Resources.LoadAll<AudioClip>(SetSEPath);
            source.clip = clips[PlayNo];//clips[Random.Range(0, clips.Length)];
            source.Play();
        }
    }
    void SE_ReadPattern()
    {
#if UNITY_ANDROID
        string asset_name = "sefilename"; //読み込みたいAsset Bundleのname
        string bundleUrl = Path.Combine(Application.streamingAssetsPath, asset_name); //Asset BundleのPath
 
        Debug.Log(asset_name +"の読み込みを開始します");
        readData.ReadList = new List<string>();
        if (SEcsv == null) //複数読み込み回避
        {
            AssetBundle assetBundle = 
                AssetBundle.LoadFromFile(bundleUrl); //questiontextのAsset Bundleを読み込む 
            SEcsv = assetBundle.LoadAsset<TextAsset>("sefilename"); //assetBundle内の外部ファイルを読み込む

        }
        string test = SEcsv.text;
        string[] values = test.Split('\n');
        //testList.AddRange(values);
        readData.ReadList.AddRange(values);

        for (int i = 0; i < readData.ReadList.Count; i++) 
        {
            Debug.Log(readData.ReadList[i]);
        }

//#elif UNITY_IPHONE
//        //端末がiPhoneだった場合の処理
//#elif UNITY_EDITOR
//        //Unity上での処理
#else
        //readData.Read_Data("", @"Assets/SEFileName.csv");
        //for (int i = 0; i < SEListMax; i++) 
        //{
        //    Debug.Log(readData.ReadList[i]);
        //}

#endif //終了
    }

}
