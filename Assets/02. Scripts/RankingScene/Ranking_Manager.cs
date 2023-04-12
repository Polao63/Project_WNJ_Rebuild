using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ranking_Manager : MonoBehaviour
{
    int PlayerScore = 0;

    public Text[] Rank_Name_text;
    public Text[] Rank_Score;
  


    public Image[] Selected_Super_image;

    public Sprite[] Selected_Super_Icon;

    

    string Rank_Name_str;

    float delta = 0;


    private void Awake()
    {
        //랭킹 초필살기 아이콘 관련 급히 수정해야함.

        //for (int i = 0; i < RankingStatus.PlayerScore.Count; i++)

        //switch (RankingStatus.SuperBomb[RankingStatus.PlayerScore.ToString])
        //{
        //    case SUPER_BOMB.MEGALASER:
        //            Selected_Super_image[i].sprite = Selected_Super_Icon[0];
        //        break;
        //    case SUPER_BOMB.ATOMIC_WAVE:
        //        Selected_Super_image[i].sprite = Selected_Super_Icon[1];
        //        break;
        //    case SUPER_BOMB.OVERLOAD:
        //        Selected_Super_image[i].sprite = Selected_Super_Icon[2];
        //        break;
        //    case SUPER_BOMB.SHIELD_RECOVERY:
        //        Selected_Super_image[i].sprite = Selected_Super_Icon[3];
        //        break;
        //    case SUPER_BOMB.ZE_WARUDO:
        //        Selected_Super_image[i].sprite = Selected_Super_Icon[4];
        //        break;
        //    case SUPER_BOMB.LUCKY_3:
        //        Selected_Super_image[i].sprite = Selected_Super_Icon[5];
        //        break;
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        delta = 0;

        PlayerScore = PlayerStatus.Player_Score;
    }

    // Update is called once per frame
    void Update()
    {
        Rank_Update();

        //if (delta > 0)
        //{
        //    delta -= Time.deltaTime;
        //}

        //if (Timer_delta > 0)
        //{
        //    Timer_delta -= Time.deltaTime;
        //}

        
        

        //if (Name_Done && delta <= 0)
        //{
        //    SceneManager.LoadScene("TitleScene");
        //    Timer_Text.gameObject.SetActive(false);
        //}

        //Timer_Text.text = Timer_delta.ToString("N1");
        //Rank_Name_text.text = Rank_Name_str;

        //ScoreFormat();


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

            ScoreFormat(i);

            RankingStatus.PlayerName[i] = RankingStatus.PlayerScore.FirstOrDefault(x => x.Value == scoreList[i]).Key.ToString();

            Debug.Log(i+1 + "등 : " + RankingStatus.PlayerName[i].ToString());
            //RankingStatus.PlayerName.Add(RankingStatus.PlayerScore.Keys.ToString());
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            for (int i = 0; i < RankingStatus.PlayerScore.Count; i++)
            {
                Debug.Log(i + 1 + "등 : " + RankingStatus.PlayerName[i].ToString());
            }
        }







    }


    public void ScoreFormat(int num)
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
        { Rank_Score[num].text = PlayerScore.ToString("D8"); }
        else
        { Rank_Score[num].text = PlayerScore.ToString("D" + Score_format.ToString()); }
    }
}
