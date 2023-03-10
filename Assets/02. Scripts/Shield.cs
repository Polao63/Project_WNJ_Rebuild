using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    Animator ani;
    SpriteRenderer sp;

    public int Shield_Dur = 3;



    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ani.SetInteger("Shield_Dur", Shield_Dur);
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
        }
    }
}
