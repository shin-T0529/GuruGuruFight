using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerState : MonoBehaviourPunCallbacks
{
    //pub.
    public GameObject SetPlayer;
    public GameObject Wepon1, Wepon2,Wepon3;
    public GameObject SkillBullet;

    public int test;

    //実際に動かす腕部分.
    public GameObject GuruX_L, GuruX_R;       //ぐるぐるパンチ.
    public GameObject GuruY_L, GuruY_R;       //ダブルラリアット.
    public GameObject GuruZ_L, GuruZ_R;       //Z軸回転.

    public GameObject Empty;                  //空のオブジェクト.

    [SerializeField]
    public GameObject M_ResultObject;         //ここで処理が都合がいい.
    [SerializeField]
    public GameObject M_Vic,M_Lose,M_Return;  //同上.
    [SerializeField]
    public GameObject M_Rating;         //ここで処理が都合がいい.
    public Text M_Ratingtext;
    public AnimationClip DeadAnim;

    public bool Guru2Attack;                 //通常攻撃の管理フラグ.
    public bool Guru2Skill;                  //スキル攻撃の管理フラグ.
    public bool UseSkill;

    //pri.
    private GameObject LArm, RArm;          //表示されている腕を取得.
    private Animation PlayerAnim;
    [SerializeField] private GameObject ShotPos;

    //pub sta.
    public static bool MultiWLCheck;
    public static int SetWepon, SetColor,SetSkill;
    //Local.
    int ResultCnt;
    int c_Wepon, c_Color, c_Skill;

    bool HealCheck;
    string LArmName = "L_Arms";
    string RArmName = "R_Arms";
    SEMusic seMusic;
    CharaStateProc charaState;
    CharaCustom charaCustom;

    void Start()
    {
        ResultCnt = 0;

        HealCheck = false;
        UseSkill = false;
        Guru2Attack = false;

        charaState = this.GetComponent<CharaStateProc>();   //スクリプトの取得.
        seMusic = SetPlayer.GetComponent<SEMusic>();   //スクリプトの取得.
        charaCustom = this.GetComponent<CharaCustom>();   //スクリプトの取得.

        MultiWLCheck = false;

        PlayerAnim = this.gameObject.GetComponent<Animation>();

        //親オブジェクトの取得.
        if (M_ResultObject == null)
        {
            M_ResultObject = GameObject.Find("MultiBattle");

            //非アクティブな子オブジェクトの取得.
            M_Vic = M_ResultObject.transform.Find("m_Victory").gameObject;
            M_Lose = M_ResultObject.transform.Find("m_Lose").gameObject;
            M_Return = M_ResultObject.transform.Find("h_Return").gameObject;
            M_Rating = M_ResultObject.transform.Find("m_Rating").gameObject;
            M_Ratingtext = M_ResultObject.transform.Find("m_Rating").gameObject.GetComponent<Text>();
            M_Vic.SetActive(false);
            M_Lose.SetActive(false);
            M_Return.SetActive(false);
            M_Rating.SetActive(false);
        }

        //すべての初期化が終わってからプレイヤー数のカウントを行う.
        photonView.RPC(nameof(JoinPlayerCount), RpcTarget.All);

        test = ButtonProc.BattleType;       //ここ変える.
        //カスタムデータの適応を行う.
        if (test != 3)
        {
            charaCustom.WeponList(Wepon1, Wepon2, Wepon3, CharaCustom.WeponNo);
            charaCustom.ColorList(SetPlayer, CharaCustom.ColorNo);
            charaCustom.SkillList(SetPlayer, CharaCustom.SkillNo, ref charaState.UseSkillPoint);
        }
        else
        {
            charaCustom.WeponList(Wepon1, Wepon2, Wepon3, c_Wepon);
            charaCustom.ColorList(SetPlayer, c_Color);
            charaCustom.SkillList(SetPlayer, c_Skill, ref charaState.UseSkillPoint);
        }
    }

    void Update()
    {
        if (test != 3)
        {
            SetWepon = CharaCustom.WeponNo;
            SetColor = CharaCustom.ColorNo;

            charaCustom.WeponList(Wepon1, Wepon2, Wepon3, CharaCustom.WeponNo);
            charaCustom.ColorList(SetPlayer, CharaCustom.ColorNo);
            charaCustom.SkillList(SetPlayer, CharaCustom.SkillNo, ref charaState.UseSkillPoint);
            SetUpPlayerWepon(CharaCustom.WeponNo);
        }
        else
        {
            SetWepon = c_Wepon;
            SetColor = c_Color;

            charaCustom.WeponList(Wepon1, Wepon2, Wepon3, c_Wepon);
            charaCustom.ColorList(SetPlayer, c_Color);
            charaCustom.SkillList(SetPlayer, c_Skill, ref charaState.UseSkillPoint);
            SetUpPlayerWepon(c_Wepon);
        }

        if (photonView.IsMine)
        {
            //被弾時処理（HP減少処理：無敵時間の設定）.
            seMusic.SEPlay(ref charaState.AtkHitSE, 1);
            charaState.HPStateGet();
            charaState.HPProc(ref charaState.AtkHit);               //HP減少処理.

            //スキルの処理.
            SkillProc();
            //ノックバック.
            charaState.CharaNockBack(ref charaState.DamageCnt);

            //当たり判定調整セット用.
            //ArmsTag();

            //死亡アニメーション処理.
            charaState.CharaDeadAnim(PlayerAnim, DeadAnim);

            WLCheck();
        }

        if (Matching.PlayerCount == 2)
        {
            photonView.RPC(nameof(BattleStart), RpcTarget.All);
        }
    }

    //装備武器と攻撃処理.
    void SetUpPlayerWepon(int WeponNo)
    {
        switch(WeponNo)
        {
            case 0: //デフォルト：ぐるぐるパンチ.
                charaState.Attack(Guru2Attack, GuruX_L, GuruX_R, WeponNo);   //攻撃処理.
                break;
            case 1: //ダブラリ.
                charaState.Attack(Guru2Attack, GuruY_L, GuruY_R, WeponNo);   //攻撃処理.
                break;
            case 2:
                charaState.Attack(Guru2Attack, GuruZ_L, GuruZ_R, WeponNo);   //攻撃処理.
                break;
            default:
                break;
        }
    }

    void SkillProc()
    {
        charaState.SkillStateGet();

        //スキルSE使用処理.
        if (UseSkill == true && charaState.GuradSkill == false
        && charaState.UseSkillPoint < charaState.CheckSkillPoint)
        {
            seMusic.SEPlay(ref UseSkill, 7);
        }

        if (UseSkill == true && CharaCustom.SkillNo == 1
         && charaState.UseSkillPoint < charaState.CheckSkillPoint)
        {
            seMusic.SEPlay(ref HealCheck, 7);
        }

        if (UseSkill == true && charaState.BulletSkill == false
            && charaState.UseSkillPoint < charaState.CheckSkillPoint)
        { seMusic.SEPlay(ref UseSkill, 7); }

        charaState.SkillProc(ref UseSkill, CharaCustom.SkillNo, ShotPos, SkillBullet);
    }

    //アニメーション再生終了時.
    void Dead()
    {
        charaState.CharaDead(charaState.Dead, SetPlayer, Empty);

        //モードに合わせた終了フラグを立てる.
        if (ButtonProc.BattleType == 1)
        {//スコアアタックモード中、プレイヤーが死んだとき終わらせる.
            Score.S_GameEnd = true;
        }
        else if (ButtonProc.BattleType == 2)
        {//タイムアタックモード中、プレイヤーが死んだとき終わらせる.
         //記録タイムの更新は行わない.
            Timer.T_GameEnd = true;
        }
        else if(ButtonProc.BattleType == 3)
        {
            //上記以外の場合はマルチプレイをしている.
            photonView.RPC(nameof(SetEndFlag), RpcTarget.All);
        }
    }

    void WLCheck()
    {
        if (MultiWLCheck == true && ResultCnt == 0)
        {

            photonView.RPC(nameof(SetResultCheck), RpcTarget.All);
            int rate = int.Parse(Record.Rating);
            M_Rating.SetActive(true);
            if (charaState.Dead == true)
            {
                //勝利敗北テキスト出す.
                Debug.Log("貴方の負けです。");
                M_Lose.SetActive(true);
                rate -= 5;
                Record.Rating = rate.ToString();
                ResultCnt++;
            }
            else
            {
                //勝利テキスト出す.
                Debug.Log("貴方の勝ちです。");
                M_Vic.SetActive(true);
                rate += 10;
                Record.Rating = rate.ToString();
                ResultCnt++;
            }
            M_Ratingtext.text = "Your Rating : " + rate.ToString();
            M_Return.SetActive(true);
            ResultProc.WritingData = true;
        }
    }

    void ArmsTag()
    {
        if (ButtonProc.BattleType != 3)
        {
            OffSetAttackTag(LArm, LArmName);
            OffSetAttackTag(RArm, RArmName);
        }
        else
        {
            photonView.RPC(nameof(OnSetAttackTag), RpcTarget.AllViaServer, LArm, LArmName);
            photonView.RPC(nameof(OnSetAttackTag), RpcTarget.AllViaServer, RArm, RArmName);
        }
    }

    void OffSetAttackTag(GameObject gameObject,string FindName)
    {
        if (gameObject == null)
        {
            gameObject = GameObject.Find(FindName);
        }

        if (Guru2Attack != true)
        {
            gameObject.tag = "NoAttack";
        }
        else
        {
            gameObject.tag = "Attack";
        }
    }

    //オンラインのタグ判定書き換え用.
    [PunRPC]
    void OnSetAttackTag(GameObject gameObject,string Name)
    {
        if (gameObject == null)
        {
            gameObject = GameObject.Find(Name);
        }
        if (Guru2Attack != true)
        {
            gameObject.tag = "NoAttack";
        }
        else
        {
            gameObject.tag = "Attack";
        }
    }

    [PunRPC]
    void ROnSetAttackTag()
    {
        if (RArm == null)
        {
            RArm = GameObject.Find(RArmName);
        }
        if (Guru2Attack != true)
        {
            RArm.tag = "NoAttack";
        }
        else
        {
            RArm.tag = "Attack";
        }
    }




    void OnCollisionEnter(Collision collision)
    {
        if(T_Controll.TutorialCheck != true)
        {
            if (collision.gameObject.tag == "Enemy")
            {//敵に攻撃が当たった時.
                Debug.Log("当たってしまった！！");
                charaState.AtkHit = true;
                charaState.AtkHitSE = true;
                //防御アップのスキル使用時.
                if (charaState.GuradSkill == true)
                { charaState.MaxDamage = 0.02f; }
                else
                { charaState.MaxDamage = 0.04f; }
                charaState.DamageCnt++;
            }

            if (collision.gameObject.tag == "Attack")
            {//敵に攻撃が当たった時.
                Debug.Log("当たってしまった！！");
                charaState.AtkHit = true;
                charaState.AtkHitSE = true;
                //防御アップのスキル使用時.
                if (charaState.GuradSkill == true)
                { charaState.MaxDamage = 0.05f; }
                else
                { charaState.MaxDamage = 0.1f; }
                charaState.DamageCnt++;
            }

        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "SkillBullet")
        {//敵に攻撃が当たった時.
            Debug.Log("スキル！！");
            charaState.AtkHit = true;
            charaState.AtkHitSE = true;
            //防御アップのスキル使用時.
            if (charaState.GuradSkill == true)
            { charaState.MaxDamage = 0.2f; }
            else
            { charaState.MaxDamage = 0.4f; }
            charaState.DamageCnt++;
        }
    }

    //リザルト同期処理.
    [PunRPC]
    void SetEndFlag()
    {
        //マルチの終了通知情報を送信.
        MultiPlay.M_GameEnd = true;
    }

    //リザルト同期処理.s
    [PunRPC]
    void SetResultCheck()
    {
        //マルチのリザルト表示情報を送信し、同期する.
        MultiWLCheck = true;
    }

    //参加プレイヤー数の加算.
    [PunRPC]
    void JoinPlayerCount()
    {
        Matching.PlayerCount++;
    }

    [PunRPC]
    void BattleStart()
    {
        Matching.MatchEnd = true;
    }

    //カスタムデータ設定用.
    [PunRPC]
    void SetChangeWepon(int SetWepon)
    {
        c_Wepon = SetWepon;
    }

    [PunRPC]
    void SetChangeColor(int SetColor)
    {
        c_Color = SetColor;
    }

    [PunRPC]
    void SetChangeSkill(int SetSkill)
    {
        c_Skill = SetSkill;
    }

}
