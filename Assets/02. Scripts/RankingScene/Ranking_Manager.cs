using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking_Manager : MonoBehaviour
{
    int a = 65;

    int k = 0;

    public Text Input_text;
    public Text Rank_Name_text;
    public Text Rank_Score;

    public char[] Rank_Name;

    string Rank_Name_str;

    // Start is called before the first frame update
    void Start()
    {
        k = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            a++;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            a--;
        }

        if(a>=65+26) { a = 65; }
        else if (a < 65) { a = (int)'Z'; }

        Input_text.text = "" + (char)a;

        if (Input.GetKeyDown(KeyCode.Z) && k < Rank_Name.Length)
        {
            Rank_Name[k] = (char)a;
            Rank_Name_str += "" + Rank_Name[k];
            k++;
            if (k == Rank_Name.Length)
            {
                AddData(Rank_Name_str,0);
                Debug.Log(Rank_Name_str + " : "  + RankingStatus.PlayerScore[Rank_Name_str]);
            }
        }
        

        Rank_Name_text.text = Rank_Name_str;

    }

    public void AddData(string Name, int Score)
    {
        RankingStatus.PlayerScore.Add(Name, Score);
    }

}
