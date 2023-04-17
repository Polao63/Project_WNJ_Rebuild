using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ranking_Manager : MonoBehaviour
{

    public Text[] Rank_Name_text;
    public Text[] Rank_Score;
  


    public Image[] Selected_Super_image;

    public Sprite[] Selected_Super_Icon;

    

    string Rank_Name_str;

    float delta;

    // Start is called before the first frame update
    void Start()
    {
        delta = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        Rank_Update();

        if (delta > 0)
        {
            delta -= Time.deltaTime;
        }

        if (delta <= 0 || (Input.GetKeyDown(KeyCode.Z) && delta < 9))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }




    void Rank_Update()
    {
        List<int> scoreList = new List<int>();
        scoreList = RankingStatus.PlayerScore.Values.ToList();
        scoreList.Sort();
        scoreList.Reverse();

        for (int i = 0; i < RankingStatus.PlayerScore.Count; i++)
        {
            Debug.Log("Count : " + i);
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

            if (i < Rank_Score.Length)
            {

                ScoreFormat(i);

                RankingStatus.PlayerName[i] = RankingStatus.PlayerScore.FirstOrDefault(x => x.Value == scoreList[i]).Key.ToString();
                Rank_Name_text[i].text = RankingStatus.PlayerName[i].ToString();

                switch (RankingStatus.SuperBomb[RankingStatus.PlayerName[i]])
                {
                    case 0:
                        Selected_Super_image[i].sprite = Selected_Super_Icon[0];
                        break;
                    case 1:
                        Selected_Super_image[i].sprite = Selected_Super_Icon[1];
                        break;
                    case 2:
                        Selected_Super_image[i].sprite = Selected_Super_Icon[2];
                        break;
                    case 3:
                        Selected_Super_image[i].sprite = Selected_Super_Icon[3];
                        break;
                    case 4:
                        Selected_Super_image[i].sprite = Selected_Super_Icon[4];
                        break;
                    case 5:
                        Selected_Super_image[i].sprite = Selected_Super_Icon[5];
                        break;
                }

                Debug.Log(i + 1 + "µî : " + RankingStatus.PlayerName[i].ToString());

                
            }
            else
            {
                RankingStatus.PlayerName.Add(RankingStatus.PlayerScore.Keys.ToString());
                RankingStatus.PlayerScore.Remove(RankingStatus.PlayerName[i]);
                RankingStatus.SuperBomb.Remove(RankingStatus.PlayerName[i]);
                RankingStatus.PlayerName.RemoveAt(i);
                return;
            }



        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            for (int i = 0; i < RankingStatus.PlayerScore.Count; i++)
            {
                Debug.Log(i + 1 + "µî : " + RankingStatus.PlayerName[i].ToString());
            }
        }
    }


    public void ScoreFormat(int num)
    {
        int Score_format = 8;
        if (num < Rank_Score.Length && RankingStatus.PlayerScore.ContainsKey(RankingStatus.PlayerName[num]))
        {
            for (int ii = 7; ii > 0; ii--)
            {
                if (RankingStatus.PlayerScore[RankingStatus.PlayerName[num]] % (ii ^ 10) != 0)
                {
                    Score_format = ii + 1;
                    break;
                }
            }
            if (RankingStatus.PlayerScore[RankingStatus.PlayerName[num]] == 0)
            { Rank_Score[num].text = RankingStatus.PlayerScore[RankingStatus.PlayerName[num]].ToString("D8"); }
            else
            { Rank_Score[num].text = RankingStatus.PlayerScore[RankingStatus.PlayerName[num]].ToString("D" + Score_format.ToString()); }
        }
    }
}
