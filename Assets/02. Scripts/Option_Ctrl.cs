using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option_Ctrl : MonoBehaviour
{
    public float angle = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player_Ctrl.inst.transform.position;

        angle += Time.deltaTime * 500f;

        if (360.0f < angle)
            angle -= 360.0f;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy_Bullet")
        {
            Destroy(collision.gameObject);
        }
    }
}
