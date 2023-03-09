using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bit_Ctrl : MonoBehaviour
{
    public float angle = 0;

    public static Bit_Ctrl inst;

    void Awake()
    {
        inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        angle = transform.localRotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (360 < angle)
        { angle -= 360f; }

        if (Player_Ctrl.inst.h != 0)
        {
            if (angle <= 90 && angle >= -90)
            {
                angle += Time.deltaTime * Player_Ctrl.inst.h * 500f;
            }
            if (angle > 90)
            {
                angle = 90;
            }
            else if (angle < -90)
            {
                angle = -90;
            }
        }
        if (Player_Ctrl.inst.v != 0)
        {
            if (Mathf.Abs(angle) <= 180 && angle >= 0)
            {
                angle += Time.deltaTime * Player_Ctrl.inst.v * 500f;
            }
            if (angle > 180)
            {
                angle = 180;
            }
            else if (angle < 0)
            {
                angle = 0;
            }

        }
       

        RotUpdate(angle);
    }

    void RotUpdate(float bit_shootDir = 0)
    {
        transform.rotation = Quaternion.Euler(0f, 0f, bit_shootDir);
    }
}
