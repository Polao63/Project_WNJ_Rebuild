using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CockpitScene_UI_Mgr : MonoBehaviour
{
    public static Dictionary<int, string> SuperList_HelpText = new Dictionary<int, string>
    {
        {0, "메가레이저\n" + "- 크고 아름다운 레이져를 일정 시간동안 발사."},
        {1, "아토믹 크래시\n" + "- 적탄과 적기를 모두 파괴하는 원형파를 발사"},
        {2, "오버로드\n" + "- 일정 시간동안 기체의 능력치가 증가"},
        {3, "실드 리커버리\n" + "- 체력을 최대치까지 회복."},
        {4, "불릿 타임\n" + "- 나 자신을 제외하고 모든 기체, 탄이 느려진다."},
        {5, "럭키 3 \n" + "- 3개의 무작위 보상 아이템 효과를 일정시간동안 적용한다."}
    };

    public RawImage[] SuperBomb;
    public GameObject SuperBomb_Parent;
    bool Super_Selected = false;

    float delta = 0f;

    float Cursor_Pos;

    public float Timer = 19.9f;

    public Text Help_Text;
    public Text Timer_Text;

    int Cursor_num = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor_num = 0;
        Super_Selected = false;
        delta = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;

        Timer_Text.text = Timer.ToString("N2");

        if (Super_Selected == false)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Cursor_num--;
                if (Cursor_num <= 0)
                {
                    Cursor_num = 0;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Cursor_num++;
                if (Cursor_num > SuperBomb.Length - 1)
                {
                    Cursor_num = SuperBomb.Length - 1;
                }
            }

            for (int ii = 0; ii < SuperBomb.Length; ii++)
            {
                if (ii == Cursor_num)
                {
                    SuperBomb[ii].GetComponent<RawImage>().color = Color.white;
                    if (SuperBomb_Parent.transform.localPosition.x != (ii * -150))
                    {
                        SuperBomb_Parent.transform.localPosition = new Vector3((ii * -150), -378, 0);
                    }
                    Help_Text.text = SuperList_HelpText[ii];
                }
                else
                {
                    SuperBomb[ii].GetComponent<RawImage>().color = Color.gray;
                }
            }

            if (Input.GetKeyDown(KeyCode.Z) || Timer <= 0)
            {
                Super_Selected = true;

                switch (Cursor_num)
                {
                    case 0:
                        PlayerStatus.Selected_Super = SUPER_BOMB.MEGALASER;
                        break;
                    case 1:
                        PlayerStatus.Selected_Super = SUPER_BOMB.ATOMIC_WAVE;
                        break;
                    case 2:
                        PlayerStatus.Selected_Super = SUPER_BOMB.OVERLOAD;
                        break;
                    case 3:
                        PlayerStatus.Selected_Super = SUPER_BOMB.SHIELD_RECOVERY;
                        break;
                    case 4:
                        PlayerStatus.Selected_Super = SUPER_BOMB.ZE_WARUDO;
                        break;
                    case 5:
                        PlayerStatus.Selected_Super = SUPER_BOMB.LUCKY_3;
                        break;
                }
            }
        }

        

        if (Super_Selected)
        {
            Timer_Text.gameObject.SetActive(false);
            Help_Text.fontSize = 100;
            Help_Text.text = "OK!";

            if (SuperBomb[Cursor_num].rectTransform.position.x > 75f)
            { SuperBomb[Cursor_num].rectTransform.Translate(new Vector3(-10f, 0, 0)); }
            if (SuperBomb[Cursor_num].rectTransform.position.y > 80f)
            { SuperBomb[Cursor_num].rectTransform.Translate(new Vector3(0, -10f, 0)); }

            if (SuperBomb[Cursor_num].rectTransform.position.x <= 75f
                && SuperBomb[Cursor_num].rectTransform.position.y <= 80f
                && delta > 0)
            {
                delta -= Time.deltaTime;
                // Super_Selected = false;
            }
            else if (delta <= 0f)
            {
                //Debug.Log("게임 시작");
                SceneManager.LoadScene("Stage_1_1");
                SceneManager.LoadScene("Scene_Play", LoadSceneMode.Additive);
            }

        }

        


    }
}
