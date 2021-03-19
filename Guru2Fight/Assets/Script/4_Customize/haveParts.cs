using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class haveParts : MonoBehaviour
{
    //public.

    //pub sta.
    public static int GachaGacha;
    //pri.
    [SerializeField] private GameObject WeponX,WeponY, WeponZ;
    [SerializeField] private GameObject Blue, Cyan,Green,Magenta,Red,White,Yellow;
    [SerializeField] private GameObject Heal, DefUP,ThrowAttack;

    //Local.
    int AddCnt;                     //セルの代入位置調整用.
    CustomPartRead customPartRead;

    void Start()
    {
        AddCnt = 2;
        customPartRead = this.GetComponent<CustomPartRead>();
        customPartRead.GachaDataRead();

        //所持状態に合わせて当てはめる.
        for (int i = 1; i < 13; i++)
        {
            GachaGacha = int.Parse(customPartRead.GachaList[AddCnt]);
            switch (i)
            {
                case 0:
                    /*
                     1 2 Y Z
                     3 4 5 6 7 8 9 黒以外のカラーリング
                     10 11 12 回復　防御アップ　攻撃スキル
                     */
                    break;
                case 1:
                    haveCheck(GachaGacha, WeponX);
                    break;
                case 2:
                    haveCheck(GachaGacha, WeponY);
                    break;
                case 3:
                    haveCheck(GachaGacha, WeponZ);
                    break;
                case 4:
                    haveCheck(GachaGacha, Blue);
                    break;
                case 5:
                    haveCheck(GachaGacha, Cyan);
                    break;
                case 6:
                    haveCheck(GachaGacha, Green);
                    break;
                case 7:
                    haveCheck(GachaGacha, Magenta);
                    break;
                case 8:
                    haveCheck(GachaGacha, Red);
                    break;
                case 9:
                    haveCheck(GachaGacha, White);
                    break;
                case 10:
                    haveCheck(GachaGacha, Yellow);
                    break;
                case 11:
                    haveCheck(GachaGacha, Heal);
                    break;
                case 12:
                    haveCheck(GachaGacha, DefUP);
                    break;
                case 13:
                    haveCheck(GachaGacha, ThrowAttack);
                    break;
            }
            AddCnt += 2;
        }
    }

    void Update()
    {
        
    }

    void haveCheck(int CheckNo,GameObject CheckObject)
    {
        if (CheckNo != 1)
        { CheckObject.SetActive(false); }
        else
        { CheckObject.SetActive(true); }
    }
}
