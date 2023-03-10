using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Option_Type
{
    Search,
    Hold,
    Rolling
}

public class Option_Ctrl : MonoBehaviour
{
    Player_Ctrl m_RefHero = null;

    public Option_Type O_type;

    public float angle = 0f;
    float radius = 1.0f;

    public GameObject parent_Obj = null;
    Vector3 parent_Pos = Vector3.zero;

    [Header("Search_Option")]
    //유도탄 변수
    [HideInInspector] public bool IsTarget = false;
    //한번이라도 타겟이 잡힌적 있는가?

    [HideInInspector] public GameObject Target_Obj = null;//타겟 참조 변수
    Vector3 m_DesiredDir; //타겟을 향하는 방향 변수

    Vector3 m_DirVec = Vector3.up;
    public float m_MoveSpeed = 15f;


    [Header("Hold_Option")]
    public float max_angle = 45f;

    public bool R_2 = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (O_type == Option_Type.Search)
        { Search_Option(); }
        if (O_type == Option_Type.Hold)
        { Hold_Option(); }

        if (O_type == Option_Type.Rolling)
        { Rolling_Option(); }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (O_type == Option_Type.Rolling)
        {
            if (collision.tag == "Enemy_Bullet")
            {
                Destroy(collision.gameObject);
            }
        }
           
    }

    void Search_Option()
    {
        //if (Target_Obj == null && IsTarget == false)//추적햐아 할 타겟이 없으면
        //{ FindEnemy(); }//타겟 찾는 함수

        //if (Target_Obj != null)
        //{ BulletHoming(); }//타겟을 향해 추적 이동하는 행동패턴 함수
        //else//Target Lost
        //{
        //    if (R_2)
        //    {
        //        m_DesiredDir = parent_Obj.transform.position - transform.position + new Vector3(-0.72f, 0, 0);
        //    }
        //    else
        //    { m_DesiredDir = parent_Obj.transform.position - transform.position + new Vector3(0.72f, 0, 0); }
        //    m_DesiredDir.z = 0f;
        //    m_DesiredDir.Normalize();

        //    float angle = Mathf.Atan2(-m_DesiredDir.x, m_DesiredDir.y) * Mathf.Rad2Deg;
        //    Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
        //    transform.rotation = angleAxis;

        //    m_DirVec = transform.up;
        //    transform.Translate(Vector3.up * m_MoveSpeed * Time.deltaTime);
          
        //    if (Mathf.Abs(transform.position.x) <= 0.7f)
        //    {
        //        Vector3 a;
        //        m_MoveSpeed = 0;
        //        if (R_2)
        //        {
        //            a = new Vector3(-0.7f, -3, 0);
        //        }
        //        else
        //        { a = new Vector3(0.7f, -3, 0); }
        //        transform.position = a;
        //        transform.rotation = Quaternion.Euler(0,0,0);
        //    }
        //}
    }

    void FindEnemy()//타겟을 찾아주는 함수
    {
        GameObject[] a_EnemyList = GameObject.FindGameObjectsWithTag("Enemy");
        if (a_EnemyList.Length <= 0)
        { return; }

        GameObject a_FindMon = null;
        float a_CacDist = 0f;
        Vector3 a_CacVec = Vector3.zero;

        for (int ii = 0; ii < a_EnemyList.Length; ii++)
        {
            a_CacVec = a_EnemyList[ii].transform.position - transform.position;
            a_CacVec.z = 0f;
            a_CacDist = a_CacVec.magnitude;

            if (4f < a_CacDist)//총알로부터 4 반경 안에 있는 몬스터만
            { continue; }

            a_FindMon = a_EnemyList[ii].gameObject;
            break;
        }

        Target_Obj = a_FindMon;
        if (Target_Obj != null)
        { IsTarget = true; }

    }

    void BulletHoming()//타겟을 향해 추적이동하는 행동패턴 함수
    {
        m_DesiredDir = Target_Obj.transform.position - transform.position;
        m_DesiredDir.z = 0f;
        m_DesiredDir.Normalize();

        m_DirVec = transform.up;
        transform.Translate(Vector3.up * m_MoveSpeed * Time.deltaTime);


    }

    void Hold_Option()
    {
        if (360 < angle)
        { angle -= 360f; }
        if (angle < -360)
        { angle += 360f; }

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            if (Player_Ctrl.inst.v != 0)
            {
                if (angle <= max_angle && angle >= -max_angle)
                {
                    if (R_2) { angle -= Time.deltaTime * Player_Ctrl.inst.v * 500f; }
                    else { angle += Time.deltaTime * Player_Ctrl.inst.v * 500f; }
                }
                if (angle > max_angle)
                {
                    angle = max_angle;
                }
                else if (angle < -max_angle)
                {
                    angle = -max_angle;
                }
            }
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }


    void Rolling_Option()
    {
        angle += Time.deltaTime * 500f;
        if (360.0f < angle)
            angle -= 360.0f;

        if (parent_Obj == null)
            return;

        parent_Pos = parent_Obj.transform.position;
        transform.position = parent_Pos +
                            new Vector3(radius * Mathf.Cos(angle * Mathf.Deg2Rad),
                                        radius * Mathf.Sin(angle * Mathf.Deg2Rad), 0.0f);

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }


    public void SubHeroSpawn(GameObject a_Paren, float a_Angle)
    {
        parent_Obj = a_Paren;
        m_RefHero = a_Paren.GetComponent<Player_Ctrl>();
        angle = a_Angle;
    }
}
