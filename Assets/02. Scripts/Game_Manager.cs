using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    Player_Ctrl player_Ctrl;

    public int P1_score = 0;
    public int Hi_score = 0;
    public int Coin = 0;
    public int Lives = 0;

    [HideInInspector]public int Cur_stage = 1;
    [HideInInspector]public int Cur_SubStage = 1;

    public bool Pause = false;
    
    [HideInInspector] public bool Super_Ready = false;
    [HideInInspector] public bool Super_ChargeStart = false;

    public float fillamount_SuperGauge = 0f;
    public float Super_Cooltime = 30f;

    public static Game_Manager Inst;

    public float delta = 0f;

    public bool Stage_Completed = false;

    AudioSource audioSource;

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
        Stage_Completed = false;

        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Pause = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        Coin = GlobalStatus.Coin;

        if (delta < 3f && PlayerStatus.Stage_Completed)
        { 
            delta += Time.deltaTime;
            if (delta >= 3)
            {
                Next_Scene();
            }
        }

        

        Score_Counter_Stop();

        if (Input.GetKeyDown(KeyCode.P))//디버그용 스테이지 스킵
        {
            Debug.Log(SceneManager.GetActiveScene().name);
            Next_Scene();
        }
        if (Input.GetKeyDown(KeyCode.O))//디버그용 스테이지 스킵
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Stage_1_5"));
        }

        if (Input.GetKeyDown(KeyCode.Q))//디버그용 스테이지 스킵
        {
            SceneManager.LoadScene("TitleScene");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerStatus.Scavenger = !PlayerStatus.Scavenger;
            Debug.Log("Scavenger : " + PlayerStatus.Scavenger.ToString());
        }

        if (Input.GetKeyDown(KeyCode.Keypad0) || Input.GetMouseButtonDown(1))//코인 투입
        {
            UI_Manager.Inst.CT_Time = 9.9f;
            if (GlobalStatus.Coin < 9) { GlobalStatus.Coin++; }
            audioSource.Play();

        }

        //스테이지 자동 이동
        if (SceneManager.GetActiveScene().ToString() != "Scene_Play" && (GameObject.Find("Boss_Scene_Manage") == null || GameObject.Find("Title_Manager") == null))
        {
            if (GameObject.FindWithTag("Enemy") == null
                && GameObject.FindWithTag("Effect") == null
                && GameObject.FindWithTag("Boss_PowerUp") == null
                && Item_Manager.inst.Item_Select_Panel.activeSelf == false)
            {
                if (PlayerStatus.Stage_Completed == false)
                {
                    Debug.Log("Stage_Completed : " + Stage_Completed);
                    PlayerStatus.Stage_Completed = true;
                }

            }
            else
            {
                PlayerStatus.Stage_Completed = false;
            }
        }
        

        if (fillamount_SuperGauge <= 0)
        {
            fillamount_SuperGauge = 0f;
            Super_ChargeStart = true;
        }
        else if (fillamount_SuperGauge >= 1)
        {
            fillamount_SuperGauge = 1f;
            Super_Ready = true;
            Super_ChargeStart = false;
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
        PlayerStatus.Stage_Completed = false;
        delta = 0;
        if (SceneManager.GetActiveScene().name.Contains("Stage_" + Cur_stage.ToString() + "_" + Cur_SubStage.ToString()))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            Cur_SubStage++;
            
            m_Dealy = 0.5f;
            if (Cur_SubStage > 10)
            {
                Cur_SubStage = 1;
                Cur_stage++;
            }
            Debug.Log("NEXT : Stage_" + Cur_stage.ToString() + "_" + Cur_SubStage.ToString());

        }
       
    }

    void Score_Counter_Stop()
    {
        if (P1_score >= 99999999)
        { P1_score = 99999999; }
        if (Hi_score >= 99999999)
        { Hi_score = 99999999; }
    }
}
