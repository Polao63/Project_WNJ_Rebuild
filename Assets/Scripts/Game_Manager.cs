﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{

    public Text P1_ScoreText = null;
    public Text P2_ScoreText = null;
    public Text CoinText = null;

    public GameObject GameOver = null;

    public bool P1_In = true;
    public bool P2_In = false;

    public int P1_score = 0;
    public int P2_score = 0;
    public int Hi_score = 0;
    public int Coin = 0;
    public int Lives = 0;

    public bool Pause = false;

    float Timer = 0;

    float CT_Time = 9.9f;

    public static Game_Manager Inst;

    private void Awake()
    {
        //60프레임 유지
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        Inst = this;

        //스테이지 로드시 파괴되지 않게
        var obj = FindObjectsOfType<Game_Manager>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UI_Update();

        if (Input.GetKeyDown(KeyCode.P))//디버그용 스테이지 스킵
        { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }

        if (Input.GetKeyDown(KeyCode.Keypad0))//코인 투입
        { if (Coin < 9) { Coin++; } }

        if (Lives <= 0)
        {
            Pause = true;
            GameOver.SetActive(true);
            Timer += Time.deltaTime;
            if (Timer >= 3f)
            {
                GameOver.GetComponentInChildren<Text>().text = "CONTINUE?\n" + ((int)CT_Time).ToString();
                CT_Time -= Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.Keypad1) && Coin > 0)
                {
                    Coin--;
                    Pause = false;
                    Lives = 3;
                    Timer = 0;
                    CT_Time = 9.9f;
                    SceneManager.LoadScene(gameObject.scene.name);
                }

                if (CT_Time < 0)
                {
                    Timer = 0;
                    CT_Time = 9.9f;
                    SceneManager.LoadScene(1);

                    Destroy(GameObject.Find("PlayerShip"));
                    Destroy(GameObject.Find("Game_Manager"));
                    Destroy(GameObject.Find("DontDestroy"));

                }


            }
        }
        else { GameOver.SetActive(false); }

        //스테이지 자동 이동
        //if (GameObject.Find("Boss_Scene_Manage") == null || GameObject.Find("Title_Manager") == null)
        //{
        //    if (GameObject.FindWithTag("Enemy") == null
        //        && GameObject.FindWithTag("Effect") == null
        //        && GameObject.FindWithTag("PowerUP") == null
        //        && GameObject.FindWithTag("Bullet") == null
        //        && GameObject.FindWithTag("Player_Bullet") == null)
        //    { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }
        //}

    }

    void UI_Update()
    {
        //UI관련
        if (P1_ScoreText != null && P1_In != true)
        {
            if (Coin > 0)
            { P1_ScoreText.text = "PRESS START"; }
            else if (Coin == 0)
            { P1_ScoreText.text = "INSERT COIN"; }
        }
        else if (P1_ScoreText != null && P1_In == true)
        { P1_ScoreText.text = P1_score.ToString(); }

        if (P2_ScoreText != null && P2_In != true)
        {
            if (Coin > 0)
            { P2_ScoreText.text = "PRESS START"; }
            else if (Coin == 0)
            { P2_ScoreText.text = "INSERT COIN"; }
        }
        else if (P2_ScoreText != null && P2_In == true)
        { P2_ScoreText.text = P2_score.ToString(); }

        CoinText.text = "Credit(s) " + Coin.ToString();
    }
}