using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    //pub.
    public GameObject Player;
    public GameObject SetPlayer;
    public GameObject Wepon1, Wepon2,Wepon3;
    public GameObject SkillBullet;
    //実際に動かす腕部分.
    public GameObject GuruX_L, GuruX_R;   //ぐるぐるパンチ.
    public GameObject GuruY_L, GuruY_R;       //ダブルラリアット.
    public GameObject GuruZ_L, GuruZ_R;       //Z軸回転.

    public GameObject Empty;                        //空のオブジェクト.

    public Image HPBar;
    public AnimationClip DeadAnim;
    //pri.
    private Animation PlayerAnim;
    [SerializeField] private GameObject ShotPos;
    //pub sta.
    public static bool Guru2Attack;                 //通常攻撃の管理フラグ.
    public static bool Guru2Skill;                  //スキル攻撃の管理フラグ.
    public static bool Gurad;
    public static bool UseSkill;
    public static int EnemyKillCount;

    //Local.
    bool HealCheck;
    SEMusic seMusic;
    CharaStateProc charaState;
    CharaCustom charaCustom;

    void Start()
    {
        EnemyKillCount = 0;
        HealCheck = false;
        UseSkill = false;
        Guru2Attack = false;
        charaState = this.GetComponent<CharaStateProc>();   //スクリプトの取得.
        seMusic = SetPlayer.GetComponent<SEMusic>();   //スクリプトの取得.
        charaCustom = this.GetComponent<CharaCustom>();   //スクリプトの取得.


        PlayerAnim = this.gameObject.GetComponent<Animation>();
    }

    void Update()
    {
        //カスタムデータの適応を行う.
        charaCustom.WeponList(Wepon1, Wepon2, Wepon3, CharaCustom.WeponNo);
        charaCustom.ColorList(SetPlayer, CharaCustom.ColorNo);
        charaCustom.SkillList(SetPlayer, CharaCustom.SkillNo, ref charaState.UseSkillPoint);

        //被弾時処理（HP減少処理：無敵時間の設定）.
        seMusic.SEPlay(ref charaState.AtkHitSE, 1);
        charaState.HPProc(ref charaState.AtkHit);               //HP減少処理.

        //スキルの処理.
        SkillProc();
        //ノックバック.
        charaState.CharaNockBack(ref charaState.DamageCnt);


        //死亡アニメーション処理.
        charaState.CharaDeadAnim(PlayerAnim, DeadAnim);


        SetUpPlayerWepon(CharaCustom.WeponNo);
        //KillHealProc();


        //seMusic.SEPlay(ref charaState.Dead, 0);
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

    void KillHealProc()
    {
        if (5 <= EnemyKillCount)
        {
            HealCheck = true;
            seMusic.SEPlay(ref HealCheck, 7);
            HPBar.fillAmount += 0.2f;
            EnemyKillCount = 0;
        }
        else
        {
            HealCheck = false;
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
        charaState.CharaDead(charaState.Dead, Player, Empty);
        if (ButtonProc.BattleType == 1)
        {//スコアアタックモード中、プレイヤーが死んだとき終わらせる.
            Score.S_GameEnd = true;
        }
        else if (ButtonProc.BattleType == 1)
        {//タイムアタックモード中、プレイヤーが死んだとき終わらせる.
         //記録タイムの更新は行わない.
            Timer.T_GameEnd = true;
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
        }
    }
}
