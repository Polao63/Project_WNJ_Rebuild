using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Item_Manager : MonoBehaviour
{
    public bool Item_Select = false;

    public GameObject Item_Select_Panel = null;
    public RawImage Item_Cursor = null;
    public int Cursor_num = 0;

    public float Timer = 20f;

    public Text Help_Text = null;
    public Text Timer_Text = null;

    public GameObject[] Item_Box;
    public RawImage[] Item_Box_Icon;
    public Sprite[] Icon;

    string Selected_Item;

    public int[] Item_array = new int[3];

    int item_code = 00;

    public static Item_Manager inst;

    int ii, jj;


    private void Awake()
    {
        inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Timer = 20f;
        Cursor_num = 0;
        //Item_array[0] = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Item_Select_Panel.SetActive(Item_Select);

        if (Item_Select == true)
        {
            if (Item_Select_Panel != null)
            {
                Panel_Update();
            }

            Timer_Update();

            Item_Random();

            for (int ii = 2; ii >= 0; ii--)
            {
                if (ii <= 0)
                { break; }

                if (Item_array[ii] == Item_array[ii - 1])
                {
                    Debug.Log("중복된 아이템이 있습니다. 코드를 다시 설정합니다." + "코드 : " + item_code.ToString());
                    Item_array[ii] = Item_Code_Random();
                }                
            }
            if (Item_array[2] == Item_array[0])
            {
                Debug.Log("중복된 아이템이 있습니다. 코드를 다시 설정합니다." + "코드 : " + item_code.ToString());
                Item_array[2] = Item_Code_Random();
            }

            if (Help_Text != null)
            {
                Help_Text.text = Item_Dictionary.ItemList_HelpText[Item_Dictionary.ItemCodeList.FirstOrDefault(x => x.Value == Item_array[Cursor_num]).Key.ToString()];
            }

            for (int i = 0; i < 3; i++)
            {
                Item_Box_Icon[i].GetComponent<RawImage>().texture 
                    = Icon[Item_Dictionary.ItemList_IconSpriteCode[Item_Dictionary.ItemCodeList.FirstOrDefault(x => x.Value == Item_array[i]).Key.ToString()]].texture;
            }
        }
        else
        {
            Timer = 20f;
            Cursor_num = 0;
            for (int i = 0; i < 3; i++)
            {
                Item_array[i] = 0;
            }
        }


    }

    void Panel_Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Cursor_num--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Cursor_num++;
        }

        if (Cursor_num > 2)
        {
            Cursor_num = 2;
        }
        else if (Cursor_num < 0)
        {
            Cursor_num = 0;
        }

        if (Item_Cursor != null)
        {
            Item_Cursor.gameObject.SetActive(true);
            for (int ii = 0; ii < Item_Box.Length; ii++)
            {
                if (ii == Cursor_num)
                { Item_Cursor.gameObject.transform.position = Item_Box[ii].transform.position; }
            }
        }

       

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Timer = 0f;
        }

        if (Timer <= 0f)
        {
            Selected_Item = Item_Dictionary.ItemCodeList.FirstOrDefault(x => x.Value == Item_array[Cursor_num]).Key;
            switch (Selected_Item)
            {
                case "Shot_Flame":
                    Player_Ctrl.inst.M_Weapon = Cur_Main_Weapon.Flame;
                    break;
                case "Shot_Rocket":
                    Player_Ctrl.inst.M_Weapon = Cur_Main_Weapon.Rocket;
                    break;
                case "Shot_Charge":
                    Player_Ctrl.inst.M_Weapon = Cur_Main_Weapon.ChargeShot;
                    break;


                case "Sub_Wide":
                    if (Player_Ctrl.inst.Sub_Weapon_On == false)
                    { Player_Ctrl.inst.Sub_Weapon_On = true; }
                    Player_Ctrl.inst.S_Weapon = Cur_Sub_Weapon.Wide;
                    break;
                case "Sub_Homing":
                    if (Player_Ctrl.inst.Sub_Weapon_On == false)
                    { Player_Ctrl.inst.Sub_Weapon_On = true; }
                    Player_Ctrl.inst.S_Weapon = Cur_Sub_Weapon.HomingMissile;
                    break;


                case "Op_Search":
                    if (Player_Ctrl.inst.Option_On == false)
                    { Player_Ctrl.inst.Option_On = true; }
                    Player_Ctrl.inst.C_option = Cur_Option.Search;
                    break;
                case "Op_Hold":
                    if (Player_Ctrl.inst.Option_On == false)
                    { Player_Ctrl.inst.Option_On = true; }
                    Player_Ctrl.inst.C_option = Cur_Option.Hold;
                    break;
                case "Op_Rolling":
                    if (Player_Ctrl.inst.Option_On == false)
                    { Player_Ctrl.inst.Option_On = true; }
                    Player_Ctrl.inst.C_option = Cur_Option.Rolling;
                    break;


                case "Pa_Sacavenger":
                    PlayerStatus.Scavenger = true;
                    PlayerStatus.Scavenger_Combo = 0;
                    break;
                case "Pa_Pity":
                    PlayerStatus.Pity = true;
                    break;
                case "Pa_Nemesis":
                    PlayerStatus.Nemesis = true;
                    break;


                case "Sy_Mimic":
                    PlayerStatus.Mimic = true;
                    break;
                case "Sy_H_Rocket":
                    PlayerStatus.H_Rocket = true;
                    break;
                case "Sy_ChargeBarrier":
                    PlayerStatus.ChargeBarrier = true;
                    break;
                case "Sy_ignition":
                    PlayerStatus.ignition = true;
                    break;


                case "Health_Up":
                    break;
                case "ATK_Up":
                    Player_Ctrl.inst.BulletDamage *= 1.2f;
                    break;
                case "Super_Up":
                    break;


            }
            Item_Dictionary.EquippedItemList[Selected_Item] = true;
        }
    }

    void Timer_Update()
    {
        if (Timer <= 0f)
        {
            Item_Select = false;
            Timer = 20f;
        }

        Timer -= Time.deltaTime;
        if (Timer_Text != null)
        {
            Timer_Text.text = Timer.ToString("N2");
        }
    }

    void Item_Random()
    {
        for (int i = 0; i < 3; i++)
        {
            if (Item_array[i] > 0)
            {
                continue;
            }

            Item_array[i] = Item_Code_Random();
        }
        
    }

    int Item_Code_Random()
    {
        ii = Random.Range(0, 6);
        jj = Random.Range(1, 5);

        item_code = (ii * 10 + jj);


        if (Item_Dictionary.ItemCodeList.ContainsValue(item_code) == false)
        {
            Debug.Log("없는 아이템코드 입니다. 코드를 다시 설정합니다." + "코드 : " + item_code.ToString());
            Item_Code_Random();
        }
        else if (Item_Dictionary.EquippedItemList[Item_Dictionary.ItemCodeList.FirstOrDefault(x => x.Value == item_code).Key.ToString()] == true)
        {
            Debug.Log("이미 장착한 아이템이 있습니다. 코드를 다시 설정합니다." + "코드 : " + item_code.ToString());
            Item_Code_Random();
        }

        if (item_code / 10 == 4)
        {
            switch (item_code)
            {
                case 41:
                    if (!(Player_Ctrl.inst.Option_On 
                        && Player_Ctrl.inst.C_option != Cur_Option.Rolling 
                        && Player_Ctrl.inst.M_Weapon != Cur_Main_Weapon.Normal))
                    {
                        Debug.Log("시너지 등장 조건이 맞지 않습니다. 코드를 다시 설정합니다." + "코드 : " + item_code.ToString());
                        Item_Code_Random();
                    }
                    
                    break;
                case 42:
                    if (!(Player_Ctrl.inst.Sub_Weapon_On
                        && Player_Ctrl.inst.S_Weapon == Cur_Sub_Weapon.HomingMissile
                        && Player_Ctrl.inst.M_Weapon == Cur_Main_Weapon.Rocket))
                    {
                        Debug.Log("시너지 등장 조건이 맞지 않습니다. 코드를 다시 설정합니다." + "코드 : " + item_code.ToString());
                        Item_Code_Random();
                    }
                    break;
                case 43:
                    if (!(Player_Ctrl.inst.Option_On
                        && Player_Ctrl.inst.C_option == Cur_Option.Rolling
                        && Player_Ctrl.inst.M_Weapon == Cur_Main_Weapon.ChargeShot))
                    {
                        Debug.Log("시너지 등장 조건이 맞지 않습니다. 코드를 다시 설정합니다." + "코드 : " + item_code.ToString());
                        Item_Code_Random();
                    }
                    break;
                case 44:
                    if (!(Player_Ctrl.inst.Sub_Weapon_On
                        && Player_Ctrl.inst.S_Weapon == Cur_Sub_Weapon.HomingMissile
                        && Player_Ctrl.inst.M_Weapon == Cur_Main_Weapon.Flame))
                    {
                        Debug.Log("시너지 등장 조건이 맞지 않습니다. 코드를 다시 설정합니다." + "코드 : " + item_code.ToString());
                        Item_Code_Random();
                    }
                    break;
            }
        }

        return item_code;
    }
}
