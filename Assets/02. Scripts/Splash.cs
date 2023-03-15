using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    public bool Splash_Damage;

    // Start is called before the first frame update
    void Start()
    {
        Splash_Damage = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Enemy_Ctrl>().TakeDamage(Player_Ctrl.inst.BulletDamage / 3);
    }

    void SplashDamage()
    {
        Splash_Damage = false;
    }

    void ExplosionDone()
    {
        Destroy(gameObject);
    }
}
