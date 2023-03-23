using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int shield_dur = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player_Ctrl.inst.Nemesis_system == true)
        {
            GetComponent<SpriteRenderer>().color = new Color32(255, 47, 108, 76);
            if (shield_dur >= 1)
            { shield_dur = 1; }
        }
        else 
        {
            if (shield_dur == 1) { GetComponent<SpriteRenderer>().color = new Color32(154, 255, 131, 76); }
            else if (shield_dur >= 2)
            {
                shield_dur = 2;
                GetComponent<SpriteRenderer>().color = new Color32(154, 255, 131, 137);
            }
        }
    }
}
