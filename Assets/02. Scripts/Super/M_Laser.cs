using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Laser : MonoBehaviour
{
    public bool Splash;

    bool Parts_None = true;


    private void Start()
    {
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy_Parts")
        {
            collision.gameObject.GetComponent<Enemy_Ctrl>().TakeDamage(Player_Ctrl.inst.BulletDamage * 3, false);
        }

        if (collision.tag == "Enemy")
        {
            GameObject[] Enemy_Parts_Obj = GameObject.FindGameObjectsWithTag("Enemy_Parts");

            for (int i = 0; i < Enemy_Parts_Obj.Length; i++)
            {
                if (Enemy_Parts_Obj[i] != null)
                { Parts_None = false; }
                else { Parts_None = true; }
            }

            if (Parts_None)
            {
                collision.gameObject.GetComponent<Enemy_Ctrl>().TakeDamage(Player_Ctrl.inst.BulletDamage * 3, false);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy_Parts")
        {
            collision.gameObject.GetComponent<Enemy_Ctrl>().TakeDamage(Player_Ctrl.inst.BulletDamage * 3, false);
        }

        if (collision.tag == "Enemy")
        {
            GameObject[] Enemy_Parts_Obj = GameObject.FindGameObjectsWithTag("Enemy_Parts");

            for (int i = 0; i < Enemy_Parts_Obj.Length; i++)
            {
                if (Enemy_Parts_Obj[i] != null)
                { Parts_None = false; }
                else { Parts_None = true; }
            }

            if (Parts_None)
            {
                collision.gameObject.GetComponent<Enemy_Ctrl>().TakeDamage(Player_Ctrl.inst.BulletDamage * 3, false);
            }
        }

    }
}
