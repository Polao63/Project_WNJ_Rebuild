using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//보스 : NSBC(Not So Big Core)
//이름에서 알수 있듯 그라디우스의 빅 코어에서 따왔으나 이름과 달리 그렇게 크지는 않아 이렇게 불리게 되었다.
//패턴 1 : 1~4초의 랜덤한 시간이 지나면 레이저 4개를 발사한다.
//패턴 2 : 좌우로 이동하면서 쏘다가 멈추고 앞으로 돌진한다. 돌진이 끝나면 뒤로 천천히 돌아온다.
//패턴 3(상위 난이도) : 발사하는 레이저중 안쪽 2개의 레이저가 플레이어를 향해 발사한다.


public class Boss_NSBC_AI : MonoBehaviour
{
    [Header("Status")]
    public float BOSS_HP;
    public int Score;

    Enemy_Ctrl Ene_Ctrl;

    Gun[] guns;
    float moveSpeed;

    public float moveDir = 1f;

    public float Pattern_Dur = 0f;
    int val = 0;

    Vector3 HalfSize = Vector3.zero;
    Vector3 m_CacCurPos = Vector3.zero;

    int shooted = 0;
    bool Turn_Back = false;

    public GameObject[] Gun_Aim;

    void Awake()
    {
        Ene_Ctrl = GetComponent<Enemy_Ctrl>();
        if (Ene_Ctrl.m_AIType == AI_Type.AI_Boss && Ene_Ctrl.m_MoType == Mon_Type.MT_BOSS)
        {
            Ene_Ctrl.m_MaxHP = BOSS_HP;
            Ene_Ctrl.Mon_Score = Score;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        guns = transform.GetComponentsInChildren<Gun>();
        moveSpeed = GetComponent<Enemy_Ctrl>().m_Speed;
        val = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Random.InitState((int)(Time.time * 100));//랜덤 시드 초기화

        if (Game_Manager.Inst.GetComponent<Game_Manager>().Pause)
        { 
            moveSpeed = 0;
            Pattern_Dur = 0f;
            val = 0;
        }
        else 
        { moveSpeed = GetComponent<Enemy_Ctrl>().m_Speed; }

        if (val == 0)
        { val = (int)Random.Range(1f, 4f); }//패턴 시간 랜덤으로 생성

        if (this.transform.position.y <= 5f)//보스 입장 뒤
        {
            //destructable.Boss_Entered = true;
            if (Game_Manager.Inst.GetComponent<Game_Manager>().Pause == false)
            { Pattern_Dur += Time.deltaTime; }

            if (shooted < 5)
            {
                if (Pattern_Dur < val)//좌우 이동
                { moveLNR(); }
                else if (Pattern_Dur >= val && Game_Manager.Inst.GetComponent<Game_Manager>().Pause == false)//쏘기
                {
                    shoot();
                    Pattern_Dur = 0f;
                    val = 0;
                }
            }
            else
            {
                if (Pattern_Dur >= 1.5f)
                {
                    Boss_Charge();
                }
            }
        }   
    }

    void Boss_Charge()
    {
        Vector2 pos = transform.position;

        if (transform.position.y > -3f && Turn_Back == false)
        {
            pos.y += -1 * moveSpeed * 4f * Time.fixedDeltaTime;
            transform.position = pos;
        }
        else 
        {
            if (Pattern_Dur >= 4f)
            {
                Turn_Back = true;
                if (Turn_Back == true)
                {
                    if (transform.position.y < 3f)
                    {
                        pos.y += 1 * moveSpeed * 1.5f * Time.fixedDeltaTime;
                        transform.position = pos;
                    }
                    else
                    {
                        Turn_Back = false;
                        Pattern_Dur = 0f;
                        shooted = 0;
                    }
                }
            }
        }

    }

    void moveLNR()
    {
        Vector2 pos = transform.position;

        pos.x += moveDir * moveSpeed * Time.fixedDeltaTime;

        transform.position = pos;
        if (pos.x <= -1.73f || pos.x >= 1.73f)
        { moveDir = -(moveDir); }
    }

    void shoot()
    {
        shooted++;

        if ((Ene_Ctrl.m_CurHP / Ene_Ctrl.m_MaxHP) <= 0.33f)
        {
            for(int ii = 0; ii < Gun_Aim.Length; ii++)
            {
                if (Gun_Aim[ii].gameObject.activeSelf)
                {
                    Gun_Aim[ii].GetComponent<Gun>().Aim2Player = true;
                }
            }
        }
        else
        {
            for (int ii = 0; ii < Gun_Aim.Length; ii++)
            {
                if (Gun_Aim[ii].gameObject.activeSelf)
                {
                    Gun_Aim[ii].GetComponent<Gun>().Aim2Player = false;
                }
            }
        }

        foreach (Gun gun in guns)
        {
            if (gun.gameObject.activeSelf)
            {
                gun.EnemyShoot();
            }
        }
    }

    
}
