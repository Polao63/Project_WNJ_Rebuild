using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    public Text Warning = null;
    public Text Warning_Info = null;

    bool alphaDone = false;

    public Color newColor;
    public Color newColor2;
    public float fadeSpeed = 0.1f;
    public float delta = 0f;



    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        newColor = Warning.color;
        newColor2 = Warning_Info.color;

        delta = 0;
        alphaDone = false;
    }



    // Update is called once per frame
    void Update()
    {
        if (newColor.a < 1 && newColor2.a < 1 && !alphaDone)//페이드 인
        {
            newColor.a += Time.deltaTime / 5;
            newColor2.a += Time.deltaTime / 5;
            Warning.color = newColor2;
            Warning_Info.color = newColor2;
            if (newColor.a >= 1 && newColor2.a >= 1)
            {
                alphaDone = true;
            }
        }

        if (alphaDone == true)
        { delta += Time.deltaTime; }
        else if (alphaDone == false)
        { delta = 0; }

        if (newColor.a >= 0 && newColor2.a >= 0 && delta >= 5f)// 페이드 아웃
        {
            newColor.a -= Time.deltaTime / 5;
            newColor2.a -= Time.deltaTime / 5;
            Warning.color = newColor2;
            Warning_Info.color = newColor2;
            if (newColor.a <= 0 && newColor2.a <= 0)
            {
                alphaDone = false;
                SceneManager.LoadScene(1);
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SceneManager.LoadScene("TitleScene");
        }


    }

}
