using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Mgr : MonoBehaviour
{
    int Stage_Num;

    AudioSource audioSource;
    public AudioClip[] STAGE_BGM;

    public AudioClip GameOver_BGM;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (UI_Manager.Inst.CT_Time < 0)
        {
            audioSource.clip = GameOver_BGM;
            audioSource.volume = 1;
            audioSource.loop = false;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else 
        {
            Stage_Num = Game_Manager.Inst.Cur_stage - 1;
            audioSource.clip = STAGE_BGM[Stage_Num];
            audioSource.volume = 0.38f;
            audioSource.loop = true;

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

        
    }
}