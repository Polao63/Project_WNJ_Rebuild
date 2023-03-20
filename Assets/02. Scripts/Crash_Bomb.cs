using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crash_Bomb : MonoBehaviour
{
    Vector3 First_size = Vector3.one;
    Vector3 m_DirVec = Vector3.up; //날아가야 할 방향 벡터
    public float m_MoveSpeed = 15f;
    public float m_Crash_site_y = 5f;

    public float Bomb_Remain_Time = 5f;

    float delta;

    float First_y;
    float Moving_y;

    // Start is called before the first frame update
    void Start()
    {
        First_size = Vector3.one;
        First_y = this.gameObject.transform.position.y;
        delta = 0f;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Moving_y >= m_Crash_site_y)
        {
            m_MoveSpeed = 0;
            if (transform.localScale.x <= 20f * (Bomb_Remain_Time / 6)
                && transform.localScale.y <= 20f * (Bomb_Remain_Time / 6)
                && transform.localScale.z <= 20f * (Bomb_Remain_Time / 6))
            {
                transform.localScale += Vector3.one * Time.deltaTime * 500f;
            }
            else 
            {
                delta += Time.deltaTime;
                if (delta <= Bomb_Remain_Time + 1f)
                {
                    GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
                    GetComponent<SpriteRenderer>().flipY = !GetComponent<SpriteRenderer>().flipY;
                }
                else
                { 
                    Destroy(gameObject);
                    transform.localScale = First_size;
                }

            }
        }
        else 
        {
            Moving_y += Time.deltaTime * m_MoveSpeed;
            transform.Translate(m_DirVec * Time.deltaTime * m_MoveSpeed); 
        }

        if ((CameraResolution.m_ScreenWMax.x + 1f < transform.position.x)
            || (CameraResolution.m_ScreenWMin.x - 1f > transform.position.x)
            || (CameraResolution.m_ScreenWMax.y + 1f < transform.position.y)
            || (CameraResolution.m_ScreenWMin.y - 1f > transform.position.y))
        {//총알이 화면을 벗어나면 제거 
            Destroy(gameObject);
        }

        //GetComponent<Rigidbody2D>().AddForce(m_DirVec,ForceMode2D.Impulse);

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy_Ctrl>().TakeDamage(Player_Ctrl.inst.BulletDamage, false);
        }

    }
}
