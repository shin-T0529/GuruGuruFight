using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class ReadData : MonoBehaviour
{
    //pub.

    //pub sta.
    public static bool ReadCheck = false;
    public List<string> ReadList = new List<string>();
    public List<string> WriteList = new List<string>();
    //pri.

    //Local.
    string WriteNaiyou;

    void Start()
    {
    }

    void Update()
    {
    }

    //汎用読込.
    public void Read_Data(string FilePath,string FileName)
    {
        ReadList = new List<string>();
        var combinedPath = FilePath + FileName;
        if (File.Exists(combinedPath))
        {//存在するとき.
            Debug.Log(FileName + "の読み込みを開始します。");
            //R_Data(combinedPath);
            // 読み込みたいCSVファイルのパスを指定して開く
            StreamReader sr = new StreamReader(combinedPath);
            {
                // 末尾まで繰り返す
                while (!sr.EndOfStream)
                {
                    // CSVファイルの一行を読み込む
                    string line = sr.ReadLine();
                    // 読み込んだ一行をカンマ毎に分けて配列に格納する
                    string[] values = line.Split(',');
                    // 配列からリストに格納する
                    ReadList.AddRange(values);
                }
                System.Console.ReadKey();
                sr.Close();
            }
        }
    }

    //リストを引数にするのはよろしくないため、書き込みのみ各自で独立させる.
    public void WriteRecordData(string FilePath)
    {
        var combinedPath = FilePath + "/Record.csv";

        //書き込み内容の更新.
        WriteList = new List<string>()
         {  "Solo Play", Record.RecordPlay,
            "Multi Play",Record.RecordPlay_M,
            "Solo Win",Record.RecordWin,
            "Multi Win",Record.RecordWin_M,
            "TimeAttack MostRecord",Record.RecordTime,
            "ScoreAttack MostRecord",Record.RecordScore,
            "have G-Coins",GCoinProc.PossGCoin.ToString(),
            "UserName",Record.UserNameInput,
            "Rating",Record.Rating
         };

        using (var fs = new StreamWriter(combinedPath))
        {
            for(int i= 0; i < WriteList.Count; i += 2)
            {
                fs.WriteLine(WriteList[i] + "," + WriteList[i + 1]);
            }
            fs.Close();
        }
    }
    
    //キャラカスタマイズデータの書き込み.
    public void WriteCharaCustomData(string FilePath)
    {
        var combinedPath = FilePath + "/CharaCustomData.csv";

        //書き込み内容の更新.
        WriteNaiyou = CharaCustom.WeponNo + "\n" + CharaCustom.ColorNo + "\n" + CharaCustom.SkillNo;
        using (var fs = new StreamWriter(combinedPath))
        {
            fs.WriteLine(WriteNaiyou);
            fs.Close();
        }
    }

    //ローカルパス取得用.
    public static string GetInternalStoragePath()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
            using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            using (var getFilesDir = currentActivity.Call<AndroidJavaObject>("getFilesDir"))
            {
                string secureDataPath = getFilesDir.Call<string>("getCanonicalPath");
                return secureDataPath;
            }
#else
        Debug.Log(UnityEngine.Application.persistentDataPath);
        return Application.persistentDataPath;
#endif
    }
}
