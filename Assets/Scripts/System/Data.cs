using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Data : MonoBehaviour
{
    [Range(0,10)]
    public int EventNum;
    protected bool[] isEventDone;
    protected bool[] isEventDoing;
    protected List<MyEvent> MyEvents = new List<MyEvent>();
    private void Update()
    {
        for (int i = 0; i < MyEvents.Count; i++)
        {
            if (!isEventDone[i] && !isEventDoing[i] && MyEvents[i].isTrigged(isEventDone, isEventDoing))
            {
                isEventDoing[i] = true;
                print("事件触发：" + i + MyEvents[i]);
                StartCoroutine(DoEvent(MyEvents[i].Excute(), i));
            }
        }
    }
    IEnumerator DoEvent(IEnumerator e,int i)
    {
        yield return e;
        isEventDoing[i] = false;
        isEventDone[i] = true;
        print("事件结束:"+i);
    }
    private void Start()
    {
        isEventDone = new bool[MyEvents.Count];
        isEventDoing = new bool[MyEvents.Count];
    }
}

public class MyEvent
{
    public virtual bool isTrigged(bool[] isDone, bool[] isDoing)
    {
        return false;
    }
    public virtual IEnumerator Excute()
    {
        yield return 0;
    }
    public static IEnumerator Speak(NPC npc, string words, float seconds)
    {
        npc.Speak(words);
        yield return new WaitForSeconds(seconds);
        npc.ShutUp();
    }
}