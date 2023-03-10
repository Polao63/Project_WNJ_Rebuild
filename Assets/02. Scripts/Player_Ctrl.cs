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

public enum Cur_Option
{
    Search,
    Hold,
    Rolling
}

public class Player_Ctrl : MonoBehaviour
{
    [Header("Movement")]
    public float h = 0f;
    public float v = 0f;
    public float Init_moveSpeed = 7f;
    float moveSpeed = 0f;
    Vector3 moveDir = Vector3.zero;
    //---- 주인공 화면 밖으로 이탈하지 않도록 하기 위한 변수
    Vector3 HalfSize = Vector3.zero;
    Vector3 m_CacCurPos = Vector3.zero;

    [Header("SuperBomb")]
    public SUPER_BOMB SuperB;
    public GameObject[] Super_Obj;
    public GameObject WAVE = null;
    public GameObject M_LASER = null;
    public bool Nemesis_system = false;


    [Header("Player_Status")]
    public GameObject Explosion_Prefab = null;
    bool Invincible = false;
    bool inplay = false;
    float inv_Time = 0f;
    public float BulletDamage = 10f;
    int Sub_Count = 2;

    public static Player_Ctrl inst;

    [Header("Option")]
    public Cur_Option C_option; 
    //float delta = 0f;
    public GameObject Sub_Rolling_Parent = null;
    public GameObject Sub_Rolling_Prefab = null;

    public GameObject Sub_Hold = null;

    public GameObject Sub_Search = null;


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

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        if (h != 0f || v != 0f)
        {
            moveDir = new Vector3(h, v, 0);

            if (1 < moveDir.magnitude)
            { moveDir.Normalize(); }

            transform.position += moveDir * moveSpeed * Time.deltaTime;

            if (inplay == true)
            {
                LimitMove();
            }

            
        }

        SUPER_MOVE();

        if (GameObject.FindObjectsOfType<Option_Ctrl>().Length < Sub_Count)
        {
            Option();
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
                    //Game_Manager.Inst.fillamount_SuperGauge = 0;
                    //Game_Manager.Inst.Super_Ready = false;
                }
                else
                {
                    //Debug.Log("LASER OFF!!");
                    M_LASER.SetActive(false);
                    moveSpeed = Init_moveSpeed;
                }

                break;

            case SUPER_BOMB.ATOMIC_WAVE:

                if (Input.GetKeyDown(KeyCode.C) && Game_Manager.Inst.Super_Ready == true)
                {
                    Debug.Log("ATOMIC WAVE!!");
                    WAVE.SetActive(true);
                    WAVE.transform.position = gameObject.transform.position;
                    Game_Manager.Inst.fillamount_SuperGauge = 0;
                    Game_Manager.Inst.Super_Ready = false;
                }

                break;

            case SUPER_BOMB.OVERLOAD:
                if (Input.GetKeyDown(KeyCode.C) && Game_Manager.Inst.Super_Ready == true)
                {
                    Debug.Log("OVERLOAD!!");
                    Game_Manager.Inst.fillamount_SuperGauge = 0;
                    Game_Manager.Inst.Super_Ready = false;
                }

                break;

            case SUPER_BOMB.SHIELD_RECOVERY:
                if (Input.GetKeyDown(KeyCode.C) && Game_Manager.Inst.Super_Ready == true)
                {
                    Debug.Log("SHIELD:RECOVERED");
                    Game_Manager.Inst.fillamount_SuperGauge = 0;
                    Game_Manager.Inst.Super_Ready = false;
                }

                break;

            case SUPER_BOMB.ZE_WARUDO:
                if (Input.GetKey(KeyCode.C) && Game_Manager.Inst.Super_Ready == true)
                {
                    Debug.Log("ZE_WARUDO! TOKIO TOMARE!!");
                    Time.timeScale = 0.5f;
                    moveSpeed = Init_moveSpeed * 2;
                    //Game_Manager.Inst.fillamount_SuperGauge = 0;
                    //Game_Manager.Inst.Super_Ready = false;
                }
                else
                {
                    Time.timeScale = 1.0f;
                    moveSpeed = Init_moveSpeed;
                }
                

                break;

            case SUPER_BOMB.LUCKY_3:
                if (Input.GetKeyDown(KeyCode.C) && Game_Manager.Inst.Super_Ready == true)
                {
                    Debug.Log("Lucky 3");
                    Game_Manager.Inst.fillamount_SuperGauge = 0;
                    Game_Manager.Inst.Super_Ready = false;
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
                collision.GetComponent<Enemy_Ctrl>().TakeDamage(Player_Ctrl.inst.BulletDamage);
            }
            else Destroy(collision.gameObject);

            if (Invincible != true)
            {
                gameObject.SetActive(false);
                GameObject Explo = Instantiate(Explosion_Prefab);
                Explo.transform.position = this.transform.position;
            }
        }


        if (this.GetComponentInChildren<BoxCollider2D>().CompareTag("Enemy") == true)
        {
            Debug.Log("1");
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

    void Option()
    {
        if (C_option == Cur_Option.Hold) { Sub_Hold.SetActive(true); }
        else { Sub_Hold.SetActive(false); }


        if (C_option == Cur_Option.Search)
        {
            Sub_Search.SetActive(true);
        }
        else { Sub_Search.SetActive(false); }

        if (C_option == Cur_Option.Rolling)
        {
            Sub_Rolling_Parent.SetActive(true);
            for (int ii = 0; ii < Sub_Count; ii++)
            {
                GameObject obj = Instantiate(Sub_Rolling_Prefab) as GameObject;
                obj.GetComponent<Option_Ctrl>().O_type = Option_Type.Rolling;
                obj.transform.SetParent(Sub_Rolling_Parent.transform);
                Option_Ctrl sub = obj.GetComponent<Option_Ctrl>();
                if (sub != null)
                    sub.SubHeroSpawn(this.gameObject, (360 / Sub_Count) * ii);
            }
        }
        else { Sub_Rolling_Parent.SetActive(false); }


    }


}
