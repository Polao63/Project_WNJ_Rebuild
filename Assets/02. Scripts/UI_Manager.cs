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

    public GameObject Hide;

    public static UI_Manager Inst;

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
            if (Game_Manager.Inst.Coin > 0)
            { P1_ScoreText.text = "PRESS START"; }
            else if (Game_Manager.Inst.Coin == 0)
            { P1_ScoreText.text = "INSERT COIN"; }
        }
        else if (P1_ScoreText != null)
        { P1_ScoreText.text = Game_Manager.Inst.P1_score.ToString(); }

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

        if (Game_Manager.Inst.Super_Ready == false && Game_Manager.Inst.Super_ChargeStart == true)
        {
            Super_Icon.color = new Color32(128, 128, 128, 255);
            if (Game_Manager.Inst.fillamount_SuperGauge >= 0 && Game_Manager.Inst.fillamount_SuperGauge < 1)
            {
                Game_Manager.Inst.fillamount_SuperGauge += Time.deltaTime / Game_Manager.Inst.Super_Cooltime;
            }

        }
        else if (Game_Manager.Inst.Super_Ready == true)
        {
            Super_Icon.color = Color.white;
        }


    }

    void Gameover_Check()
    {
        if (Game_Manager.Inst.Lives <= 0)
        {
            Game_Manager.Inst.Lives = 0;
            Game_Manager.Inst.Pause = true;
            //Time.timeScale = 0f;
            GameOver.GetComponentInChildren<Text>().text = "GAME OVER";
            GameOver.SetActive(true);
            Timer += Time.deltaTime;
            if (Timer >= 3f)
            {

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

                if (CT_Time < 0)
                {
                    Timer = 0;
                    CT_Time = 9.9f;
                    GameOver.SetActive(false);
                    SceneManager.LoadScene(1);

                    Destroy(GameObject.Find("PlayerShip"));
                    Destroy(GameObject.Find("Game_Manager"));
                    Destroy(GameObject.Find("DontDestroy"));

                }


            }
        }
        else { GameOver.SetActive(false); }
    }
}
