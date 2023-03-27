using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolutaion : MonoBehaviour
{
    // 스크린의 월드 좌표
    public static Vector3 m_ScreenWMin = new Vector3(-10.0f, -5.0f, 0.0f);
    public static Vector3 m_ScreenWMax = new Vector3(10.0f, 5.0f, 0.0f);
    // 스크린의 월드 좌표

    // Start is called before the first frame update
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float scaleheight = ((float)Screen.width / Screen.height) /
                             ((float)900 / 1600);
        float scalewidth = 1.0f / scaleheight;

        if(scaleheight < 1.0f)
        {
            rect.height = scaleheight;
            rect.y = (1.0f - scaleheight) / 2.0f;
        }
        else
        {
            rect.width = scalewidth;
            rect.x = (1.0f - scalewidth) / 2.0f;
        }
        camera.rect = rect;

        //----- 스크린의 월드 좌표 구하기
        Vector3 a_ScMin = new Vector3(0.0f, 0.0f, 0.0f);
        m_ScreenWMin = camera.ViewportToWorldPoint(a_ScMin);
        //카메라 화면 좌측하단 코너의 월드 좌표

        Vector3 a_ScMax = new Vector3(1.0f, 1.0f, 1.0f);
        m_ScreenWMax = camera.ViewportToWorldPoint(a_ScMax);
        //카메라 화면 우측상단 코너의 월드 좌표
        //----- 스크린의 월드 좌표 구하기

    }//void Start()

    // Update is called once per frame
    void Update()
    {
        
    }
}
