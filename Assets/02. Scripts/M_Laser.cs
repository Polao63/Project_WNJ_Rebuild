using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Laser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy_Ctrl>().TakeDamage(1000);
        }
    }
}
