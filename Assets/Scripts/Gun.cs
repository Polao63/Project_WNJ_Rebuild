using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public bool isEnemy;
    public bool Autoshot;

    public GameObject m_BulletPrefab = null;
    float m_ShootCool = 0f; //�ֱ� ���
    public float m_MaxShootCool = 2f;

    Bullet a_BulletSc;

    Vector2 direction;

    [HideInInspector] public bool IsHoming = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isEnemy && Autoshot)
        { EnemyShoot(); }
        else { FireUpdate(); }
        
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

                m_ShootCool = 0.15f;
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

                m_ShootCool = 0.15f;
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
