﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 m_DirVec = Vector3.up; //날아가야 할 방향 벡터
    public float m_MoveSpeed = 15f;
    public bool isEnemyBullet = false;

    //유도탄 변수
    [HideInInspector] public bool IsHoming = false;//유도 On
    [HideInInspector] public bool IsTarget = false;
    //한번이라도 타겟이 잡힌적 있는가?

    [HideInInspector] public GameObject Target_Obj = null;//타겟 참조 변수
    Vector3 m_DesiredDir; //타겟을 향하는 방향 변수

    public Vector2 direction = new Vector2(0, 1);

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Enemy_Bullet")
        { m_DirVec = Vector3.down; }
       
    }

    // Update is called once per frame
    void Update()
    {
        if ( Mathf.Abs(this.transform.position.y) > 8)
        { Destroy(gameObject); }

        m_DirVec.Normalize();

        if (IsHoming == true)//유도탄
        {
            if (Target_Obj == null && IsTarget == false)//추적햐아 할 타겟이 없으면
            { FindEnemy(); }//타겟 찾는 함수

            if (Target_Obj != null)
            { BulletHoming(); }//타겟을 향해 추적 이동하는 행동패턴 함수
            else//Target Lost
            { transform.position += m_DirVec * Time.deltaTime * m_MoveSpeed; }
        }
        else//일반 총알
        { transform.Translate(m_DirVec * Time.deltaTime * m_MoveSpeed); }



        if ((CameraResolution.m_ScreenWMax.x + 1f < transform.position.x)
            || (CameraResolution.m_ScreenWMin.x - 1f > transform.position.x)
            || (CameraResolution.m_ScreenWMax.y + 1f < transform.position.y)
            || (CameraResolution.m_ScreenWMin.y - 1f > transform.position.y))
        {//총알이 화면을 벗어나면 제거 
            Destroy(gameObject);
        }

        if (GameObject.Find("Game_Mgr").GetComponent<Game_Manager>().Pause)
        { m_MoveSpeed = 0; }
    }

    public void BulletSpawn(Vector3 a_StPos, Vector3 a_DirVec, float a_MvSpeed = 15f, float att = 20f)
    {
        m_DirVec = a_DirVec;
        transform.position = new Vector3(a_StPos.x, a_StPos.y, a_StPos.z);
        m_MoveSpeed = a_MvSpeed;
    }

    void FindEnemy()//타겟을 찾아주는 함수
    {
        GameObject[] a_EnemyList = GameObject.FindGameObjectsWithTag("Enemy");
        if (a_EnemyList.Length <= 0)
        { return; }

        GameObject a_FindMon = null;
        float a_CacDist = 0f;
        Vector3 a_CacVec = Vector3.zero;

        for (int ii = 0; ii < a_EnemyList.Length; ii++)
        {
            a_CacVec = a_EnemyList[ii].transform.position - transform.position;
            a_CacVec.z = 0f;
            a_CacDist = a_CacVec.magnitude;

            if (4f < a_CacDist)//총알로부터 4 반경 안에 있는 몬스터만
            { continue; }

            a_FindMon = a_EnemyList[ii].gameObject;
            break;
        }

        Target_Obj = a_FindMon;
        if (Target_Obj != null)
        { IsTarget = true; }

    }

    void BulletHoming()//타겟을 향해 추적이동하는 행동패턴 함수
    {
        m_DesiredDir = Target_Obj.transform.position - transform.position;
        m_DesiredDir.z = 0f;
        m_DesiredDir.Normalize();

        //적을 향해 회전 이동하는 코드

        //스프라이트 회전
        float angle = Mathf.Atan2(-m_DesiredDir.x, m_DesiredDir.y) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = angleAxis;


        m_DirVec = transform.up;
        transform.Translate(Vector3.up * 10 * Time.deltaTime);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || !isEnemyBullet)
        {
            Enemy_Ctrl a_RefMon = collision.gameObject.GetComponent<Enemy_Ctrl>();
            if (a_RefMon != null)
            {
                a_RefMon.TakeDamage(1000);
            }
            Destroy(gameObject);
        }
    }
}