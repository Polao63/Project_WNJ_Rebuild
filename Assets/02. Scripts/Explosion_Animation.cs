﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Explosion_Animation : MonoBehaviour
{
    //public AudioClip Explosion_Sfx;
    public AudioClip Explosion_Boss_Sfx;


    void ExplosionDone()//폭발 애니메이션 종료뒤
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)//플레이어 폭발
        {
            GameObject.FindObjectOfType<Game_Manager>().Lives--;
            if (Game_Manager.Inst.Lives > 0)
            { Player_Ctrl.inst.Respawn(); }



            //GameObject.FindObjectOfType<Game_Manager>().Respawned = true;
            //if(GameObject.FindObjectOfType<Game_Manager>().Lives >= 1)
            //SceneManager.LoadScene(gameObject.scene.name);
        }
        Destroy(gameObject);
        
    }
}
