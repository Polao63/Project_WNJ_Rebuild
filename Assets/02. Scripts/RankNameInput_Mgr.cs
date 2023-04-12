using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class RankNameInput_Mgr : MonoBehaviour
{
    int PlayerScore = 0;

    public Text Input_text;
    public Text Rank_Name_text;
    public Text Rank_Score;
    public Text Input_text_Label;
    public Text Timer_Text;

    float delta = 0;

    public char[] Rank_Name;

    string Rank_Name_str;

    float Timer_delta = 19.9f;

    int Cur_Letter = 65;

    int Inputed_Letters = 0;

  

    bool Name_Done = false;

    // Start is called before the first frame update
    void Start()
    {
        Timer_delta = 19.9f;
        Inputed_Letters = 0;
        Name_Done = false;

        Input_text_Label.text = "Input Your Name";
        Timer_Text.gameObject.SetActive(true);

        PlayerScore = PlayerStatus.Player_Score;
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

        Rank_Name_text.text = Rank_Name_str;

        ScoreFormat();

        if (Name_Done && delta <= 0)
        {
            SceneManager.LoadScene("Ranking_Scene");
            Timer_Text.gameObject.SetActive(false);
        }

        Rank_NameEntry();

        Timer_Text.text = Timer_delta.ToString("N1");
    }


    void Rank_NameEntry()
    {
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

        if (Cur_Letter >= 65 + 26) { Cur_Letter = 65; }
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
                AddData(Rank_Name_str, PlayerScore);
                Debug.Log(Rank_Name_str + " : " + RankingStatus.PlayerScore[Rank_Name_str]);
                //Debug.Log(Rank_Name_str + " : " + RankingStatus.SuperBomb.ToString());
            }
            else
            {
                Debug.Log("RankNameNotNull");
                AddData(Rank_Name_str, PlayerScore);
                Debug.Log(Rank_Name_str + " : " + RankingStatus.PlayerScore[Rank_Name_str]);
                //Debug.Log(Rank_Name_str + " : " + RankingStatus.SuperBomb.ToString());
            }

            Timer_Text.gameObject.SetActive(false);
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
    }

    public void AddData(string Name, int Score)
    {
        if (RankingStatus.PlayerScore.ContainsValue(Score))
        {
            RankingStatus.PlayerScore.Remove(RankingStatus.PlayerScore.FirstOrDefault(x => x.Value == Score).Key.ToString());
        }

        RankingStatus.PlayerScore.Add(Name, Score);
        RankingStatus.SuperBomb.Add(Name, PlayerStatus.Selected_Super.GetHashCode());
        RankingStatus.PlayerName.Add(Name);

    }

    public void ScoreFormat()
    {
        int Score_format = 8;

        for (int ii = 7; ii > 0; ii--)
        {
            if (PlayerScore % (ii ^ 10) != 0)
            {
                Score_format = ii + 1;
                break;
            }
        }
        if (PlayerScore == 0)
        { Rank_Score.text = PlayerScore.ToString("D8"); }
        else
        { Rank_Score.text = PlayerScore.ToString("D" + Score_format.ToString()); }
    }
}
