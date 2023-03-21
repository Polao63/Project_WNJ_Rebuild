using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_SubWep : MonoBehaviour
{
    public GameObject m_BulletPrefab = null;
    float m_ShootCool = 0f; //주기 계산
    public float m_MaxShootCool = 2f;

    Bullet a_BulletSc;

    Vector2 direction;

    Player_Ctrl m_RefHero = null;
    Vector3 m_DirVec;

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
            FireUpdate(); 
        }

        
    }

    void FireUpdate()
    {
        direction = (transform.localRotation * Vector2.up).normalized;
        if (m_ShootCool > 0)
        {
            m_ShootCool -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Z) == true)
        {
            if (m_ShootCool <= 0f)
            {
                GameObject a_CloneObj = Instantiate(m_BulletPrefab) as GameObject;
                a_CloneObj.transform.position = this.transform.position;

                if (a_CloneObj.gameObject.tag == "Homing_Missile")
                {
                    //a_CloneObj.GetComponent<Homing_Missle>();
                    //a_BulletSc.direction = direction;
                }
                a_CloneObj.transform.rotation = transform.rotation;

                m_ShootCool = m_MaxShootCool;
            }
        }
    }

  
}
