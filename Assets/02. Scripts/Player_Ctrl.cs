using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SUPER_BOMB
{
    MEGALASER,
    ATOMIC_WAVE,
    OVERLOAD,
    SHIELD_RECOVERY,
    ZE_WARUDO,
    LUCKY_3
}

public class Player_Ctrl : MonoBehaviour
{
    public float h = 0f;
    public float v = 0f;

    public float Init_moveSpeed = 7f;
    float moveSpeed = 0f;
    Vector3 moveDir = Vector3.zero;

    public SUPER_BOMB SuperB;
    public GameObject[] Super_Obj;

    public GameObject WAVE = null;
    public GameObject M_LASER = null;
    
    //---- 주인공 화면 밖으로 이탈하지 않도록 하기 위한 변수
    Vector3 HalfSize = Vector3.zero;
    Vector3 m_CacCurPos = Vector3.zero;

    public GameObject Explosion_Prefab = null;

    bool Invincible = false;
    bool inplay = false;

    public static Player_Ctrl inst;

    float inv_Time = 0f;

    float delta = 0f;


    // Start is called before the first frame update
    void Start()
    {
        inst = this;
        inplay = true;
        moveSpeed = Init_moveSpeed;
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

            { //if (h != 0)
              //{
              //    if (h < 1 || h > -1)
              //    {
              //        if (h <= 0.5)
              //        { h = 0; }
              //        Bit_Ctrl.inst.angle = (h * 180) / 2;
              //        if (h == 0)
              //        { return; }
              //    }
              //}

                //if (h < 1 || h > -1)
                //{
                //    if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1)
                //    {

                //        delta = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
                //        if (Mathf.Abs(Bit_Ctrl.inst.angle) <= 90)
                //        {

                //            if (Bit_Ctrl.inst.angle > 90)
                //            {
                //                Bit_Ctrl.inst.angle = 90;
                //            }
                //            else if (Bit_Ctrl.inst.angle < -90)
                //            {
                //                Bit_Ctrl.inst.angle = -90;
                //            }
                //            Bit_Ctrl.inst.angle = (delta * 180); 
                //        }


                //    }
                //}

                //if (h <= 1 || h >= -1)
                //{
                //    if (Mathf.Abs(h) > 0)
                //    {
                //        Bit_Ctrl.inst.angle = (h * 180) / 2;
                //    }
                //}
                //if (v != 0)
                //{
                //    if (Mathf.Abs(v) > 0)
                //    {
                //        Bit_Ctrl.inst.angle = (v * 180) / 2;
                //    }
                //}}

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

        SUPER_MOVE();
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

    void SUPER_MOVE()
    {
        switch (SuperB)
        {
            case SUPER_BOMB.MEGALASER:
                if (Input.GetKey(KeyCode.C))
                {
                    Debug.Log("LASER ON!!");
                    M_LASER.SetActive(true);
                    moveSpeed = Init_moveSpeed / 3;
                    M_LASER.transform.position = gameObject.transform.position;
                }
                else
                {
                    Debug.Log("LASER OFF!!");
                    M_LASER.SetActive(false);
                    moveSpeed = Init_moveSpeed;
                }

                break;

            case SUPER_BOMB.ATOMIC_WAVE:

                if (Input.GetKeyDown(KeyCode.C))
                {
                    Debug.Log("ATOMIC WAVE!!");
                    GameObject Wave = Instantiate(WAVE);
                    Wave.transform.position = gameObject.transform.position;
                }

                break;

            case SUPER_BOMB.OVERLOAD:
                if (Input.GetKeyDown(KeyCode.C))
                {
                    Debug.Log("OVERLOAD!!");
                }

                break;

            case SUPER_BOMB.SHIELD_RECOVERY:
                if (Input.GetKeyDown(KeyCode.C))
                {
                    Debug.Log("SHIELD:RECOVERED");
                }

                break;

            case SUPER_BOMB.ZE_WARUDO:
                if (Input.GetKey(KeyCode.C))
                {
                    Debug.Log("ZE_WARUDO! TOKIO TOMARE!!");
                    Time.timeScale = 0.5f;
                }
                else
                {
                    Time.timeScale = 1.0f;
                }
                

                break;

            case SUPER_BOMB.LUCKY_3:
                if (Input.GetKeyDown(KeyCode.C))
                {
                    Debug.Log("Lucky 3");
                }

                break;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Enemy_Bullet" || collision.tag == "Enemy")
        {
            if (collision.tag == "Enemy")
            {
                collision.GetComponent<Enemy_Ctrl>().TakeDamage(999f);
            }
            else Destroy(collision.gameObject);
            
            if (Invincible != true)
            {
                gameObject.SetActive(false);
                GameObject Explo = Instantiate(Explosion_Prefab);
                Explo.transform.position = this.transform.position;
            }
        }
    }

    public void Respawn()
    {
        Debug.Log("respawned");
        Invincible = true;
        gameObject.SetActive(true);
        this.gameObject.transform.position = new Vector3(0,-3, 0);
        inv_Time = 3f;

    }


}
