using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option_ChargeShot : MonoBehaviour
{
    bool Parts_None = true;
    [HideInInspector]public bool Hit = false;

    public float Damage = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy_Parts")
        {
            Enemy_Ctrl a_RefMon = collision.gameObject.GetComponent<Enemy_Ctrl>();
            if (a_RefMon != null)
            {
                a_RefMon.TakeDamage(Damage);
            }

            Hit = true;
            this.gameObject.SetActive(false);
        }

        if ((collision.tag == "Enemy"))
        {
            GameObject[] Enemy_Parts_Obj = GameObject.FindGameObjectsWithTag("Enemy_Parts");

            for (int i = 0; i < Enemy_Parts_Obj.Length; i++)
            {
                if (Enemy_Parts_Obj[i] != null)
                { Parts_None = false; }
                else { Parts_None = true; }
            }


            if (Parts_None == true)
            {
                Enemy_Ctrl a_RefMon = collision.gameObject.GetComponent<Enemy_Ctrl>();
                if (a_RefMon != null)
                {
                    a_RefMon.TakeDamage(Damage);
                }

            }

            Hit = true;
            this.gameObject.SetActive(false);
        }
    }
}
