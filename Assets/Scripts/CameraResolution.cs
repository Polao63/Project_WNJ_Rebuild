using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    //----- ��ũ���� ���� ��ǥ
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

        //----- ��ũ���� ���� ��ǥ ���ϱ�
        Vector3 a_ScMin = new Vector3(0, 0, 0);
        m_ScreenWMin = camera.ViewportToWorldPoint(a_ScMin);
        //ī�޶� ȭ�� ���� �ϴ��ڳ��� ���� ��ǥ

        Vector3 a_ScMax = new Vector3(1, 1, 1);
        m_ScreenWMax = camera.ViewportToWorldPoint(a_ScMax);
        //ī�޶� ȭ�� ������� �ڳ��� ���� ��ǥ


    }

    // Update is called once per frame
    void Update()
    {

    }
}
