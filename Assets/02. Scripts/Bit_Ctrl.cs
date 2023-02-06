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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (360 < angle)
        { angle -= 360f; }

        RotUpdate(angle);
    }

    void RotUpdate(float bit_shootDir = 0)
    {
        transform.rotation = Quaternion.Euler(0f, 0f, bit_shootDir);
    }
}
