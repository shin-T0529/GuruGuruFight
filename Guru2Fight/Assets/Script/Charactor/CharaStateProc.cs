using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharaStateProc : MonoBehaviour
{
    /*キャラクターが共通してもつもの*/

    //pub.
    public int DamageCnt;
    public float MaxDamage;                     //受けるダメージ.

    public bool Dead,DeadWait;                  //死亡フラグと死亡猶予ﾌﾗｸﾞ.
    public bool EnemySet = false;               //アタッチ先で個々に切り替える.
    public bool AtkHit;
    public bool AtkHitSE;                       //ヒット時にSE鳴らす.

    public Image HitPoint;                      //HP減少処理用.


    //pri.
    private float DamageSpeed = 0.005f;         //HP減少速度.
    private float currentDamage;                //内部計算用.


    private AudioSource SESource;


    //Local.
    float lx, rx;                               //腕の回転値格納用.
    float AddPower;
    Vector3 Pos;                                //WorldC_HPの計算用.
    Vector3 Rotate;                             //同上.



    void Start()
    {
        DamageCnt = 0;
        rx = lx = 0;
        AddPower = 1000.0f;
        currentDamage = 0.0f;
        Dead = false;
        DeadWait = false;
        AtkHit = false;
        SESource = GetComponent<AudioSource>();
    }

    void Update()
    {

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
            if (currentDamage < MaxDamage && HitPoint.fillAmount != 0.0f)
            {
                if (HitPoint.fillAmount < MaxDamage && DeadWait == false)
                {//ゲージ無くなると死亡までのラグを無くすため.
                    Debug.Log("死ぬ準備します");
                    DeadWait = true;
                }
                HitPoint.fillAmount -= DamageSpeed; //HPの計算.
                currentDamage += 0.01f;        //徐々に減らすため加算していく.
                if(DeadWait == true && HitPoint.fillAmount <= 0.0f)
                {
                    Debug.Log("死にます");
                    Dead = true;
                }
            }
            else
            {//減少終了後はリセットする.
                currentDamage = 0.0f;
                //MaxDamage = 0.0f;
                Hit = false;
                AtkHitSE = false;
            }
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
        if(Dead == true)
        {
            Debug.Log("死にました～＾");
            Destroy(Chara);
            Destroy(CharaHP);
        }
    }
    
    //一定回数被弾時ノックバックする.
    public void CharaNockBack(ref int HitCnt)
    {
        if(3 < HitCnt)
        {
            var rigidbody = GetComponent<Rigidbody>();
            //敢えてある程度遠くへ飛ばす.
            //rigidbody.AddForce(-transform.forward * 200f, ForceMode.VelocityChange);      //戻す方向,力の加え方.
            //HitCnt = 0;
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
