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

    float a_LmtBdLeft = 0;
    float a_LmtBdTop = 0;
    float a_LmtBdRight = 0;
    float a_LmtBdBottom = 0;

    public GameObject Explosion_Prefab = null;



    // Start is called before the first frame update
    void Start()
    {
        
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

        LimitMove();

       
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
        if (collision.tag == "Enemy_Bullet" || collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            GameObject Explo = Instantiate(Explosion_Prefab);
            Explo.transform.position = this.transform.position;
            GameObject.FindObjectOfType<Game_Manager>().Lives--;
        }
    }


}
