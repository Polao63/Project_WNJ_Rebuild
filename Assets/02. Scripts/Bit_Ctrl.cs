using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bit_Ctrl : MonoBehaviour
{
    public float angle = 0;

    public float max_angle = 45f;

    public bool R_2 = false;

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
        if (angle < -360)
        { angle += 360f; }

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            {
                //if (Player_Ctrl.inst.h != 0)
                //{
                //    if (Mathf.Abs(angle) <= 180 && Mathf.Abs(angle) >= 0)
                //    {
                //        if (R_2) { angle -= Time.deltaTime * Player_Ctrl.inst.h * 500f; }
                //        else { angle += Time.deltaTime * Player_Ctrl.inst.h * 500f; }
                //    }
                //    if (angle > 180)
                //    {
                //        angle = 180;
                //    }
                //    else if (angle < -180)
                //    {
                //        angle = -180;
                //    }
                //}
                //if (Player_Ctrl.inst.v != 0)
                //{
                //    if (angle <= 180 && angle >= -180)
                //    {
                //        if (R_2) { angle -= Time.deltaTime * Player_Ctrl.inst.v * 500f; }
                //        else { angle += Time.deltaTime * Player_Ctrl.inst.v * 500f; }
                //    }
                //    if (angle > 180)
                //    {
                //        angle = 180;
                //    }
                //    else if (angle < -180)
                //    {
                //        angle = -180;
                //    }

                //}
            }

            
            if (Player_Ctrl.inst.v != 0)
            {
                if (angle <= max_angle && angle >= -max_angle)
                {
                    if (R_2) { angle -= Time.deltaTime * Player_Ctrl.inst.v * 500f; }
                    else { angle += Time.deltaTime * Player_Ctrl.inst.v * 500f; }
                }
                if (angle > max_angle)
                {
                    angle = max_angle;
                }
                else if (angle < -max_angle)
                {
                    angle = -max_angle;
                }
            }
        }




        RotUpdate(angle);
    }

    void RotUpdate(float bit_shootDir = 0)
    {
        transform.rotation = Quaternion.Euler(0f, 0f, bit_shootDir);
    }
}
