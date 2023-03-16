using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    int Bullet_Absorbed = 0;

    // Start is called before the first frame update
    void Start()
    {
        Bullet_Absorbed = 0;
        if (Player_Ctrl.inst.Nemesis_system == true)
            transform.localScale = new Vector3(30, 30, 30);
        else transform.localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player_Ctrl.inst.Nemesis_system == true)
        {
            transform.localScale -= Vector3.one * Time.deltaTime * 40f;
            if (transform.localScale.x <= 0f
                && transform.localScale.y <= 0f
                && transform.localScale.z <= 0f)
            {
                transform.localScale = new Vector3(30, 30, 30);
                gameObject.SetActive(false);
                transform.position = new Vector3(-5, 0, 0);

            }
        }
        else 
        {
            transform.localScale += Vector3.one * Time.deltaTime * 40f;
            if (transform.localScale.x >= 30f
                && transform.localScale.y >= 30f
                && transform.localScale.z >= 30f)
            {
                transform.localScale = new Vector3(1, 1, 1);
                gameObject.SetActive(false);
                transform.position = new Vector3(-5, 0, 0);

            }
        }
        
        if (Bullet_Absorbed > 6)
        { Bullet_Absorbed = 6; }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy_Bullet")
        {
            if (Player_Ctrl.inst.Nemesis_system == true)
            {
                Bullet_Absorbed++;
                Destroy(collision.gameObject);
            }
            else
            { Destroy(collision.gameObject); }
            
        }
        else if (collision.tag == "Enemy" && Player_Ctrl.inst.Nemesis_system == false)
        {
            collision.GetComponent<Enemy_Ctrl>().TakeDamage(999f,false);
        }
    }
}
