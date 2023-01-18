using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ctrl : MonoBehaviour
{
    float h = 0f;
    float v = 0f;

    float moveSpeed = 7f;
    Vector3 moveDir = Vector3.zero;

    

    //---- 주인공 화면 밖으로 이탈하지 않도록 하기 위한 변수
    Vector3 HalfSize = Vector3.zero;
    Vector3 m_CacCurPos = Vector3.zero;

    public GameObject Explosion_Prefab = null;

    bool Invincible = false;
    bool inplay = false;

    public static Player_Ctrl inst;



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
            // 또는 transform.Translate(moveDir * moveSpeed * Time.deltaTime);
        }

        if (inplay == true)
        {
            LimitMove();
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
                GameObject.FindObjectOfType<Game_Manager>().Lives--;
            }
        }
    }

    public void Respawn()
    {
        Debug.Log("respawned");
        Invincible = true;
        gameObject.SetActive(true);
        this.gameObject.transform.position = new Vector3(0,-6, 0);

        
        
        if (transform.position.y == -3)
        {
            inplay = true;
            Invincible = false;
            return;
        }
    }


}
