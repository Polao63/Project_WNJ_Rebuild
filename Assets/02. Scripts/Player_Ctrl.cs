using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ctrl : MonoBehaviour
{
    public float h = 0f;
    public float v = 0f;

    float moveSpeed = 7f;
    Vector3 moveDir = Vector3.zero;

    

    //---- 주인공 화면 밖으로 이탈하지 않도록 하기 위한 변수
    Vector3 HalfSize = Vector3.zero;
    Vector3 m_CacCurPos = Vector3.zero;

    public GameObject Explosion_Prefab = null;

    bool Invincible = false;
    bool inplay = false;

    public static Player_Ctrl inst;

    float inv_Time = 0f;



    // Start is called before the first frame update
    void Start()
    {
        inst = this;
        inplay = true;
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        if (h != 0f || v != 0f)
        {
            moveDir = new Vector3(h, v, 0);

            if (1 < moveDir.magnitude)
            { moveDir.Normalize(); }

            transform.position += moveDir * moveSpeed * Time.deltaTime;

            //if (h != 0)
            //{
            //    if (h < 1 || h > -1)
            //    {
            //        if (h <= 0.5)
            //        { h = 0; }
            //        Bit_Ctrl.inst.angle = h * 180 - 90;
            //        if (h == 0)
            //        { return; }
            //    }
            //}
            //if (v != 0)
            //{
            //    if (Mathf.Abs(v) > 0 )
            //    {
            //        Bit_Ctrl.inst.angle = v * 180 - 180;
            //    }
            //}

            // 또는 transform.Translate(moveDir * moveSpeed * Time.deltaTime);
        }

        if (inplay == true)
        {
            LimitMove();
        }

        if (Invincible == true)
        {
            GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 110);
            inv_Time -= Time.deltaTime;
            if (inv_Time <= 0)
            {
                GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                Invincible = false;
                inplay = true;
            }
        }

    }

    void LimitMove()
    {
        m_CacCurPos = transform.position;

        if (m_CacCurPos.x < CameraResolution.m_ScreenWMin.x + HalfSize.x)
        { m_CacCurPos.x = CameraResolution.m_ScreenWMin.x + HalfSize.x; }


        if (m_CacCurPos.x > CameraResolution.m_ScreenWMax.x - HalfSize.x)
        { m_CacCurPos.x = CameraResolution.m_ScreenWMax.x - HalfSize.x; }

        if (m_CacCurPos.y < CameraResolution.m_ScreenWMin.y + HalfSize.y)
        { m_CacCurPos.y = CameraResolution.m_ScreenWMin.y + HalfSize.y; }


        if (m_CacCurPos.y > CameraResolution.m_ScreenWMax.y - HalfSize.y)
        { m_CacCurPos.y = CameraResolution.m_ScreenWMax.y - HalfSize.y; }

        transform.position = m_CacCurPos;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Invincible != true)
        {
            if (collision.tag == "Enemy_Bullet" || collision.tag == "Enemy")
            {
                Destroy(collision.gameObject);
                gameObject.SetActive(false);
                GameObject Explo = Instantiate(Explosion_Prefab);
                Explo.transform.position = this.transform.position;
                
            }
        }
    }

    public void Respawn()
    {
        GameObject[] Bullet2Des = GameObject.FindGameObjectsWithTag("Enemy_Bullet");
        //Debug.Log(Bullet2Des.Length);
        for (int i = 0; i < Bullet2Des.Length; i++)
        {
            Destroy(Bullet2Des[i]);
        }

        Debug.Log("respawned");
        Invincible = true;
        gameObject.SetActive(true);
        this.gameObject.transform.position = new Vector3(0,-3, 0);
        inv_Time = 3f;

    }


}
