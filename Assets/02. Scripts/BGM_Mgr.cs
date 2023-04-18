using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Mgr : MonoBehaviour
{
    int Stage_Num;

    AudioSource audioSource;
    public AudioClip[] STAGE_BGM;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Stage_Num = Game_Manager.Inst.Cur_stage-1;
        audioSource.clip = STAGE_BGM[Stage_Num];
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
