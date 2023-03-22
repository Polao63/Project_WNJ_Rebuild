using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Cur_Main_Weapon
{
    Normal,
    Flame,
    Rocket,
    ChargeShot
}

public enum Cur_Sub_Weapon
{
    Wide,
    HomingMissile
}

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
    [Header("DEBUG")]
    public bool DEBUG_MODE = false;
    //public bool DEBUG_MODE_IDDQD = false;

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
    public GameObject Crash_Bomb_Gun = null;
    public bool Nemesis_system = false;
    public bool TimeLimitedSkill_On = false;
    public float Super_Time = 0f;

    [Header("MainWeapon")]
    public Cur_Main_Weapon M_Weapon;
    public GameObject[] M_Weapon_Obj;

    [Header("SubWeapon")]
    public bool Sub_Weapon_On = false;
    public GameObject Sub_Weapon_Parent = null;
    public Cur_Sub_Weapon S_Weapon;
    public GameObject[] S_Weapon_Obj;

    [Header("Option")]
    public bool Option_On = false;
    public GameObject Option_Parent = null;
    public Cur_Option C_option;
    //float delta = 0f;
    public GameObject[] Sub_Option_Obj;
    public GameObject Sub_Rolling_Prefab = null;

    [Header("Player_Status")]
    public GameObject Explosion_Prefab = null;
    bool Invincible = false;
    bool inplay = false;
    float inv_Time = 0f;
    public float BulletDamage = 10f;
    int Sub_Count = 2;

    public static Player_Ctrl inst;

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
        if (Item_Manager.inst.Item_Select == true)
        { return; }

        if (Invincible == true)
        {
            GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 110);
            inv_Time -= Time.deltaTime;
            if (inv_Time <= 0)
            {
                GetComponentInChildren<SpriteRenderer>().color = Color.white;
                Invincible = false;
                inplay = true;
            }
        }

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

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

        Main_Weapon();

        SUPER_MOVE();

        Sub_Weapon_Parent.gameObject.SetActive(Sub_Weapon_On);

        if (Sub_Weapon_Parent.activeSelf)
        { Sub_Weapon(); }

        Option_Parent.gameObject.SetActive(Option_On);

        if (Option_Parent.gameObject.activeSelf)
        { Option(); }
        
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
        if (DEBUG_MODE == false)
        {
            SuperB = PlayerStatus.Selected_Super;

            Nemesis_system = PlayerStatus.Nemesis;
        }

        if (Super_Time > 0)
        { Super_Time -= Time.deltaTime; }

        switch (SuperB)
        {
            case SUPER_BOMB.MEGALASER:
                if (Nemesis_system)
                {
                    if (Game_Manager.Inst.fillamount_SuperGauge > 0 && Game_Manager.Inst.Super_ChargeStart == false)
                    {
                        if (Input.GetKey(KeyCode.C))
                        {
                            Debug.Log("LASER ON!!");
                            M_LASER.SetActive(true);
                            M_LASER.transform.localScale = new Vector3(0.5f, 2, 1);
                            moveSpeed = Init_moveSpeed / 2;
                            M_LASER.transform.position = gameObject.transform.position;
                            Game_Manager.Inst.fillamount_SuperGauge -= 0.005f;
                        }
                        else
                        {
                            //Debug.Log("LASER OFF!!");
                            M_LASER.SetActive(false);
                            moveSpeed = Init_moveSpeed;
                        }
                    }
                    else 
                    {
                        M_LASER.SetActive(false);
                        moveSpeed = Init_moveSpeed;
                        Game_Manager.Inst.Super_Ready = false;
                        Game_Manager.Inst.Super_ChargeStart = true;
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.C) && Game_Manager.Inst.Super_Ready == true)
                    {
                        Game_Manager.Inst.Super_Ready = false;
                        M_LASER.transform.localScale = new Vector3(2, 2, 1);
                        TimeLimitedSkill_On = true;
                    }
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
                    TimeLimitedSkill_On = true;
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
                if (Game_Manager.Inst.fillamount_SuperGauge > 0 && Game_Manager.Inst.Super_ChargeStart == false) 
                {
                    if (Input.GetKey(KeyCode.C))
                    {
                        Debug.Log("ZE_WARUDO! TOKIO TOMARE!!");
                        Time.timeScale = 0.5f;
                        moveSpeed = Init_moveSpeed * 2;
                        Game_Manager.Inst.fillamount_SuperGauge -= 0.005f;
                    }
                    else
                    {
                        Time.timeScale = 1.0f;
                        moveSpeed = Init_moveSpeed;
                    }
                }
                else
                {
                    Time.timeScale = 1.0f;
                    moveSpeed = Init_moveSpeed;
                    Game_Manager.Inst.Super_Ready = false;
                    Game_Manager.Inst.Super_ChargeStart = true;
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

        if (Nemesis_system == false && TimeLimitedSkill_On && Game_Manager.Inst.fillamount_SuperGauge > 0)
        {
            switch (SuperB)
            {
                case SUPER_BOMB.MEGALASER:
                    Debug.Log("LASER ON!!");
                    M_LASER.SetActive(true);
                    moveSpeed = Init_moveSpeed / 3;
                    M_LASER.transform.position = gameObject.transform.position;
                    Game_Manager.Inst.fillamount_SuperGauge -= 0.004f;
                    if (Game_Manager.Inst.fillamount_SuperGauge <= 0) { TimeLimitedSkill_On = false; }
                    break;
                case SUPER_BOMB.OVERLOAD:
                    BulletDamage = 20f;

                    //수정 필요
                    Super_Time = 8f;
                    if (Super_Time <= 0) { TimeLimitedSkill_On = false; }
                    break;
                case SUPER_BOMB.LUCKY_3:


                    Super_Time = 8f;
                    if (Super_Time <= 0) { TimeLimitedSkill_On = false; }
                    break;

            }

            
        }
        else if(Nemesis_system == false && TimeLimitedSkill_On == false)
        {
            Game_Manager.Inst.Super_Ready = false;
            M_LASER.SetActive(false);
            moveSpeed = Init_moveSpeed;
            BulletDamage = 10f;
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

        if (collision.tag == "Scv_Item")
        {
            Game_Manager.Inst.fillamount_SuperGauge += 0.01f * (PlayerStatus.Scavenger_Combo + 1);
            PlayerStatus.Scavenger_Combo += 1;
            Destroy(collision.gameObject);
        }

        if (this.GetComponentInChildren<BoxCollider2D>().CompareTag("Enemy"))
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

    void Main_Weapon()
    {
        for (int ii = 0; ii < M_Weapon_Obj.Length; ii++)
        {
            if (ii == M_Weapon.GetHashCode())
                M_Weapon_Obj[ii].SetActive(true);
            else M_Weapon_Obj[ii].SetActive(false);
        }
    }

    void Sub_Weapon()
    {
        for (int ii = 0; ii < S_Weapon_Obj.Length; ii++)
        {
            if (ii == S_Weapon.GetHashCode())
                S_Weapon_Obj[ii].SetActive(true);
            else S_Weapon_Obj[ii].SetActive(false);
        }
    }

    void Option()
    {
        for (int ii = 0; ii < Sub_Option_Obj.Length; ii++)
        {
            if (ii == C_option.GetHashCode())
                Sub_Option_Obj[ii].SetActive(true);
            else Sub_Option_Obj[ii].SetActive(false);
        }

        if (C_option == Cur_Option.Rolling && Sub_Option_Obj[2].activeSelf)
        {
            if (GameObject.FindObjectsOfType<Option_Ctrl>().Length < Sub_Count)
            {
                for (int ii = 0; ii < Sub_Count; ii++)
                {
                    GameObject obj = Instantiate(Sub_Rolling_Prefab) as GameObject;
                    obj.GetComponent<Option_Ctrl>().O_type = Option_Type.Rolling;
                    obj.transform.SetParent(Sub_Option_Obj[2].transform);
                    Option_Ctrl sub = obj.GetComponent<Option_Ctrl>();
                    if (sub != null)
                    {
                        sub.SubHeroSpawn(this.gameObject, (360 / Sub_Count) * ii);
                    }  
                }
            }
        }
    }


}
