using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class BGMMusic : MonoBehaviour
{
    //public.
    public GameObject BGMObject;
    public int BGMSetNo;

    //pub sta.
    public static TextAsset csv;

    //pri.
    private int BGMListMax = 8;

    //Local.
    int CreateControll;
    int CreateContScene;
    string SetBGMPath = "Sound/BGM/";       //リソースフォルダの読込先.
    ReadData readData;

    void Start()
    {
        readData = this.GetComponent<ReadData>();

        BGM_ReadPattern();

        ////シーン番号を取得.
        BGMSetNo = FadeProc.FadeJumpScene;

        //再生オブジェクトの生成(マルチ以外なら流す).
        if (FadeProc.FadeJumpScene != 8)
        {
            CreateBGM(BGMSetNo);
        }
    }

    void Update()
    {
        CreateContScene = SceneManager.GetActiveScene().buildIndex;

        if(CreateContScene == 8)
        {
            if (Matching.MatchEnd == true && CreateControll == 0 && FadeProc.FadeJumpScene == 8)
            {
                CreateControll = 1;
                CreateBGM(BGMSetNo);
            }
        }
    }

    //プラットフォームに合わせて読み込み方を変える.
    void BGM_ReadPattern()
    {
#if UNITY_ANDROID
        string asset_name = "bgmfilename"; //読み込みたいAsset Bundleのname
        string bundleUrl = Path.Combine(Application.streamingAssetsPath, asset_name); //Asset BundleのPath

        Debug.Log(asset_name +"の読み込みを開始します");

        if (csv == null) //複数読み込み回避
        {
            AssetBundle assetBundle = 
                AssetBundle.LoadFromFile(bundleUrl); //questiontextのAsset Bundleを読み込む 
            csv = assetBundle.LoadAsset<TextAsset>("bgmfilename"); //assetBundle内の外部ファイルを読み込む
        }
        string test = csv.text;
        string[] values = test.Split('\n');
        readData.ReadList.AddRange(values);

        for (int i = 0; i < BGMListMax; i++) 
        {
            Debug.Log(readData.ReadList[i]);
        }

#else
        readData.Read_Data("", @"Assets/BGMFileName.csv");
        for (int i = 0; i < BGMListMax; i++)
        {
            Debug.Log(readData.ReadList[i]);
        }

#endif //終了
    }

    //BGM再生オブジェクトの生成.
    void CreateBGM(int SetNo)
    {
        GameObject obj = Instantiate(BGMObject, new Vector3(Random.Range(1, 100), Random.Range(7, 10), Random.Range(1, 100)), Quaternion.identity) as GameObject;
        AudioSource source = obj.GetComponent<AudioSource>();
        //Resources.LoadAllですべてのサウンドを取得 ()内はファルダ名
        AudioClip[] clips = Resources.LoadAll<AudioClip>(SetBGMPath);
        source.clip = clips[SetNo];//clips[Random.Range(0, clips.Length)];
        source.Play();
    }
}
