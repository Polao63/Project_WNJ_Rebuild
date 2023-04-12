using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingStatus
{
    public static Dictionary<string, int> PlayerScore = new Dictionary<string, int>(); // 나중에 디폴트 값 넣자.
    public static Dictionary<string, int> SuperBomb = new Dictionary<string, int>();

    public static List<string> PlayerName = new List<string>();
}
