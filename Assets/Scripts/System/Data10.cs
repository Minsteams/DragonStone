using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Data10 : MonoBehaviour {
    public GameSystem.DailyData10 data;
    private void Awake()
    {
        GameSystem.dailyData10 = data;
    }
}
