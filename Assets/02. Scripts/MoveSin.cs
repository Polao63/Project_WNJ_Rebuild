using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSin : MonoBehaviour
{
    float sinCenterX;
    public float amplitude = 2;
    public float frequency = 2;
    public bool inverted;

    // Start is called before the first frame update
    void Start()
    {
        sinCenterX = transform.position.x;
    }

    // Update is called once per frame
    void Update()//사인파 형태로 이동
    {
        Vector2 pos = transform.position;

        float sin = Mathf.Sin(pos.y * frequency) * amplitude;

        if (inverted)
        {
            sin *= -1;
        }

        pos.x = sinCenterX + sin;

        transform.position = pos;

    }
}
