using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    //----- 스크린의 월드 좌표
    public static Vector3 m_ScreenWMin = new Vector3(-2.4f, -4.5f, 0f);
    public static Vector3 m_ScreenWMax = new Vector3(2.4f, 4.5f, 0f);

    //Start is called before the first frame update
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float scaleheight = ((float)Screen.width / Screen.height) / ((float)9 / 16);
        float scalewidth = 1 / scaleheight;

        if (scaleheight < 1)
        {
            rect.height = scaleheight;
            rect.y = (1 - scaleheight) / 2f;
        }
        else
        {
            rect.width = scalewidth;
            rect.x = (1 - scalewidth) / 2f;
        }
        camera.rect = rect;

        //----- 스크린의 월드 좌표 구하기
        Vector3 a_ScMin = new Vector3(0, 0, 0);
        m_ScreenWMin = camera.ViewportToWorldPoint(a_ScMin);
        //카메라 화면 좌측 하단코너의 월드 좌표

        Vector3 a_ScMax = new Vector3(1, 1, 1);
        m_ScreenWMax = camera.ViewportToWorldPoint(a_ScMax);
        //카메라 화면 우측상단 코너의 월드 좌표


    }

    // Update is called once per frame
    void Update()
    {

    }
}
