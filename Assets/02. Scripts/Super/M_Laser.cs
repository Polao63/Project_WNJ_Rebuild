using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Laser : MonoBehaviour
{
    public bool Splash;

    private void Start()
    {
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy_Ctrl>().TakeDamage(Player_Ctrl.inst.BulletDamage * 3,false);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy_Ctrl>().TakeDamage(Player_Ctrl.inst.BulletDamage*3,false);
        }
        
    }
}
