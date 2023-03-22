using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_BG_Ctrl : MonoBehaviour
{
    public float ScrollSpeed = 0.2f;
    float Offset = 0.01f;
    MeshRenderer m_Renderer;

    // Start is called before the first frame update
    void Start()
    {
        m_Renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
       Offset += Time.deltaTime * ScrollSpeed;
       m_Renderer.material.mainTextureOffset = new Vector2(Offset, Offset);
    }
}
