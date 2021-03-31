using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CustomPartRead : MonoBehaviour
{
    //public.
    public List<string> GachaList = new List<string>();

    //pub sta.
    public static bool OnceGC,TenGC;

    //pri.

    //Local.
    ReadData readData;


    void Start()
    {
        OnceGC = false;
        TenGC = false;
    }

    void Update()
    {
    }

    //ガチャで出た所持パーツのデータを読み込む.
    public void GachaDataRead()
    {
        readData = this.GetComponent<ReadData>();
        readData.ReadList = new List<string>();
        readData.Read_Data(ReadData.GetInternalStoragePath(), "/GachaData.csv");
        GachaList = readData.ReadList;
        if(GachaList.Count == 0)
        {
            var combinedPath = ReadData.GetInternalStoragePath() + "/GachaData.csv";

            //書き込み内容の更新.
            using (var fs = new StreamWriter(combinedPath))
            {
                GachaList = new List<string>()
                { "HaveCheck", "EmissionCount",
                    "1","0","0","0",
                    "0","0","0","0",
                    "0","0","0","0",
                    "0","0","0","0",
                    "0","0","0","0",
                    "0","0","0","0",
                    "0","0",
                    "0 - No have","1 - have"};

                for (int i = 0; i < GachaList.Count; i += 2)
                {
                    //取得フラグと改行カンマを挿入し改行する.
                    fs.WriteLine(GachaList[i] + "," + GachaList[i + 1]);
                }
                fs.Close();
            }
        }
    }

    //リスト変数の内容を上書きで書き込む.
    public void GachaDataWrite()
    {
            var combinedPath = ReadData.GetInternalStoragePath() + "/GachaData.csv";

            //書き込み内容の更新.
            using (var fs = new StreamWriter(combinedPath))
            {
                for (int i = 0; i < GachaList.Count; i+=2)
                {
                    //取得フラグ・取得回数・改行カンマを挿入し改行する.
                    fs.WriteLine(GachaList[i] +"," + GachaList[i + 1]);
                }
                fs.Close();
            }
    }
}