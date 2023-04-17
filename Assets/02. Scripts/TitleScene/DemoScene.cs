using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class DemoScene : MonoBehaviour
{
    public VideoPlayer player;
    public double time;
    public double currentTime;

    public Text Help_Text;

    bool alphaDone;

    public Color newColor;
    public float fadeSpeed = 0.1f;

    public AudioSource Audio;

    public static bool Coin_Inputed;

    // Start is called before the first frame update
    void Start()
    {
        player.loopPointReached += CheckOver;

        time = player.GetComponent<VideoPlayer>().clip.length;

        newColor = Help_Text.color;
    }

    // Update is called once per frame
    void Update()
    {
        HelpText_Manage();

        currentTime = player.GetComponent<VideoPlayer>().time;
        

        if (Input.GetKeyDown(KeyCode.Keypad0))//코인 투입
        {
            if (GlobalStatus.Coin < 9) { GlobalStatus.Coin++; }
            Coin_Inputed = true;
            SceneManager.LoadScene("TitleScene");
        }

    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene("TitleScene");
    }

    void HelpText_Manage()
    {
        if (newColor.a < 1 && !alphaDone)//페이드 인
        {
            newColor.a += Time.deltaTime / fadeSpeed;
            Help_Text.color = newColor;

            if (newColor.a >= 1)
            {
                alphaDone = true;
            }
        }
        else 
        {
            newColor.a -= Time.deltaTime / fadeSpeed;
            Help_Text.color = newColor;

            if (newColor.a <= 0)
            {
                alphaDone = false;
            }
        }

       
    }
}
