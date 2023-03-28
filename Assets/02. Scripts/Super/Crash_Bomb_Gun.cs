using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crash_Bomb_Gun : MonoBehaviour
{
    public bool shoted = false;
    public GameObject m_BulletPrefab = null;
    public Cur_Main_Weapon imsi_M_Weapon;

    public static Crash_Bomb_Gun inst;

    public int Bullet_Absorbed = 0;

    private void Awake()
    {
        inst = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        shoted = false;
        imsi_M_Weapon = Player_Ctrl.inst.M_Weapon;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.X) == true && shoted == false)
        {
            GameObject a_CloneObj = Instantiate(m_BulletPrefab) as GameObject;
            a_CloneObj.transform.position = this.transform.position;
            a_CloneObj.GetComponent<Crash_Bomb>().Bomb_Remain_Time = Bullet_Absorbed;
            shoted = true;
        }
        if (shoted)
        {
            this.gameObject.SetActive(false);
        }
    }
}
