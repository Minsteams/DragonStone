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

    bool[] isEventDone;
    List<MyEvent> MyEvents=new List<MyEvent>();
    private void Update()
    {
        for (int i = 0; i < MyEvents.Count; i++)
        {
            if (!isEventDone[i] && MyEvents[i].isTrigged()) { StartCoroutine(MyEvents[i].Excute(isEventDone)); isEventDone[i] = true; }
        }
    }
    private void Start()
    {
        MyEvents.Add(new MyEvent01(MyEvents));
        isEventDone = new bool[10];
    }
    /// <summary>
    /// 事件表列
    /// </summary>
    class MyEvent01 : MyEvent
    {
        public MyEvent01(List<MyEvent> list)
        {

        }
        public override bool isTrigged()
        {
            return true;
        }
        public override IEnumerator Excute(bool[] isDone)
        {
            return base.Excute(isDone);
        }
    }
}
