using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public bool isEnemy;
    public bool Aim2Player;
    public bool Autoshot;

    public GameObject m_BulletPrefab = null;
    float m_ShootCool = 0f; //주기 계산
    public float m_MaxShootCool = 2f;

    Bullet a_BulletSc;

    Vector2 direction;

    Player_Ctrl m_RefHero = null;
    Vector3 m_DirVec;

    [HideInInspector] public bool IsHoming = false;

    // Start is called before the first frame update
    void Start()
    {
        m_RefHero = GameObject.FindObjectOfType<Player_Ctrl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.FindObjectOfType<Game_Manager>().Pause)
        {
            if (isEnemy && Autoshot)
            { EnemyShoot(); }
            else { FireUpdate(); }
        }

        if (Aim2Player == true)
        {
            GameObject Target_Obj = m_RefHero.gameObject;

            Vector3 m_DesiredDir = Target_Obj.transform.position - transform.position;
            m_DesiredDir.z = 0f;
            m_DesiredDir.Normalize();

            //스프라이트 회전
            float angle = Mathf.Atan2(-m_DesiredDir.x, m_DesiredDir.y) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = angleAxis;


            m_DirVec = -transform.up;
            //transform.Translate(Vector3.up * 10 * Time.deltaTime);
        }
    }

    void FireUpdate()
    {
        direction = (transform.localRotation * Vector2.up).normalized;
        if (m_ShootCool > 0)
        {
            m_ShootCool -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.X) == true && Input.GetKeyDown(KeyCode.Z) == true)
        { Debug.Log("zx"); }

        if (Input.GetKey(KeyCode.X) == true)
        {

            if (m_ShootCool <= 0f)
            {
                IsHoming = true;
                GameObject a_CloneObj = Instantiate(m_BulletPrefab) as GameObject;
                a_CloneObj.transform.position = this.transform.position;
                a_BulletSc = a_CloneObj.GetComponent<Bullet>();

                if (a_BulletSc != null)
                { a_BulletSc.IsHoming = IsHoming; }

                m_ShootCool = m_MaxShootCool;
            }

        }
        else { IsHoming = false; }


        if (Input.GetKey(KeyCode.Z) == true)
        {
            if (m_ShootCool <= 0f)
            {
                GameObject a_CloneObj = Instantiate(m_BulletPrefab) as GameObject;
                a_CloneObj.transform.position = this.transform.position;

                a_BulletSc = a_CloneObj.GetComponent<Bullet>();
                a_BulletSc.direction = direction;
                a_CloneObj.transform.rotation = transform.rotation;

                m_ShootCool = m_MaxShootCool;
            }
        }
    }

    void EnemyShoot()
    {
       
        direction = (transform.localRotation * Vector2.up).normalized;
        if (m_ShootCool > 0)
        {
            m_ShootCool -= Time.deltaTime;
        }

        if (m_ShootCool <= 0f)
        {
            GameObject a_CloneObj = Instantiate(m_BulletPrefab) as GameObject;
            a_CloneObj.transform.position = this.transform.position;

            a_BulletSc = a_CloneObj.GetComponent<Bullet>();
            a_BulletSc.direction = direction;
            a_CloneObj.transform.rotation = transform.rotation;

            m_ShootCool = m_MaxShootCool;
        }
        
    }
}
