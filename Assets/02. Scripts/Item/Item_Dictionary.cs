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
        {"Shot_Flame", "���� ���ϴ� ȭ������. ��Ÿ��� ª���� ������ �������� Ư¡." + "- \" �� �������׿�!\" "},
        {"Shot_Rocket", "������ ������ ������ ���÷��� �������� �ش�." + "- \" ������ �����̰� ������ �����̴�.\" "},
        {"Shot_Charge", "�Ϲ� ���� ��Ƽ� ��� �ִ�." + "- \" ������ ����� ��ƾ� �մϴ�.\" "},


        {"Sub_Wide", "���� ������ Ŀ���Ҽ� �ִ� ��������." + "- \"���� �ô뿡 ���̵� ���̶�� �ʹ� ���Ѱ� �ƴѰ�??\" "},
        {"Sub_Homing", "�������� Ȯ���ϰ� �����ϴ� �̻��� 2���� �߻��ϴ� ��������." + "- \" ��¥ ������� ����ɴϴ�.\" "},


        {"Op_Search", "���� �����Ͽ� �̵��ϴ� ���� ��ü." + "- \" ����� ������ �ð�! ��� ���ϴ� ���� ������ ���.\" "},
        {"Op_Hold", "��ü �ֺ����� �����Ǿ� ���� ����ϴ� �ɼ�. �����ӿ� ���� ������ ȸ���Ѵ�. \n (Ư�� �׼� (Ȧ��) : ������ �����Ҽ� �ִ�.)" + "- \"���� ������ ���� ����� ģ���Դϴ�.\" "},
        {"Op_Rolling", "��ü �ֺ��� �������� ���� ��ź�� �����.  \n (Ư�� �׼� (Ȧ��) : ȸ�� �ӵ��� ��������.) " + "- \"������ �� ���������ϴ�.\" "},


        {"Pa_Scavenger", "���Ⱑ ���� ���� ������ ���� �������� ����Ѵ�.(��� ���� �ı������� ���� �Ұ�.)" + "- \" ������ ��� �������̴� ����ǰ ���ݱ⿡ �޷��־��� ����̶�� �Ѵ�.\" "},
        {"Pa_Pity", "���� ����� �������� 3�� �ڿ� �ݵ�� ���´�." + "- \" �ͼ��� õ���̴�.\" "},
        {"Pa_Nemesis", "���ʻ���� ������ �ٲ��. (�ް�������, ����� ���̺�, �ǵ� ����)\n" + "- \" � �� �����ڰ� �ɽ�Ǯ�̷� ���� ���� ���� ���... �̶�°� ����.\" "},


        {"Sy_Mimic", "�������� - ������ü�� �� ��ü�� ���� �����Ͽ� ���. (��� ������ ���� ��ȭ��.)" + "- \" �̷��� ���� ������ ������ ���ο� ��ü�� ����� ���̴�!\" "},
        {"Sy_H_Rocket", "ȣ�� ���� - ȣ�� �̻����� ���÷��� �������� �ټ� �ְ� �ȴ�." + "- \" ??? \" "},
        {"Sy_ChargeBarrier", "���� ������ - �Ϲ� ���� ����Ͽ� ��ȣ �ɼ� �ֺ��� �������� ������ ���� ó�� �Ҽ� �ִ�. (�Ϲ� �� ��� �Ұ�)" + "- \" ��ҳ���\" "},
        {"Sy_ignition", "�̱״ϼ� - ȣ�� �̻��Ͽ� �÷��� ������ ���� �ٿ� �̻����� �̵��ӵ��� ������ �Ѵ�. " + "- \" �� �������׿�!\" "},


        {"Health_Up", "ü�� �߰� - ��ƿ�� �ִ� �������� ����." + "- \" ���̵� �϶��� �ֹ�.\" "},
        {"ATK_Up", "ź ���ݷ� ��ȭ - �Ϲ� ���� ���ݷ��� ����." + "- \" ���������� ���� �ɷ�.\" "},
        {"Super_Up", "���� ������ ���޷� ���� - �������� ���޷� ����." + "- \" ??? \" "}

    };

}
