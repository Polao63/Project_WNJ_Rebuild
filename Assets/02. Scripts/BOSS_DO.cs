using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS_DO : MonoBehaviour
{
    //보스 : 더블원
    //두개의 포대와 하나의 포대로 공격한다 하여 지어진 이름.대충 짓는 다는 걸 알수 있다...
    //패턴 1 : 양 옆에 있는 포대로 플레이어를 향해 난사. (한번 쏠때 3번)
    //패턴 2 : 중앙에 포대를 꺼네 탄 흩뿌리기를 시전.
    //패턴 3(상위 난이도) : 중앙의 레이저 포대를 꺼내 플레이어를 향해 발사.발사 중 현재 방향에서 반대쪽으로 2초동안 뱡향을 바꾼다.

    [Header("Status")]
    public float BOSS_HP;
    public int Score;

    Enemy_Ctrl Ene_Ctrl;

    public GameObject Mid_Cannon = null;
    public GameObject Mid_Cannon_Laser = null;

    public GameObject[] Main_Cannon = null;

    public float Pattern_Dur = 0f;
    public int Pattern_Shooted;

    float Mid_Cannon_rot;

    public float Mid_Cannon_rotSpeed = 5f;

    public int Mid_Cannon_Dir = 1;

    float Mid_Cannon_Pos_y;

    Vector3 pos;

    bool Pattern2_on = false;
    public bool Pattern3_on = false;

    Player_Ctrl m_RefHero = null;
    float Laser_Charge = 0;
    float Laser_Dur = 0;


    private void Awake()
    {
        Ene_Ctrl = GetComponent<Enemy_Ctrl>();
        if (Ene_Ctrl.m_AIType == AI_Type.AI_Boss && Ene_Ctrl.m_MoType == Mon_Type.MT_BOSS)
        {
            Ene_Ctrl.m_MaxHP = BOSS_HP;
            Ene_Ctrl.Mon_Score = Score;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_RefHero = GameObject.FindObjectOfType<Player_Ctrl>();
        Mid_Cannon_Laser.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 3)
        {
            transform.Translate(0, -Time.deltaTime, 0);
        }
        else 
        {
            for (int i = 0; i < Main_Cannon.Length; i++)
            {
                if (Main_Cannon[i] != null)
                {
                    if (Main_Cannon[i].GetComponentInChildren<Gun>().shooted >= Pattern_Shooted)
                    {
                        Main_Cannon[i].GetComponentInChildren<Gun>().Autoshot = false;
                        Pattern2_on = true;
                    }
                }

            }

            if (Main_Cannon[0] != null || Main_Cannon[1] != null)
            {
                if (Pattern2_on == true)
                {
                    if (Pattern_Dur <= 3)
                    {
                        SpreadShot();
                    }
                    else
                    {
                        Mid_Cannon.GetComponentInChildren<Gun>().Autoshot = false;
                        Mid_Cannon.transform.rotation = Quaternion.Euler(0, 0, 0);
                        if (Mid_Cannon_Pos_y < transform.position.y)
                        {
                            pos = new Vector3(0, Mid_Cannon_Pos_y, 1);
                            Mid_Cannon_Pos_y += Time.deltaTime * 1f;

                            Mid_Cannon.transform.position = pos;
                        }
                        else
                        {
                            for (int i = 0; i < Main_Cannon.Length; i++)
                            {
                                if (Main_Cannon[i] != null)
                                {
                                    Main_Cannon[i].GetComponentInChildren<Gun>().Autoshot = true;
                                    Main_Cannon[i].GetComponentInChildren<Gun>().shooted = 0;
                                }
                            }
                            Pattern_Dur = 0;
                            Pattern2_on = false;
                        }
                    }
                }
                else { Mid_Cannon_Pos_y = transform.position.y; }
            }
            else
            {
                if (Mid_Cannon.transform.position.y > -0.65f + transform.position.y)
                {
                    pos = new Vector3(0, Mid_Cannon_Pos_y, 1);
                    Mid_Cannon_Pos_y -= Time.deltaTime * 1f;

                    Mid_Cannon.transform.position = pos;
                }
                else
                {
                    //Pattern3_on = true;
                    Laser();
                }
            }
        }


       
    }

    void SpreadShot()
    {
        pos = new Vector3(0, Mid_Cannon_Pos_y, 1);
        Mid_Cannon.transform.position = pos;
        if (Mid_Cannon_Pos_y > -0.65f + transform.position.y)
        {
            Mid_Cannon_Pos_y -= Time.deltaTime * 1f;
            Mid_Cannon.GetComponentInChildren<Gun>().Autoshot = false;
        }


        if (Mid_Cannon.transform.position.y < -0.65f + transform.position.y)
        {
            Pattern_Dur += Time.deltaTime;

            Mid_Cannon_rot += Time.deltaTime * Mid_Cannon_Dir * Mid_Cannon_rotSpeed;
            if (Mid_Cannon_rot >= 45)
            {
                Mid_Cannon_Dir = -1;
            }
            else if (Mid_Cannon_rot <= -45)
            {
                Mid_Cannon_Dir = 1;
            }

            Quaternion i = Quaternion.Euler(0, 0, Mid_Cannon_rot);

            Mid_Cannon.transform.localRotation = i;
            Mid_Cannon.GetComponentInChildren<Gun>().Autoshot = true;

            Mid_Cannon.GetComponentInChildren<Gun>().m_MaxShootCool = Random.Range(0.01f, 0.1f);
        }
    }

    void Laser()
    {
        Mid_Cannon.GetComponentInChildren<Gun>().Autoshot = false;
        if (Pattern3_on)
        {
            //Mid_Cannon_Laser.transform.position = new Vector3
            //    (Mid_Cannon_Laser.transform.position.x,
            //    -Mid_Cannon_Laser.transform.position.y,
            //    Mid_Cannon_Laser.transform.position.z);
            Pattern_Dur = 0;
            Pattern3_on = false;

        }
        

        if (Pattern_Dur <= 2)
        {
            GameObject Target_Obj = m_RefHero.gameObject;


            Vector3 m_DesiredDir = Target_Obj.transform.position - Mid_Cannon.transform.position;
            m_DesiredDir.z = 0f;
            m_DesiredDir.Normalize();

            //스프라이트 회전
            float angle = Mathf.Atan2(m_DesiredDir.x, m_DesiredDir.y) * -Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
            Mid_Cannon.transform.rotation = angleAxis;

            Mid_Cannon.GetComponent<SpriteRenderer>().flipY = true;
            Mid_Cannon.GetComponent<SpriteRenderer>().flipX = true;
            Pattern_Dur += Time.deltaTime;
        }

        else 
        {
            Mid_Cannon.transform.rotation = Mid_Cannon.transform.rotation;
            

            if (Laser_Charge <= 1.5)
            {
                Laser_Charge += Time.deltaTime;
                Laser_Dur = 0;
            }
            else
            {
                if (Laser_Dur <= 3)
                {
                    Laser_Dur += Time.deltaTime;
                    Mid_Cannon_Laser.SetActive(true);
                }
                else
                {
                    Mid_Cannon_Laser.SetActive(false);
                    Laser_Charge = 0;
                    Pattern3_on = true;
                }
            }
            

            
        }

        //Mid_Cannon.GetComponentInChildren<Gun>().transform.rotation = angleAxis;
        //Mid_Cannon.GetComponentInChildren<Gun>().Autoshot = true;
    }

}
