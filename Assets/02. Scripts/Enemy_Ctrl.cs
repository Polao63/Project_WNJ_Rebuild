using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public enum AI_Type
{
    AI_ShootBullet,
    AI_Charge,
    AI_ShootNRun
}

public enum Mon_Type
{
    MT_Small,
    MT_Medium,
    MT_Big
}

public class Enemy_Ctrl : MonoBehaviour
{
    public Mon_Type m_MoType = Mon_Type.MT_Small; 
    public AI_Type m_AIType = AI_Type.AI_Charge;

    //---- 몬스터 체력 변수
    float m_MaxHP = 200f;
    float m_CurHP = 200f;
    //public Image m_HpSdBar = null;


    public float m_Speed = 4; //이동속도
    Vector3 m_CurPos; //위치 계산용 변수
    Vector3 m_SpawnPos; //스폰 위치

    Player_Ctrl m_RefHero = null;
    Vector3 m_DirVec;

    public GameObject Explosion_Prefab = null;
    public bool isHoming = false;
    public bool isTracking = false;

    int Mon_Score = 10;

    //waypoint
    public GameObject Path_Root = null;
    private Transform[] WayPointList;
    public int nextIdx = 1;

    GameObject Target_Obj = null;//타겟 참조 변수
    Vector3 m_DesiredDir; //타겟을 향하는 방향 변수

    public GameObject ItemDrop = null;

    float delta = 0f;

    // Start is called before the first frame update
    void Start()
    {

        m_SpawnPos = this.transform.position;

        m_MaxHP = 10f;
        m_CurHP = 10f;

        if (m_MoType == Mon_Type.MT_Small)
        {
            m_CurHP = m_MaxHP;
            Mon_Score = 10;
            m_RefHero = GameObject.FindObjectOfType<Player_Ctrl>();
        }
        if (m_MoType == Mon_Type.MT_Medium)
        {
            m_MaxHP *= 2f;
            m_CurHP = m_MaxHP;
            Mon_Score = 50;
            m_RefHero = GameObject.FindObjectOfType<Player_Ctrl>();
        }
        if (m_MoType == Mon_Type.MT_Big)
        {
            m_MaxHP *= 3f;
            m_CurHP = m_MaxHP;
            Mon_Score = 100;
            m_RefHero = GameObject.FindObjectOfType<Player_Ctrl>();
        }
        
    }
    // Update is called once per frame
    void Update()
    {

        if (delta > 0)
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
            delta -= Time.deltaTime; 
        }
        if (delta <= 0)
        { GetComponentInChildren<SpriteRenderer>().color = Color.white; }

        if (m_AIType == AI_Type.AI_Charge)
        { AI_Charge_Update(); }

        if (this.transform.position.x < CameraResolution.m_ScreenWMin.x - 2)
        { Destroy(gameObject); }

