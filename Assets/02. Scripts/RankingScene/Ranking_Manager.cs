using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ranking_Manager : MonoBehaviour
{
    int Cur_Letter = 65;

    int Inputed_Letters = 0;

    int PlayerScore = 0;

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

    private void Awake()
    {
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

    // Start is called before the first frame update
    void Start()
    {
        delta = 0;
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
        Rank_Update();

        if (delta > 0)
        {
            delta -= Time.deltaTime;
        }

        if (Timer_delta > 0)
        {
            Timer_delta -= Time.deltaTime;
        }

        Rank_NameEntry();
        

        if (Name_Done && delta <= 0)
        {
            SceneManager.LoadScene("TitleScene");
            Timer_Text.gameObject.SetActive(false);
        }

        Timer_Text.text = Timer_delta.ToString("N1");
        Rank_Name_text.text = Rank_Name_str;

        ScoreFormat();


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


    void Rank_Update()
    {
        List<int> scoreList = new List<int>();
        scoreList = RankingStatus.PlayerScore.Values.ToList();
        scoreList.Sort();
        scoreList.Reverse();

        for (int i = 0; i < RankingStatus.PlayerScore.Count; i++)
        {
            //if (i > 0)
            //{
            //    if (scoreList[i] == scoreList[i-1])
            //    {
            //        List<string> imsi = new List<string>();
            //        imsi.Add(RankingStatus.PlayerScore.FirstOrDefault(x => x.Value == scoreList[i-1]).Key.ToString());
            //        imsi.Add(RankingStatus.PlayerScore.FirstOrDefault(x => x.Value == scoreList[i]).Key.ToString());
            //        for (int ii = 0; ii < 2; ii++)
            //        {
            //            Debug.Log("imsi[" + ii.ToString() + "] : " + imsi[ii].ToString());
            //        }
            //        imsi.Sort();
            //        imsi.Reverse();
            //    }
            //}

            RankingStatus.PlayerName[i] = RankingStatus.PlayerScore.FirstOrDefault(x => x.Value == scoreList[i]).Key.ToString();

            Debug.Log(i+1 + "�� : " + RankingStatus.PlayerName[i].ToString());
            //RankingStatus.PlayerName.Add(RankingStatus.PlayerScore.Keys.ToString());
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            for (int i = 0; i < RankingStatus.PlayerScore.Count; i++)
            {
                Debug.Log(i + 1 + "�� : " + RankingStatus.PlayerName[i].ToString());
            }
        }







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
