using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Ctrl : MonoBehaviour
{
    public float ScrollSpeed = 0.2f;
    float Offset = 0.01f;
    MeshRenderer m_Renderer;
    Player_Ctrl player_Ctrl;

    // Start is called before the first frame update
    void Start()
    {
        m_Renderer = GetComponent<MeshRenderer>();
        player_Ctrl = GameObject.FindObjectOfType<Player_Ctrl>().GetComponent<Player_Ctrl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.FindObjectOfType<Game_Manager>().Pause)
        {
            Offset += Time.deltaTime * ScrollSpeed;
            //Offset += Time.deltaTime * ScrollSpeed * (player_Ctrl.gameObject.transform.position.y + 5f) / 2;
            m_Renderer.material.mainTextureOffset = new Vector2(0, Offset);
        }
    }
}
