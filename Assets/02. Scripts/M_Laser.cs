using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Laser : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy_Ctrl>().TakeDamage(1000);
        }
    }
}
