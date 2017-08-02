using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Data11 : MonoBehaviour
{
    public GameSystem.DailyData11 data;
    private void Awake()
    {
        GameSystem.dailyData11 = data;
    }

    bool[] isEventDone = new bool[10];
}
