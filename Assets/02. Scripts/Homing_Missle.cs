using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Homing_Missle : MonoBehaviour
{
    private void Awake()
    {
        var obj = FindObjectsOfType<Homing_Missle>();
        if (obj.Length > 2)
        {
            Destroy(gameObject);
        }
    }


    //����ź ����
    [HideInInspector] public bool IsTarget = false;
    //�ѹ��̶� Ÿ���� ������ �ִ°�?

    [HideInInspector] public GameObject Target_Obj = null;//Ÿ�� ���� ����
    Vector3 m_DesiredDir; //Ÿ���� ���ϴ� ���� ����

    Vector3 m_DirVec = Vector3.up;
    public float m_MoveSpeed = 15f;

    public bool isEnemyBullet = false;

    // Update is called once per frame
    void Update()
    {
        if ((CameraResolution.m_ScreenWMax.x + 1f < transform.position.x)
            || (CameraResolution.m_ScreenWMin.x - 1f > transform.position.x)
            || (CameraResolution.m_ScreenWMax.y + 1f < transform.position.y)
            || (CameraResolution.m_ScreenWMin.y - 1f > transform.position.y))
        {//�Ѿ��� ȭ���� ����� ���� 
            Destroy(gameObject);
        }


        if (Target_Obj == null && IsTarget == false)//������� �� Ÿ���� ������
        { FindEnemy(); }//Ÿ�� ã�� �Լ�

        if (Target_Obj != null)
        { BulletHoming(); }//Ÿ���� ���� ���� �̵��ϴ� �ൿ���� �Լ�
        else//Target Lost
        { transform.position += m_DirVec * Time.deltaTime * m_MoveSpeed; }
    }

    void FindEnemy()//Ÿ���� ã���ִ� �Լ�
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

            if (4f < a_CacDist)//�Ѿ˷κ��� 4 �ݰ� �ȿ� �ִ� ���͸�
            { continue; }

            a_FindMon = a_EnemyList[ii].gameObject;
            break;
        }

        Target_Obj = a_FindMon;
        if (Target_Obj != null)
        { IsTarget = true; }

    }

    void BulletHoming()//Ÿ���� ���� �����̵��ϴ� �ൿ���� �Լ�
    {
        m_DesiredDir = Target_Obj.transform.position - transform.position;
        m_DesiredDir.z = 0f;
        m_DesiredDir.Normalize();

        //���� ���� ȸ�� �̵��ϴ� �ڵ�

        //��������Ʈ ȸ��
        float angle = Mathf.Atan2(-m_DesiredDir.x, m_DesiredDir.y) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = angleAxis;


        m_DirVec = transform.up;
        transform.Translate(Vector3.up * m_MoveSpeed * Time.deltaTime);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && !isEnemyBullet)
        {
            Enemy_Ctrl a_RefMon = collision.gameObject.GetComponent<Enemy_Ctrl>();
            if (a_RefMon != null)
            {
                a_RefMon.TakeDamage(1000);
            }
            Destroy(gameObject);
        }
    }
}
