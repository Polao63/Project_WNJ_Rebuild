using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title_Manager : MonoBehaviour
{
    public GameObject Title = null;

    public Color newColor;
    public Color newColor2;
    public float fadeSpeed = 0.1f;
    public float delta = 0f;



    // Start is called before the first frame update
    void Start()
    {
        Title.SetActive(false);

        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        //newColor = Warning.color;
        //newColor2 = Warning_Info.color;

        delta = 10;
    }



    // Update is called once per frame
    void Update()
    {
        if (delta > 0)
        {
            delta -= Time.deltaTime;
        }

        //if (newColor.a < 1 && newColor2.a < 1 && !alphaDone)
        //{
        //    newColor.a += Time.deltaTime / 5;
        //    newColor2.a += Time.deltaTime / 5;
        //    Warning.color = newColor2;
        //    Warning_Info.color = newColor2;
        //    if (newColor.a >= 1 && newColor2.a >= 1)
        //    {
        //        alphaDone = true;
        //        booted = false;
        //    }
        //}

        //if (alphaDone == true)
        //{ delta += Time.deltaTime; }
        //else if (alphaDone == false)
        //{ delta = 0; }

        //if (newColor.a >= 0 && newColor2.a >= 0 && delta >= 5f)
        //{
        //    newColor.a -= Time.deltaTime / 5;
        //    newColor2.a -= Time.deltaTime / 5;
        //    Warning.color = newColor2;
        //    Warning_Info.color = newColor2;
        //    if (newColor.a <= 0 && newColor2.a <= 0)
        //    {
        //        alphaDone = false;
        //        
        //    }
        //}

        Title.SetActive(true);

        if (delta <= 0)
        {
            SceneManager.LoadScene("DemoScene");
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SceneManager.LoadScene("Cockpit_Scene");
        }

    }

    public void Start_Btn_Click()
    {
        SceneManager.LoadScene("Cockpit_Scene");
    }

}
