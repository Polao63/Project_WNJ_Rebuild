using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += Vector3.one * Time.deltaTime * 40f;
        if (transform.localScale.x >= 25f
            && transform.localScale.y >= 25f
            && transform.localScale.z >= 25f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            gameObject.SetActive(false);
            transform.position = new Vector3(-5, 0, 0);
            
        }
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
