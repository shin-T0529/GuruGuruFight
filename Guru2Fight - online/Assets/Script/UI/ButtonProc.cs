using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonProc : MonoBehaviour
{
    //pub.
    public int BattleSet;
    public int SetCont;

    public GameObject MainAnimObject;       //代用もできる.
    //アニメ派生先用.
    public GameObject NextAnimObject1;
    public GameObject NextAnimObject2;
    public GameObject NextAnimObject3;

    /*メニューUI操作*/
    public AnimationClip UIAnim1;
    public AnimationClip UIAnim2;
    public AnimationClip UIAnim3;

    //pri.
    private Animation UIPlay;

    //pub sta.
    public static int BattleType;
    public static int AnimSet;

    //Local.
    string ReadFileName = "/Record.csv";

    SEMusic seMusic;
    ReadData readData;

    void Start()
    {
        SetCont = 0;
        readData = this.GetComponent<ReadData>();
        readData.Read_Data(ReadData.GetInternalStoragePath(), ReadFileName);
    }

    //モード選択前のアニメ再生.
    public void SceneUIAnim(int SetAnimNo)
    {
        AnimSet = SetAnimNo;
        UIPlay = MainAnimObject.gameObject.GetComponent<Animation>();
        UIPlay.clip = UIAnim1;
        UIPlay.Play();
    }

    public void SceneUIAnimReturn()
    {
        UIPlay = MainAnimObject.gameObject.GetComponent<Animation>();
        UIPlay.clip = UIAnim2;
        UIPlay.Play();
    }

    void OverUI()
    {
        if(AnimSet == 0)
        { UIPlay = NextAnimObject1.gameObject.GetComponent<Animation>(); }
        if (AnimSet == 1)
        { UIPlay = NextAnimObject2.gameObject.GetComponent<Animation>(); }
        if (AnimSet == 2)
        { UIPlay = NextAnimObject3.gameObject.GetComponent<Animation>(); }

        UIPlay.clip = UIAnim3;
        UIPlay.Play();
    }

    //SE再生用.
    public void AddPlaySE(int PlaySENo)
    {
        bool Play = true;
        seMusic = this.GetComponent<SEMusic>();   //スクリプトの取得.
        seMusic.SEPlay(ref Play, PlaySENo);
    }

    //シーン変更用ボタン.
    public void ChangeScene(int Jump)
    {
        FadeProc.FadeJumpScene = Jump;
        FadeProc.FadeControll = true;
    }

    //対戦モード選択用.
    public void BattleTypeSet(int Type)
    {
        BattleType = Type;
    }

    public void ChangeBattleScene()
    {
        //押したボタンで対戦内容を変更・情報を記録する.
        switch (BattleType)
        {
            //0以降にモードの割り当てをする.
            case 0:
                break;
            case 1:     //スコアアタック.
                FadeProc.FadeJumpScene = 5;
                break;
            case 2:     //タイムアタック.
                FadeProc.FadeJumpScene = 6;
                break;
            case 3:     //マルチ:ローカル.
                FadeProc.FadeJumpScene = 8;
                break;
            default:
                break;
        }
        FadeProc.FadeControll = true;
    }

    //ひとまず武器タイプ変更.
    public void WeoponChange(int WeponNo)
    {
        if(WeponNo < 3)
        {
            CharaCustom.WeponNo = WeponNo;
            CharaCustom.WriteData = true;
        }
        else
        {   //インデックス外の範囲選択ｴﾗｰ防止用.
            CharaCustom.WeponNo = 0;
            CharaCustom.WriteData = true;
        }
    }

    //ひとまず色変更.
    public void ColorChange(int SetColorNo)
    {
        if (SetColorNo < 8)
        {
            CharaCustom.ColorNo = SetColorNo;
            CharaCustom.WriteData = true;
        }
        else
        {   //インデックス外の範囲選択ｴﾗｰ防止用(変更しない).
            CharaCustom.WriteData = true;
        }
    }

    //ひとまずスキル変更.
    public void SkillChange(int SetSkillNo)
    {
        if (SetSkillNo < 4)
        {
            CharaCustom.SkillNo = SetSkillNo;
            CharaCustom.WriteData = true;
        }
        else
        {   //インデックス外の範囲選択ｴﾗｰ防止用(変更しない).
            CharaCustom.WriteData = true;
        }
    }

    //ユーザーネーム入力関連.
    public void ChangeName()
    {
        MainAnimObject.SetActive(true);
        NextAnimObject2.SetActive(true);
    }

    public void AddName()
    {
        Record.NameChange = true;
        readData.WriteRecordData(ReadData.GetInternalStoragePath());
        NextAnimObject1.SetActive(false);
        NextAnimObject2.SetActive(false);
        MainAnimObject.SetActive(false);
    }


    //ガチャ関連.
    //単発.
    public void OnceGacha(GameObject Window)
    {
        if (500 <= GCoinProc.PossGCoin)
        {
            MainAnimObject.SetActive(true);
            CustomPartRead.OnceGC = true;
        }
        else
        {
            Window.SetActive(true);
        }
    }

    public void MessageClose(GameObject Window)
    {
        Window.SetActive(false);
    }
    //
    public void GachaResultClose()
    {
        GachaEff.RollImage = false;
        MainAnimObject.SetActive(false);
    }

    //エラーウィンドウ表示アニメ後.
    void MessageAnim()
    {
        MainAnimObject.SetActive(true);
    }
}
