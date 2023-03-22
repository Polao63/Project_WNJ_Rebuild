using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus
{
    public static SUPER_BOMB Selected_Super;


    //--패시브 관련
    public static bool Scavenger = false;
    public static int Scavenger_Combo = 0;

    public static bool Pity = false;
    public static bool Nemesis = false;

    //--시너지 관련
    public static bool Mimic = false;
    public static bool H_Rocket = false;
    public static bool ChargeBarrier = false;
    public static bool ignition = false;

}
