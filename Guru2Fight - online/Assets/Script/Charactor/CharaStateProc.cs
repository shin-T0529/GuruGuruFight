using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class CharaStateProc : MonoBehaviourPunCallbacks

{
    /*キャラクターが共通してもつもの*/

    //pub.
    public int DamageCnt;
    public float MaxDamage;                     //受けるダメージ.
    public float UseSkillPoint,CheckSkillPoint;
    public bool Dead,DeadWait;                  //死亡フラグと死亡猶予ﾌﾗｸﾞ.
    public bool EnemySet = false;               //アタッチ先で個々に切り替える.
    public bool AtkHit;
    public bool AtkHitSE;                       //ヒット時にSE鳴らす.
    public bool SkillCheck;                     //スキル使用時に付随する追加効果の処理を行う用.
    public bool GuradSkill;
    public bool BulletSkill;
    public Image HitPoint;                      //HP減少処理用.
    public Image SkillPoint;                      //HP減少処理用.


    //pri.
    private float DamageSpeed = 0.05f;         //HP減少速度.
    private float currentDamage;                //内部計算用.


    private AudioSource SESource;


    //Local.
    int SkillGauge;                             //スキルゲージ回復用.
    int GuradSkillTime, BulletSkillTime;         //スキルの効果時間適応用.
    float lx, rx;                               //腕の回転値格納用.
    float AddPower;
    Vector3 Pos, Rotate;                        //WorldC_HPの計算用.
    Vector3 CrePos;                             //同上.
    Rigidbody BuAdd;

    void Start()
    {
        SkillGauge = 0;
        DamageCnt = 0;
        rx = lx = 0;
        AddPower = 1000.0f;
        currentDamage = 0.0f;
        UseSkillPoint = CheckSkillPoint = 0.0f;
        Dead = false;
        DeadWait = false;
        AtkHit = false;
        SkillCheck = false;
        SESource = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    public void SkillStateGet()
    {
        if(SkillPoint == null)
        {
            SkillPoint = GameObject.Find("SkillTip").GetComponent<Image>();
            Debug.Log("CharaStateProc：Skill取得しました！");
        }
        CheckSkillPoint = SkillPoint.fillAmount;
    }

    public void HPStateGet()
    {
        if (HitPoint == null)
        {
            HitPoint = GameObject.Find("HP").GetComponent<Image>();
            Debug.Log("CharaStateProc：HP取得しました！");
        }
    }

    //攻撃処理.
    public void Attack(bool AttackFlag, GameObject RollLeft,GameObject RollRight,int W_No)
    {
        if(DeadWait == false)
        {
            if (AttackFlag == true)
            {
                if (lx < 10.0f && rx < 10.0f)
                {
                    lx += 0.4f;
                    rx += 0.4f;
                }
                AttackWepon(RollLeft, RollRight, W_No);
            }
            else
            {
                if (0.0f < lx || 0.0f < rx)
                {
                    lx -= 0.4f;
                    rx -= 0.4f;
                    if (lx <= 0.0f)
                    { lx = 0.0f; }
                    if (rx <= 0.0f)
                    { rx = 0.0f; }
                }
                AttackWepon(RollLeft, RollRight, W_No);
            }
        }
    }

    void AttackWepon(GameObject RollLeft, GameObject RollRight,int W_No)
    {
        switch (W_No)
        {
            case 0: //デフォルト：ぐるぐるパンチ.
                RollLeft.transform.Rotate(new Vector3(lx, 0, 0));
                RollRight.transform.Rotate(new Vector3(rx, 0, 0));
                break;
            case 1: //ダブラリ.
                RollLeft.transform.Rotate(new Vector3(0, lx, 0));
                RollRight.transform.Rotate(new Vector3(0, rx, 0));
                break;
            case 2:
                RollLeft.transform.Rotate(new Vector3(0, 0, lx));
                RollRight.transform.Rotate(new Vector3(0, 0, rx));
                break;
            default:
                break;
        }
    }
    
    //HPの減少処理(攻撃ヒットﾌﾗｸﾞ).
    public void HPProc(ref bool Hit)
    {
        if(Hit == true)
        {
            if (currentDamage < MaxDamage)
            {
                if (HitPoint.fillAmount <= MaxDamage && DeadWait == false)
                {//ゲージ無くなると死亡までのラグを無くすため.
                    Debug.Log("死ぬ準備します");
                    DeadWait = true;
                }
                HitPoint.fillAmount -= DamageSpeed; //HPの計算.
                currentDamage += 0.05f;        //徐々に減らすため加算していく.
                if(DeadWait == true && HitPoint.fillAmount <= 0.0f)
                {
                    Debug.Log("死にます");
                    Dead = true;
                }
            }
            else
            {//減少終了後はリセットする.
                currentDamage = 0.0f;
                MaxDamage = 0.0f;
                Hit = false;
                AtkHitSE = false;
            }
        }
    }

    public void SkillProc(ref bool Skill,int SkillNo,GameObject SetPlayer,GameObject CreateObj)
    {
        if (Skill == true)
        {
            switch (SkillNo)
            {
                case 0:
                    //スキルセットなし.
                    break;
                case 1:
                    //回復スキル.
                    if(SkillPoint.fillAmount > 0.61f)
                    {
                        HitPoint.fillAmount += HitPoint.fillAmount / 2; //HPの計算.
                        SkillPoint.fillAmount -= 0.61f;
                    }
                    break;
                case 2:
                    //防御スキル.
                    if (SkillPoint.fillAmount > 0.61f)
                    {
                        SkillPoint.fillAmount -= 0.61f;
                        GuradSkillTime++;
                        GuradSkill = true;
                    }

                    break;
                case 3:
                    //遠距離攻撃.
                    if (SkillPoint.fillAmount > 0.81f && BulletSkill == false)
                    {
                        CrePos = SetPlayer.transform.position;
                        GameObject ShpBullet = (GameObject)Instantiate(CreateObj, CrePos, Quaternion.identity);
                        BuAdd = ShpBullet.GetComponent<Rigidbody>();
                        AddPower = 700.0f;
                        BuAdd.AddForce(transform.forward * AddPower);      //戻す方向,力の加え方.
                        ShpBullet.SetActive(true);
                        SkillPoint.fillAmount -= 0.81f;
                        BulletSkill = true;
                    }
                    break;

                default:
                    break;
            }
            Skill = false;
        }


        //防御アップスキルの効果時間の設定.
        if (GuradSkill == true)
        {
            GuradSkillTime++;
            if(300 < GuradSkillTime)
            {
                GuradSkillTime = 0;
                GuradSkill = false;
            }
        }

        if(BulletSkill == true)
        {
            if (200 < BulletSkillTime)
            {
                AddPower = 1.0f;
                BulletSkill = false;
                BulletSkillTime = 0;
                //Destroy(ShpBullet);
            }
            else
            {
                BulletSkillTime++;
            }
        }
        //スキルゲージの回復処理.
        SkillGauge++;
        if(10 < SkillGauge)
        {
            SkillPoint.fillAmount += 0.01f;
            SkillGauge = 0;
        }
    }

    //死亡時のアニメーション再生.
    public void CharaDeadAnim(Animation animation, AnimationClip PlayAnim)
    {
        if(Dead == true)
        {
            animation.clip = PlayAnim;
            animation.Play();
        }
    }
    
    //死亡時に破棄を行う(死亡フラグ,消すオブジェクト,それに貼りついているHP).
    public void CharaDead(bool Dead,GameObject Chara,GameObject CharaHP)
    {
        if (Dead == true && MultiPlay.M_GameEnd == false)
        {
            switch(ButtonProc.BattleType)
            {
                case 0:
                    //チュートリアル.
                    Debug.Log("死にました～＾");
                    Destroy(CharaHP);
                    Destroy(Chara);
                    break;
                case 1:     //スコアアタックモード.
                    Debug.Log("死にました～＾");
                    Destroy(CharaHP);
                    Destroy(Chara);
                    break;
                case 2:     //タイムアタックモード.
                    Debug.Log("死にました～＾");
                    Destroy(CharaHP);
                    Destroy(Chara);
                    break;
                case 3:     //マルチプレイモード.
                    Debug.Log("死にました～＾");
                    Destroy(CharaHP);
                    break;
            }
        }
    }
    
    //一定回数被弾時ノックバックする.
    public void CharaNockBack(ref int HitCnt)
    {
        if(5 < HitCnt)
        {
            var rigidbody = GetComponent<Rigidbody>();
            rigidbody.AddForce(-transform.forward * AddPower, ForceMode.Impulse);      //戻す方向,力の加え方.

            AddPower -= 20.0f;
            if(AddPower <= 0.0f)
            {
                HitCnt = 0;
                AddPower = 1000.0f;
            }
        }
    }

    //ワールドキャンバスに敵のHPを表示・追尾する(貼りつけるHP,それを貼りつけるオブジェクト).
    public void WorldC_HP(ref GameObject SetHP,GameObject Target)
    {
        if(EnemySet == true)
        {
            //座標合わせ.
            Pos.x = Target.transform.position.x;
            Pos.y = Target.transform.position.y +2.0f;
            Pos.z = Target.transform.position.z;
            //頭上に表示.
            SetHP.transform.position = Pos;

            // transformを取得.
            Transform RoteHP = SetHP.transform;
            Transform RoteCharaTrans = Target.transform;

            // ワールド座標を基準に、回転値を取得する.
            Vector3 worldAngleHP = RoteHP.eulerAngles;              //HPの回転値.
            Vector3 worldAngleChara = RoteCharaTrans.eulerAngles;   //追うオブジェクトの回転値.
            //体力の回転値：追うオブジェクトの回転値.
            worldAngleHP.x = worldAngleChara.x;
            worldAngleHP.y = worldAngleChara.y;
            worldAngleHP.z = worldAngleChara.z;
            //ワールド座標に適応.
            RoteHP.eulerAngles = worldAngleHP; 
        }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            
    }
}
