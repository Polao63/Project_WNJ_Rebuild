using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    Player_Ctrl player_Ctrl;

    public float test_speed;

    public Text P1_ScoreText = null;
    public Text CoinText = null;
    public Text Lives_Text = null;

    //public Image SpeedBar = null;

    public GameObject GameOver = null;

    public bool P1_In = true;

    public int P1_score = 0;
    public int Hi_score = 0;
    public int Coin = 0;
    public int Lives = 0;
    public int Cur_stage = 1;
    int Cur_SubStage = 1;

    public bool Pause = false;

    public float Timer = 0;
    public float CT_Time = 9.9f;

    public static Game_Manager Inst;

    public GameObject Hide;

    private void Awake()
    {
        //60프레임 유지
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        Inst = this;

        //스테이지 로드시 파괴되지 않게
        //var obj = FindObjectsOfType<Game_Manager>();
        //if (obj.Length == 1)
        //{
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}

        player_Ctrl = GameObject.FindObjectOfType<Player_Ctrl>().GetComponent<Player_Ctrl>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Pause = false;
        GameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UI_Update();
        
        //test_speed = (0.2f * (player_Ctrl.gameObject.transform.position.y + 5f) / 2);


        if (Input.GetKeyDown(KeyCode.P))//디버그용 스테이지 스킵
        {
            Debug.Log(SceneManager.GetActiveScene().name);
            Next_Scene();
        }
        if (Input.GetKeyDown(KeyCode.O))//디버그용 스테이지 스킵
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Stage_1_2"));
        }

        if (Input.GetKeyDown(KeyCode.Keypad0))//코인 투입
        {
            CT_Time = 9.9f;
            if (Coin < 9) { Coin++; } 
        }

        if (Lives <= 0)
        {
            Lives = 0;
            Pause = true;
            //Time.timeScale = 0f;
            GameOver.GetComponentInChildren<Text>().text = "GAME OVER";
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

        //스테이지 자동 이동
        if (GameObject.Find("Boss_Scene_Manage") == null || GameObject.Find("Title_Manager") == null)
        {
            if (GameObject.FindWithTag("Enemy") == null
                && GameObject.FindWithTag("Effect") == null
                //&& GameObject.FindWithTag("PowerUP") == null
                && GameObject.FindWithTag("Enemy_Bullet") == null
                && GameObject.FindWithTag("Player_Bullet") == null)
            { Next_Scene(); }
        }

        if (0.0f < m_Dealy)
        {
            m_Dealy -= Time.deltaTime;
            if(m_Dealy <= 0)
            {
                if (SceneManager.GetSceneByName("Credits_Scene").isLoaded == false)
                {
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName("Stage_" + Cur_stage.ToString() + "_" + Cur_SubStage.ToString()));
                }
                else 
                {
                    SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Scene_Play").buildIndex); 
                }
                
            }
        }

    }

    float m_Dealy = 0.0f;
    public void Next_Scene()
    {
        if (SceneManager.GetActiveScene().name.Contains("Stage_" + Cur_stage.ToString() + "_" + Cur_SubStage.ToString()))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
           
            Cur_SubStage++;

            m_Dealy = 0.1f;
            if (Cur_SubStage >= 5)
            {
                Cur_SubStage = 1;
                Cur_stage++;
            }
        }
       
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

        CoinText.text = "Credit(s) " + Coin.ToString();

        Lives_Text.text = "= " + (Lives).ToString();

        if (GameObject.FindObjectOfType<Player_Ctrl>() != null)
        {
            if (GameObject.FindObjectOfType<Player_Ctrl>().transform.position.y > 4)
            {
                Hide.SetActive(false);
            }
            else
            { Hide.SetActive(true); }
        }
        else return;

        
        
    }
}
