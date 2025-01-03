using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Common")]
    public GameObject m_BulletPrefab = null;
    public bool isEnemy;
    public float m_MaxShootCool = 2f;
    
    float m_ShootCool = 0f; //주기 계산

    [Header("Enemy")]
    public bool Aim2Player;
    public bool Autoshot;
    public float Autoshot_delay = 0f;
    public int shoot_Limit = 0;
    public int shooted = 0;

    [Header("Player")]
    public bool R_2 = false;
    float Chargetime = 0f;

    float rocket2_shoot;

    Bullet a_BulletSc;
     
    Vector2 direction;

    Player_Ctrl m_RefHero = null;
    Vector3 m_DirVec;

    AudioSource audioSource;

    [HideInInspector] public bool IsHoming = false;

    // Start is called before the first frame update
    void Start()
    {
        m_RefHero = GameObject.FindObjectOfType<Player_Ctrl>();
        rocket2_shoot = m_MaxShootCool / 2;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.FindObjectOfType<Game_Manager>().Pause && Item_Manager.inst.Item_Select == false)
        {
            if (isEnemy && Autoshot)
            {
                if (GetComponentInParent<Enemy_Ctrl>().transform.position.y <= CameraResolution.m_ScreenWMax.y
                    || GetComponentInParent<Enemy_Ctrl>().transform.position.y >= CameraResolution.m_ScreenWMin.y)
                {
                    if (shoot_Limit == 0 || shooted < shoot_Limit)
                    { EnemyShoot(); }
                }
                
            }
            else if(isEnemy == false) { FireUpdate(); }
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
        }
    }

    void FireUpdate()
    {
        direction = (transform.localRotation * Vector2.up).normalized;
        if (m_ShootCool > 0)
        {
            m_ShootCool -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.V) == true)
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


        if (Input.GetKey(KeyCode.Z) == true && Player_Ctrl.inst.Crash_Bomb_Gun.activeSelf == false)
        {
            if (Player_Ctrl.inst.M_Weapon == Cur_Main_Weapon.ChargeShot)
            {
                if (Chargetime < 3f)
                {
                    Debug.Log("chargeing... current : " + Chargetime);
                    Chargetime += Time.deltaTime * 2;
                }
            }
            else 
            {
                if (rocket2_shoot > 0f) rocket2_shoot -= Time.deltaTime;

                if (m_ShootCool <= 0f)
                {
                    if (R_2 && rocket2_shoot > 0f) return;

                    GameObject a_CloneObj = Instantiate(m_BulletPrefab) as GameObject;
                    a_CloneObj.transform.position = this.transform.position;

                    a_BulletSc = a_CloneObj.GetComponent<Bullet>();
                    a_BulletSc.direction = direction;
                    a_CloneObj.transform.rotation = transform.rotation;

                    m_ShootCool = m_MaxShootCool;
                    if (R_2) rocket2_shoot = m_MaxShootCool / 2;
                    audioSource.Play();
                }
            }
        }
        else { if (R_2) rocket2_shoot = m_MaxShootCool / 2; ; }

        if(Input.GetKeyUp(KeyCode.Z) && Player_Ctrl.inst.M_Weapon == Cur_Main_Weapon.ChargeShot)
        {
            GameObject a_CloneObj = Instantiate(m_BulletPrefab) as GameObject;
            a_CloneObj.transform.localScale = Vector3.one * (Chargetime+1);
            a_CloneObj.transform.position = this.transform.position;

            a_BulletSc = a_CloneObj.GetComponent<Bullet>();
            a_BulletSc.direction = direction;
            a_CloneObj.transform.rotation = transform.rotation;

            m_ShootCool = m_MaxShootCool;
            
            Chargetime = 0;
        }


        //if (Input.GetKeyDown(KeyCode.X) == true && Input.GetKeyDown(KeyCode.Z) == true)
        //{ Debug.Log("zx"); }
    }

    public void EnemyShoot()
    {
        if (Autoshot_delay > 0)
        {
            Autoshot_delay -= Time.deltaTime;
        }


        direction = (transform.localRotation * Vector2.up).normalized;
        if (m_ShootCool > 0)
        {
            m_ShootCool -= Time.deltaTime;
        }

        if (m_ShootCool <= 0f && Autoshot_delay <= 0)
        {
            GameObject a_CloneObj = Instantiate(m_BulletPrefab) as GameObject;
            a_CloneObj.transform.position = this.transform.position;

            a_BulletSc = a_CloneObj.GetComponent<Bullet>();
            a_BulletSc.direction = direction;
            if (GetComponentInParent<Enemy_Ctrl>().bullet_Speed != 0)
            {
                a_BulletSc.m_MoveSpeed = GetComponentInParent<Enemy_Ctrl>().bullet_Speed;
            }
                
            a_CloneObj.transform.rotation = transform.rotation;

            m_ShootCool = m_MaxShootCool;

            shooted++;
        }
        
    }
}
