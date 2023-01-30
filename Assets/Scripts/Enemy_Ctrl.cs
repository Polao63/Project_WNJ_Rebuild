using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

public enum BossAttState
{
    BS_MOVE,// ����
    BS_NORMAL_ATT, //�⺻ ����
    BS_FEVER1_ATT, //�ǹ�1 Ÿ�� ����
    BS_FEVER2_ATT,
}

public class Enemy_Ctrl : MonoBehaviour
{
    public Mon_Type m_MoType = Mon_Type.MT_Small; 
    public AI_Type m_AIType = AI_Type.AI_Charge;

    //---- ���� ü�� ����
    float m_MaxHP = 200f;
    float m_CurHP = 200f;
    //public Image m_HpSdBar = null;


    public float m_Speed = 4; //�̵��ӵ�
    Vector3 m_CurPos; //��ġ ���� ����
    Vector3 m_SpawnPos; //���� ��ġ

    float m_CacPosY = 0f; //���� �Լ��� �� ���� ���� ����
    float m_Rand_Y = 0f; // ������ ������ ����� ����

    //---- �Ѿ� �߻� ����
    public GameObject m_BulletPrefab = null;
    public GameObject m_ShootPos = null;
    float Shoot_Time = 0f; // �Ѿ� �߻� �ֱ� ���� ����
    float shoot_Delay = 1.5f;// �Ѿ� ��Ÿ��
    float BulletMvSpeed = 10f; // �Ѿ� �̵� �ӵ�

    //---- ������ �ൿ ���� ����
    BossAttState m_BossState = BossAttState.BS_MOVE;
    int m_ShootCount = 0;

    Player_Ctrl m_RefHero = null;
    Vector3 m_DirVec;

    public GameObject Explosion_Prefab = null;
    public bool isHoming = false;
    public bool isTracking = false;

    int Mon_Score = 10;

    //waypoint
    private Transform[] points;
    public int nextIdx = 1;

    private Transform tr;

    public float speed = 1.0f;
    public float damping = 3.0f;


    // Start is called before the first frame update
    void Start()
    {

        tr = GetComponent<Transform>();
        points = GameObject.Find("Path").GetComponentsInChildren<Transform>();

        m_SpawnPos = this.transform.position;

        float m_MaxHP = 200f;
        float m_CurHP = 200f;

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
        //else if (m_MonType == MonType.MT_Boss)
        //{
        //    m_MaxHP = 3000f;
        //    m_CurHP = m_MaxHP;
        //    m_RefHero = GameObject.FindObjectOfType<Player_Ctrl>();

        //    Shoot_Time = 2f;
        //    m_BossState = BossAttState.BS_MOVE;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //if (m_MonType == MonType.MT_Zombi)
        //{ Zombi_Ai_Update(); }
        //else if (m_MonType == MonType.MT_Missle)
        //{ Missle_AI_Update(); }
        //else if (m_MonType == MonType.MT_Boss)
        //{ Boss_AI_Update(); }

        if (m_AIType == AI_Type.AI_Charge)
        { AI_Charge_Update(); }

        if (this.transform.position.x < CameraResolution.m_ScreenWMin.x - 2)
        { Destroy(gameObject); }
    }


    void AI_Charge_Update()
    {
        m_CurPos = transform.position;
        if (isHoming)
        {
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
                transform.Translate(Vector3.up * -m_Speed * Time.deltaTime);
            }
        }

        else if (isTracking)
        {
            MoveWayPoint();
        }

