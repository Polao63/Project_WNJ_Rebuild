using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ranking_Manager : MonoBehaviour
{
    int Cur_Letter = 65;

    int Inputed_Letters = 0;

    public Text Input_text;
    public Text Rank_Name_text;
    public Text Rank_Score;
    public Text Input_text_Label;
    public Text Timer_Text;


    public Image Selected_Super_image;

    public Sprite[] Selected_Super_Icon;

    public char[] Rank_Name;

    string Rank_Name_str;

    float Timer_delta = 19.9f;

    float delta = 0;

    bool Name_Done = false;

    // Start is called before the first frame update
    void Start()
    {
        delta = 0;
        Timer_delta = 19.9f;
        Inputed_Letters = 0;
        Name_Done = false;

        Input_text_Label.text = "Input Your Name";
        Timer_Text.gameObject.SetActive(true);

        switch (PlayerStatus.Selected_Super)
        {
            case SUPER_BOMB.MEGALASER:
                Selected_Super_image.sprite = Selected_Super_Icon[0];
                break;
            case SUPER_BOMB.ATOMIC_WAVE:
                Selected_Super_image.sprite = Selected_Super_Icon[1];
                break;
            case SUPER_BOMB.OVERLOAD:
                Selected_Super_image.sprite = Selected_Super_Icon[2];
                break;
            case SUPER_BOMB.SHIELD_RECOVERY:
                Selected_Super_image.sprite = Selected_Super_Icon[3];
                break;
            case SUPER_BOMB.ZE_WARUDO:
                Selected_Super_image.sprite = Selected_Super_Icon[4];
                break;
            case SUPER_BOMB.LUCKY_3:
                Selected_Super_image.sprite = Selected_Super_Icon[5];
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (delta > 0)
        {
            delta -= Time.deltaTime;
        }

        if (Timer_delta > 0)
        {
            Timer_delta -= Time.deltaTime;
        }

        if (Inputed_Letters < Rank_Name.Length)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Cur_Letter++;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Cur_Letter--;
            }
        }

        if(Cur_Letter >=65+26) { Cur_Letter = 65; }
        else if (Cur_Letter < 65) { Cur_Letter = (int)'Z'; }


        if ((Inputed_Letters == Rank_Name.Length || Timer_delta <= 0) && Name_Done == false)
        {
            Input_text.text = "END";
            Input_text_Label.text = "Thank You!";

            for (int i = 0; i < Rank_Name.Length; i++)
            {
                if (Rank_Name[i] == '\0')
                {
                    Rank_Name[i] = ' ';
                }
            }
            if (Rank_Name[0] == ' ')
            {
                Debug.Log("RankNameIsNull");
                Rank_Name_str = "RND";
                AddData(Rank_Name_str, 0);
                Debug.Log(Rank_Name_str + " : " + RankingStatus.PlayerScore[Rank_Name_str]);
                Debug.Log(Rank_Name_str + " : " + RankingStatus.SuperBomb);
            }
            else 
            {
                Debug.Log("RankNameNotNull");
                AddData(Rank_Name_str, 0);
                Debug.Log(Rank_Name_str + " : " + RankingStatus.PlayerScore[Rank_Name_str]);
                Debug.Log(Rank_Name_str + " : " + RankingStatus.SuperBomb);
            }
            
            
            delta = 3;
            Name_Done = true;
        }
        else
        {
            Input_text.text = "" + (char)Cur_Letter;
        }


        if (Input.GetKeyDown(KeyCode.Z) && Inputed_Letters < Rank_Name.Length && Name_Done == false)
        {
            Rank_Name[Inputed_Letters] = (char)Cur_Letter;
            Rank_Name_str += "" + Rank_Name[Inputed_Letters];
            Inputed_Letters++;
        }

        if (Name_Done && delta <= 0)
        {
            SceneManager.LoadScene("IntroScene");
            Timer_Text.gameObject.SetActive(false);
        }

        Timer_Text.text = Timer_delta.ToString("N1");
        Rank_Name_text.text = Rank_Name_str;

    }

    public void AddData(string Name, int Score)
    {
        RankingStatus.PlayerScore.Add(Name, Score);
        RankingStatus.SuperBomb.Add(Name, PlayerStatus.Selected_Super.GetHashCode());
    }

}
