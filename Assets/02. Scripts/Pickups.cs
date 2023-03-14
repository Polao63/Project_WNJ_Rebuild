using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if ((CameraResolution.m_ScreenWMin.y - 1f > transform.position.y))
        {//총알이 화면을 벗어나면 제거 
            Destroy(gameObject);
            PlayerStatus.Scavenger_Combo = 0;
            Debug.Log("Combo Reset");

        }
    }

}
