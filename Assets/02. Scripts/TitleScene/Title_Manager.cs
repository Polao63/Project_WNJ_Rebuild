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

    public Text Help_Text = null;
    public Text Coin_Text = null;

    bool alphaDone;

    public AudioSource Audio;

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

        Title.SetActive(true);

        HelpText_Manage();


        Coin_Text.text = "CREDIT(S) " + GlobalStatus.Coin.ToString();

        if (GlobalStatus.Coin > 0)
        { Help_Text.text = "PRESS START"; }
        else { Help_Text.text = "INSERT COIN"; }

        if (delta <= 0 && !(GlobalStatus.Coin > 0))
        {
            SceneManager.LoadScene("DemoScene");
        }

        if (Input.GetKeyDown(KeyCode.Keypad1) && GlobalStatus.Coin > 0)
        {
            GlobalStatus.Coin--;
            SceneManager.LoadScene("Cockpit_Scene");
        }

        if (Input.GetKeyDown(KeyCode.Keypad0))//코인 투입
        {
            Audio.Play();
            if (GlobalStatus.Coin < 9) { GlobalStatus.Coin++; }
        }

        if (GlobalStatus.Coin > 9 && DemoScene.Coin_Inputed) 
        { 
            Audio.Play(); 
            DemoScene.Coin_Inputed = false;
        }

    }

    //public void Start_Btn_Click()
    //{
    //    SceneManager.LoadScene("Cockpit_Scene");
    //}


    void HelpText_Manage()
    {
        if (newColor.a < 1 && !alphaDone)//페이드 인
        {
            newColor.a += Time.deltaTime / fadeSpeed;
            Help_Text.color = newColor;

            if (newColor.a >= 1)
            {
                alphaDone = true;
            }
        }
        else
        {
            newColor.a -= Time.deltaTime / fadeSpeed;
            Help_Text.color = newColor;

            if (newColor.a <= 0)
            {
                alphaDone = false;
            }
        }


    }
}
