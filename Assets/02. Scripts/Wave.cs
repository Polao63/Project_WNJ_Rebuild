using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += Vector3.one * Time.deltaTime * 40f;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy_Bullet")
        {
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy_Ctrl>().TakeDamage(999f);
        }
    }
}
