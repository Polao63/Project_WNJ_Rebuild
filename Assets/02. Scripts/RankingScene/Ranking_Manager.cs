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

    public Image Selected_Super_image;

    public Sprite[] Selected_Super_Icon;

    public char[] Rank_Name;

    string Rank_Name_str;

    // Start is called before the first frame update
    void Start()
    {
        k = 0;

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
        if (k < Rank_Name.Length)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                a++;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                a--;
            }
        }

        if(a>=65+26) { a = 65; }
        else if (a < 65) { a = (int)'Z'; }


        if (k == Rank_Name.Length)
        {
            Input_text.text = "END";
        }
        else
        {
            Input_text.text = "" + (char)a;
        }


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
