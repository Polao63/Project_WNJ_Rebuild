using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Dictionary
{
    public static Dictionary<string, int> ItemCodeList = new Dictionary<string, int>
    {
        {"Shot_Flame", 01},
        {"Shot_Rocket", 02},
        {"Shot_Charge", 03},


        {"Sub_Wide", 11},
        {"Sub_Homing", 12},


        {"Op_Search", 21},
        {"Op_Hold", 22},
        {"Op_Rolling", 23},


        {"Pa_Scavenger", 31},
        //{"Pa_Pity", 32},
        {"Pa_Nemesis", 33},


        {"Sy_Mimic", 41},
        {"Sy_H_Rocket", 42},
        {"Sy_ChargeBarrier", 43},
        //{"Sy_ignition", 44},

        //{"Health_Up", 51},
        {"ATK_Up", 52},
        {"Super_Up", 53}

    };

    public static Dictionary<string, bool> EquippedItemList = new Dictionary<string, bool>
    {
        {"Shot_Flame", false},
        {"Shot_Rocket", false},
        {"Shot_Charge", false},


        {"Sub_Wide", false},
        {"Sub_Homing", false},


        {"Op_Search", false},
        {"Op_Hold", false},
        {"Op_Rolling", false},


        {"Pa_Scavenger", false},
        {"Pa_Pity", false},
        {"Pa_Nemesis", false},


        {"Sy_Mimic", false},
        {"Sy_H_Rocket", false},
        {"Sy_ChargeBarrier", false},
        {"Sy_ignition", false},

        {"Health_Up", false},
        {"ATK_Up", false},
        {"Super_Up", false}

    };

    public static Dictionary<string, int> ItemList_IconSpriteCode = new Dictionary<string, int>
    {
        {"Shot_Flame", 0},
        {"Shot_Rocket", 1},
        {"Shot_Charge", 2},
        {"Sub_Wide", 3},
        {"Sub_Homing", 4},
        {"Op_Search", 5},
        {"Op_Hold", 6},
        {"Op_Rolling", 7},
        {"Pa_Scavenger", 8},
        {"Pa_Pity", 9},
        {"Pa_Nemesis", 10},
        {"Sy_Mimic", 11},
        {"Sy_H_Rocket", 12},
        {"Sy_ChargeBarrier", 13},
        {"Sy_ignition", 14},
        {"Health_Up", 15},
        {"ATK_Up", 16},
        {"Super_Up", 17}

    };

    public static Dictionary<string, string> ItemList_HelpText = new Dictionary<string, string>
    {
        {"Shot_Flame", "흔히 말하는 화염방사기. 사거리는 짧지만 강력한 데미지가 특징." + "- \" 잘 구워졌네요!\" "},
        {"Shot_Rocket", "적에게 닿으면 폭발해 스플래시 데미지를 준다." + "- \" 폭발은 예술이고 예술이 폭발이다.\" "},
        {"Shot_Charge", "일반 샷을 모아서 쏠수 있다." + "- \" 우주의 기운을 모아야 합니다.\" "},


        {"Sub_Wide", "넓은 범위를 커버할수 있는 보조무기." + "- \"요즘 시대에 와이드 샷이라니 너무 흔한거 아닌가??\" "},
        {"Sub_Homing", "느리지만 확실하게 조준하는 미사일 2발을 발사하는 보조무기." + "- \" 진짜 끈질기게 따라옵니다.\" "},


        {"Op_Search", "적을 추적하여 이동하는 보조 기체." + "- \" 여기는 나한테 맡겨! 라고 말하는 듯한 느낌이 든다.\" "},
        {"Op_Hold", "기체 주변에서 고정되어 보조 사격하는 옵션. 움직임에 따라 방향이 회전한다. \n (특수 액션 (홀드) : 방향을 고정할수 있다.)" + "- \"옆에 있으면 아주 든든한 친구입니다.\" "},
        {"Op_Rolling", "기체 주변을 원형으로 돌며 적탄을 방어함.  \n (특수 액션 (홀드) : 회전 속도가 빨라진다.) " + "- \"솔직히 좀 어지럽습니다.\" "},


        {"Pa_Scavenger", "적기가 이제 초필 게이지 수급 아이템을 드롭한다.(대신 이제 파괴만으로 수급 불가.)" + "- \" 원래는 기밀 제작중이던 시제품 폭격기에 달려있었던 기능이라고 한다.\" "},
        {"Pa_Pity", "전설 등급의 아이템이 3번 뒤에 반드시 나온다." + "- \" 익숙한 천장이다.\" "},
        {"Pa_Nemesis", "초필살기의 성능이 바뀐다. (메가레이저, 아토믹 웨이브, 실드 지원)\n" + "- \" 어떤 한 개발자가 심심풀이로 만든 무기 개조 모듈... 이라는것 같다.\" "},


        {"Sy_Mimic", "따라쟁이 - 보조기체가 본 기체의 샷을 따라하여 쏜다. (대신 성능이 조금 약화됨.)" + "- \" 이런걸 만들 여유가 있으면 새로운 기체나 만들란 말이다!\" "},
        {"Sy_H_Rocket", "호밍 로켓 - 호밍 미사일이 스플래시 데미지를 줄수 있게 된다." + "- \" ??? \" "},
        {"Sy_ChargeBarrier", "차지 베리어 - 일반 샷을 대신하여 보호 옵션 주변에 에너지를 충전해 적을 처리 할수 있다. (일반 샷 사용 불가)" + "- \" 쥐불놀이\" "},
        {"Sy_ignition", "이그니션 - 호밍 미사일에 플레임 샷으로 불을 붙여 미사일의 이동속도를 빠르게 한다. " + "- \" 잘 구워졌네요!\" "},


        {"Health_Up", "체력 추가 - 버틸수 있는 데미지가 증가." + "- \" 난이도 하락의 주범.\" "},
        {"ATK_Up", "탄 공격력 강화 - 일반 샷의 공격력을 증가." + "- \" 심플하지만 강한 능력.\" "},
        {"Super_Up", "초필 게이지 수급량 증가 - 게이지의 수급량 증가." + "- \" ??? \" "}

    };

}
