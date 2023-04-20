using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public Text P1_ScoreText = null;
    public Text CoinText = null;
    public Text Lives_Text = null;

    public float Timer = 0;
    public float CT_Time = 9.9f;

    //public Image SpeedBar = null;

    public GameObject GameOver = null;

    public Sprite[] Sp_Super_Icon;

    public Image fill_SuperGauge = null;
    public RawImage Super_Icon;
    public Text SuperReady_Text = null;

    public GameObject Hide;
    public GameObject Boss_UI;
    public Image Boss_HP_Bar;

    public float Boss_MaxHP;
    public float Boss_CurHP;

    public static UI_Manager Inst;

    public int Score_format = 8;

    // Start is called before the first frame update
    void Start()
    {
        Inst = this;
        GameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UI_Update();
        Gameover_Check();
    }

    void UI_Update()
    {
        //UI°ü·Ã
        if (P1_ScoreText != null)
        {
            //if (GameOver.activeSelf)
            //{
            //    if (Game_Manager.Inst.Coin > 0)
            //    { P1_ScoreText.text = "PRESS START"; }
            //    else if (Game_Manager.Inst.Coin == 0)
            //    { P1_ScoreText.text = "INSERT COIN"; }
            //}

            for (int ii = 7; ii > 0; ii--)
            {
                if (Game_Manager.Inst.P1_score % (ii ^ 10) != 0)
                {
                    Score_format = ii+1;
                    break;
                }
            }
            if (Game_Manager.Inst.P1_score == 0)
            { P1_ScoreText.text = Game_Manager.Inst.P1_score.ToString("D8"); }
            else
            { P1_ScoreText.text = Game_Manager.Inst.P1_score.ToString("D" + Score_format.ToString()); }

            
        }

        CoinText.text = "Credit(s) " + Game_Manager.Inst.Coin.ToString();

        Lives_Text.text = "= " + (Game_Manager.Inst.Lives).ToString();

        if (GameObject.FindObjectOfType<Player_Ctrl>() != null)
        {
            if (GameObject.FindObjectOfType<Player_Ctrl>().transform.position.y > 4)
            {
                Hide.SetActive(false);
            }
            else
            { Hide.SetActive(true); }

            switch (GameObject.FindObjectOfType<Player_Ctrl>().SuperB)
            {
                case SUPER_BOMB.MEGALASER:
                    Super_Icon.texture = Sp_Super_Icon[0].texture;
                    break;
                case SUPER_BOMB.ATOMIC_WAVE:
                    Super_Icon.texture = Sp_Super_Icon[1].texture;
                    break;
                case SUPER_BOMB.OVERLOAD:
                    Super_Icon.texture = Sp_Super_Icon[2].texture;
                    break;
                case SUPER_BOMB.SHIELD_RECOVERY:
                    Super_Icon.texture = Sp_Super_Icon[3].texture;
                    break;
                case SUPER_BOMB.ZE_WARUDO:
                    Super_Icon.texture = Sp_Super_Icon[4].texture;
                    break;
                case SUPER_BOMB.LUCKY_3:
                    Super_Icon.texture = Sp_Super_Icon[5].texture;
                    break;

            }

        }
        else return;

        fill_SuperGauge.fillAmount = 1 - Game_Manager.Inst.fillamount_SuperGauge;

        Boss_HP_Bar.fillAmount = Boss_CurHP / Boss_MaxHP;
        

        if (Game_Manager.Inst.Super_Ready == false && Game_Manager.Inst.Super_ChargeStart == true)
        {
            
            Super_Icon.color = new Color32(128, 128, 128, 255);
            if (Game_Manager.Inst.fillamount_SuperGauge >= 0 && Game_Manager.Inst.fillamount_SuperGauge < 1)
            {
                Game_Manager.Inst.fillamount_SuperGauge += Time.deltaTime / Game_Manager.Inst.Super_Cooltime;
            }

        }
        
        if (Game_Manager.Inst.fillamount_SuperGauge >= 1)
        {
            SuperReady_Text.text = "SUPER READY!";
            SuperReady_Text.gameObject.SetActive(true);
            Super_Icon.color = Color.white;
        }

        if (Game_Manager.Inst.fillamount_SuperGauge > 0 && Game_Manager.Inst.fillamount_SuperGauge < 1)
        {
            SuperReady_Text.gameObject.SetActive(false);
        }


    }

    void Gameover_Check()
    {
        if (Game_Manager.Inst.Lives <= 0)
        {
            Game_Manager.Inst.Lives = 0;
            Game_Manager.Inst.Pause = true;
            //Time.timeScale = 0f;
            GameOver.SetActive(true);
            
            GameOver.GetComponentInChildren<Text>().text = "CONTINUE?\n" + ((int)CT_Time).ToString();
            CT_Time -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Keypad1) && Game_Manager.Inst.Coin > 0)
            {
                Game_Manager.Inst.Coin--;
                Game_Manager.Inst.Pause = false;
                Game_Manager.Inst.Lives = 3;
                Timer = 0;
                CT_Time = 9.9f;
                GameOver.SetActive(false);
                GameObject[] Bullet2Des = GameObject.FindGameObjectsWithTag("Enemy_Bullet");
                //Debug.Log(Bullet2Des.Length);
                for (int i = 0; i < Bullet2Des.Length; i++)
                {
                    Destroy(Bullet2Des[i]);
                }
                Player_Ctrl.inst.Respawn();
                //SceneManager.LoadScene(gameObject.scene.name);
            }

            if (Input.GetKeyDown(KeyCode.Z) && CT_Time < 8 && CT_Time > 0)
            {
                CT_Time--;
            }

            if (CT_Time < 0)
            {
                Timer += Time.deltaTime;
                GameOver.GetComponentInChildren<Text>().text = "GAME OVER";
                if (Timer >= 8f)
                {
                    Timer = 0;
                    CT_Time = 9.9f;
                    PlayerStatus.Player_Score = Game_Manager.Inst.P1_score;
                    //GameOver.SetActive(false);
                    SceneManager.LoadScene("RankNameInput_Scene");
                }
            }

        }
        else { GameOver.SetActive(false); }
    }
}