        else
        {
            m_CurPos.y += (-1f * Time.deltaTime * m_Speed);
            transform.position = m_CurPos;

        }
        
        
    }

    

    void MoveWayPoint()
    {
        //TODO : ����ź �ҽ� �����ؼ� �����

        if (nextIdx >= points.Length)
        { nextIdx = 1; }

        //if (transform.position == points[nextIdx].position)
        //{
        //    nextIdx++;
        //}

        //���� ��ġ���� ���� ��������Ʈ�� �ٶ󺸴� ���͸� ���
        Vector3 direction =  points[nextIdx].position - transform.position;
        //����� ������ ȸ�� ������ ���ʹϾ� Ÿ������ ����
        Quaternion rot = Quaternion.LookRotation(direction);

        tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * damping);

        transform.position = Vector3.MoveTowards(transform.position, points[nextIdx].position, Time.deltaTime * m_Speed);
        //tr.Translate(Vector3.down * Time.deltaTime * m_Speed);






        ////���� ��ġ���� ���� ��������Ʈ�� �ٶ󺸴� ���͸� ���
        //Vector3 direction =  points[nextIdx].position - tr.position;
        ////����� ������ ȸ�� ������ ���ʹϾ� Ÿ������ ����
        //Quaternion rot = Quaternion.LookRotation(direction);
        ////���� �������� ȸ���ؾ� �� �������� �ε巴�� ȸ�� ó��
        //tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * damping);
        ////���� �������� �̵� ó��
        //tr.Translate(Vector3.up * Time.deltaTime * -m_Speed);
    }

    void Zombi_Ai_Update()
    {
        m_CurPos = transform.position;
        m_CurPos.x += (-1f * Time.deltaTime * m_Speed);
        m_CacPosY += Time.deltaTime * (m_Speed / 2.2f);
        m_CurPos.y = m_SpawnPos.y + Mathf.Sin(m_CacPosY) * m_Rand_Y;

        transform.position = m_CurPos;

        //�Ѿ� �߻�
        if (m_BulletPrefab == null)
        { return; }

        Shoot_Time += Time.deltaTime;
        if (shoot_Delay <= Shoot_Time)
        {
            GameObject a_NewObj = Instantiate(m_BulletPrefab) as GameObject;
            Bullet a_BulletSc = a_NewObj.GetComponent<Bullet>();
            a_BulletSc.BulletSpawn(m_ShootPos.transform.position, Vector3.left, BulletMvSpeed);

            Shoot_Time = 0f;
        }
    }

    public void TakeDamage(float a_Value)
    {
        if (m_CurHP <= 0)
        { return; }

        Vector3 a_CacPos = this.transform.position;
        float a_CacDmg = a_Value;
        if (m_CurHP < a_Value)
        { a_CacDmg = m_CurHP; }

        //Game_Manager.Inst.DamageText(-a_CacDmg, a_CacPos, Color.red);

        m_CurHP -= a_Value;
        if (m_CurHP < 0)
        { m_CurHP = 0; }

        //if (m_HpSdBar != null)
        //{ m_HpSdBar.fillAmount = m_CurHP / m_MaxHP; }

        if (m_CurHP <= 0)
        {//���� ���ó��

            //����
            int Dice = Random.Range(0, 10);
            if (Dice < 3)
            { //Game_Manager.Inst.SpawnCoin(transform.position); 
            }

            Game_Manager.Inst.P1_score += Mon_Score;

            GameObject Explo = Instantiate(Explosion_Prefab);
            Explo.transform.position = this.transform.position;
            Destroy(gameObject); //<-- ���� GameObject ����
        }

    }

    //void Missle_AI_Update()
    //{
    //    m_CurPos = transform.position;

    //    m_DirVec.x = -1f;
    //    m_DirVec.z = 0f;
    //    m_DirVec.y = 0f;


    //    if (m_RefHero != null)
    //    {
    //        Vector3 a_CacVec = m_RefHero.transform.position - transform.position;
    //        m_DirVec = a_CacVec;

    //        if (-3.5f <= a_CacVec.x)
    //        {//�̻����� ���ΰ����� �Ÿ��� ���� �������� 3.5 �̻��̸�
    //            m_DirVec.y = 0f;
    //        }
    //    }
    //    m_DirVec.Normalize();
    //    m_DirVec.x = -1f; ///������ ���� �������� �̵��ϱ� ����.
    //    m_DirVec.z = 0f;



    //    m_CurPos += (m_DirVec * Time.deltaTime * m_Speed);
    //    transform.position = m_CurPos;
    //}

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
    //            if (m_ShootCount < 3)//3�� �߻�
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

    void OnTriggerEnter2D(Collider2D collision)
    {// ���ΰ��� �� �Ѿ˸� �������� �߻��ϵ��� ó��
        if (collision.tag == "PlayerBullet")
        {
            Destroy(collision.gameObject); //���Ϳ� �浹�� �Ѿ˻���
            TakeDamage(80f);
        }

        if (collision.tag == "Player")
        {
           TakeDamage(80f);

        }

        if (collision.tag == "WAYPOINT")
        {
            nextIdx++;
        }


            //if (collision.CompareTag("WAYPOINT"))
            //{
            //    nextIdx = (++nextIdx >= points.Length) ? 1 : nextIdx;
            //}
    }


}
