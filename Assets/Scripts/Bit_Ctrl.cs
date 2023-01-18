using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bit_Ctrl : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RotUpdate(float bit_shootDir = 0)
    {
        transform.rotation = Quaternion.Euler(0f, 0f, bit_shootDir);
    }
}