        if (GameObject.Find("Game_Mgr").GetComponent<Game_Manager>().Pause)
        { m_Speed = 0; }
    }
    void AI_Charge_Update()
    {
        m_CurPos = transform.position;
        if (isHoming)
        {
            this.GetComponentInChildren<SpriteRenderer>().flipY = false;
            m_DirVec.x = 0f;
            m_DirVec.z = 0f;
            m_DirVec.y = -1f;

            if (m_RefHero != null)
            {
                Vector3 a_CacVec = m_RefHero.transform.position - transform.position;
                m_DirVec = a_CacVec;

                if (-1f >= a_CacVec.y)
                {
                    float angle = Mathf.Atan2(a_CacVec.x, -a_CacVec.y) * Mathf.Rad2Deg;
                    Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
                    transform.rotation = angleAxis;
                }
                m_DirVec = transform.up;
                transform.Translate(Vector3.up * -m_Speed * Time.deltaTime, Space.Self);
            }
        }

        else if (isTracking)
        {
            this.GetComponentInChildren<SpriteRenderer>().flipY = true;
            MoveWayPoint();
        }

        else
        {
            this.GetComponentInChildren<SpriteRenderer>().flipY = false;
            transform.rotation = Quaternion.Euler(0,0,0);
            m_CurPos.y += (-1f * Time.deltaTime * m_Speed);
            transform.position = m_CurPos;

        }
        
        
    }
    void MoveWayPoint()
    {
        WayPointList = Path_Root.GetComponentsInChildren<Transform>();

        Target_Obj = WayPointList[nextIdx].gameObject;

        m_DesiredDir = Target_Obj.transform.position - transform.position;
        m_DesiredDir.z = 0f;
        m_DesiredDir.Normalize();

        //스프라이트 회전
        float angle = Mathf.Atan2(-m_DesiredDir.x, m_DesiredDir.y) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = angleAxis;


        m_DirVec = -transform.up;
        transform.Translate(Vector3.up * m_Speed * Time.deltaTime);

    }

    public void TakeDamage(float a_Value, bool ScoreON = true)
    {
        if (m_CurHP <= 0)
        { return; }

        Vector3 a_CacPos = this.transform.position;
        float a_CacDmg = a_Value;
        if (m_CurHP < a_Value)
        { a_CacDmg = m_CurHP; }

        m_CurHP -= a_Value;
        delta = 0.1f;

        
        if (m_CurHP < 0)
        { m_CurHP = 0; }

        if (m_CurHP <= 0)
        {//몬스터 사망처리

            if (PlayerStatus.Scavenger)
            {
                if (ItemDrop != null)
                {
                    GameObject Item = Instantiate(ItemDrop) as GameObject;
                    Item.transform.position = this.transform.position;
                }
            }
            else 
            {
                if (ScoreON && Player_Ctrl.inst.TimeLimitedSkill_On == false) Game_Manager.Inst.fillamount_SuperGauge += 0.1f;
            }
            Game_Manager.Inst.P1_score += Mon_Score;

            GameObject Explo = Instantiate(Explosion_Prefab);
            Explo.transform.position = this.transform.position;
            Destroy(gameObject); //<-- 몬스터 GameObject 제거
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {// 주인공이 쏜 총알만 데미지가 발생하도록 처리
        //if (collision.tag == "PlayerBullet")
        //{
        //    Destroy(collision.gameObject); //몬스터에 충돌된 총알삭제
        //    TakeDamage(80f);
        //}

        if (collision.tag == "Player")
        { TakeDamage(80f); }

        if (isTracking)
        {
            if (collision.tag == "WAYPOINT")
            {
                if (nextIdx >= WayPointList.Length - 1)
                {
                    Destroy(gameObject);
                    //nextIdx = 1;
                }
                else { nextIdx++; }
            }
        }
    }

    
    //void Boss_AI_Update()
    //{
    //    if (m_BossState == BossAttState.BS_MOVE)
    //    {
    //        m_CurPos = this.transform.position;

    //        if (7 < m_CurPos.x)
    //        {
    //            m_CurPos.x += (-1 * Time.deltaTime * m_Speed);
    //            if (m_CurPos.x <= 7f)
    //            {
    //                Shoot_Time = 1.28f;
    //                m_BossState = BossAttState.BS_FEVER1_ATT;
    //            }

    //        }

    //        this.transform.position = m_CurPos;
    //    }
    //    else if (m_BossState == BossAttState.BS_NORMAL_ATT)
    //    {
    //        Shoot_Time -= Time.deltaTime;
    //        if (Shoot_Time <= 0f)
    //        {
    //            Vector3 a_TargetV = m_RefHero.transform.position - this.transform.position;
    //            a_TargetV.Normalize();
    //            GameObject a_NewObj = (GameObject)Instantiate(m_BulletPrefab);
    //            Bullet a_BulletSc = a_NewObj.GetComponent<Bullet>();
    //            a_BulletSc.BulletSpawn(this.transform.position, a_TargetV, BulletMvSpeed);
    //            a_BulletSc.IsTarget = true;
    //            float a_CacAngle = Mathf.Atan2(a_TargetV.y, a_TargetV.x) * Mathf.Rad2Deg;
    //            a_CacAngle += 180f;
    //            a_NewObj.transform.eulerAngles = new Vector3(0f, 0f, a_CacAngle);

    //            m_ShootCount++;
    //            if (m_ShootCount < 7)
    //            { Shoot_Time = 0.7f; }
    //            else
    //            {
    //                m_ShootCount = 0;
    //                Shoot_Time = 2f;
    //                m_BossState = BossAttState.BS_FEVER1_ATT;
    //            }
    //        }
    //    }

    //    else if (m_BossState == BossAttState.BS_FEVER1_ATT)
    //    {
    //        Shoot_Time -= Time.deltaTime;
    //        if (Shoot_Time <= 0f)
    //        {
    //            float Radius = 100f;
    //            Vector3 a_TargetV = Vector3.zero;
    //            GameObject a_NewObj;
    //            Bullet a_BulletSc = null;
    //            float a_CacAngle = 0f;
    //            for (float Angle = 0f; Angle < 300f; Angle += 15f)
    //            {
    //                a_TargetV.x = Radius * Mathf.Cos(Angle * Mathf.Deg2Rad);
    //                a_TargetV.y = Radius * Mathf.Sin(Angle * Mathf.Deg2Rad);
    //                a_TargetV.Normalize();
    //                a_NewObj = (GameObject)Instantiate(m_BulletPrefab);
    //                a_BulletSc = a_NewObj.GetComponent<Bullet>();
    //                a_BulletSc.BulletSpawn(this.transform.position, a_TargetV, BulletMvSpeed);

    //                a_BulletSc.IsTarget = true;
    //                a_CacAngle = Mathf.Atan2(a_TargetV.y, a_TargetV.x) * Mathf.Rad2Deg;
    //                a_CacAngle += 180f;
    //                a_NewObj.transform.eulerAngles = new Vector3(0f, 0f, a_CacAngle);
    //            }

    //            m_ShootCount++;
    //            if (m_ShootCount < 3)//3번 발사
    //            { Shoot_Time = 1f; }
    //            else
    //            {
    //                m_ShootCount = 0;
    //                Shoot_Time = 1.5f;
    //                m_BossState = BossAttState.BS_NORMAL_ATT;
    //            }
    //        }
    //    }


    //}




}
