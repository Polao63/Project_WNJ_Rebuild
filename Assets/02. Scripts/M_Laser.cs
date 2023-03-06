using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Laser : MonoBehaviour
{
    public bool Splash = true;

    private void Start()
    {
        Splash = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && Splash)
        {
            collision.gameObject.GetComponent<Enemy_Ctrl>().TakeDamage(1000);
        }
        
    }

    void SplashDamage()
    {
        Splash = false;
    }

    void ExplosionDone()
    {
        Destroy(gameObject);
    }
}
