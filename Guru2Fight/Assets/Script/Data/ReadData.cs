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
    public void ReadRecordData(string FilePath,string FileName)
    {
        var combinedPath = FilePath + FileName;
        if (File.Exists(combinedPath))
        {//存在するとき.
            Debug.Log(FileName + "の読み込みを開始します。");
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
            }
        }
        else
        {
            //ちょっと強引かもしれない.
            Debug.Log(combinedPath + "にそのファイルは存在しません。作成します。");
            //ファイルの作成.
            using (var fs = new StreamWriter(combinedPath))
            {
                fs.WriteLine("0\n0\n0\n0\n1.0\n0");
            }

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
            }

        }

    }

    //リストを引数にするのはよろしくないため、書き込みのみ各自で独立させる.
    //バトル記録の書き込み.
    public void WriteRecordData(string FilePath)
    {
        var combinedPath = FilePath + "/ReadCSV.csv";

        //書き込み内容の更新.
        WriteNaiyou = BattleRecord.RecordPlay + "\n" + BattleRecord.RecordPlay_M + "\n" +
            BattleRecord.RecordWin + "\n" + BattleRecord.RecordWin_M + "\n" +
            BattleRecord.RecordTime + "\n" + BattleRecord.RecordScore;
        using (var fs = new StreamWriter(combinedPath))
        {
            fs.WriteLine(WriteNaiyou);
        }
    }
    //キャラカスタマイズデータの書き込み.
    public void WriteCharaCustomData(string FilePath)
    {
        var combinedPath = FilePath + "/CharaCustomData.csv";

        //書き込み内容の更新.
        WriteNaiyou = CharaCustom.WeponNo + "\n" + CharaCustom.ColorNo + "\n" +
                      CharaCustom.BodyNo + "\n" + CharaCustom.SkillNo;
        using (var fs = new StreamWriter(combinedPath))
        {
            fs.WriteLine(WriteNaiyou);
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
