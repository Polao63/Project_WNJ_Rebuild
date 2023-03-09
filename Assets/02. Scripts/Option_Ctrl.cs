using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Option_Type
{
    Search,
    Hold,
    Rolling
}

public class Option_Ctrl : MonoBehaviour
{
    Player_Ctrl m_RefHero = null;

    public Option_Type O_type;

    public float angle = 0f;
    float radius = 1.0f;

    public GameObject parent_Obj = null;
    Vector3 parent_Pos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (O_type == Option_Type.Hold)
        { }
        if (O_type == Option_Type.Hold)
        { }

        if (O_type == Option_Type.Rolling)
        { Rolling_Option(); }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy_Bullet")
        {
            Destroy(collision.gameObject);
        }
    }

    void Hold_Option()
    {
        
    }


    void Rolling_Option()
    {
        angle += Time.deltaTime * 500f;
        if (360.0f < angle)
            angle -= 360.0f;

        if (parent_Obj == null)
            return;

        parent_Pos = parent_Obj.transform.position;
        transform.position = parent_Pos +
                            new Vector3(radius * Mathf.Cos(angle * Mathf.Deg2Rad),
                                        radius * Mathf.Sin(angle * Mathf.Deg2Rad), 0.0f);

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }


    public void SubHeroSpawn(GameObject a_Paren, float a_Angle)
    {
        parent_Obj = a_Paren;
        m_RefHero = a_Paren.GetComponent<Player_Ctrl>();
        angle = a_Angle;
    }
}
