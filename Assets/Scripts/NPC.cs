using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// NPC父类
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
[AddComponentMenu("演出/NPC")]
public class NPC : MonoBehaviour {
    [Header("以下是NPC通用属性")]
    [Header("行走速度，Units/Seconds")]
    [SerializeField]
    float speed = 4;
    /// <summary>
    /// 脸是否初始朝向左
    /// </summary>
    [Header("脸部初始是否向左")]
    public bool facingLeft;
    /// <summary>
    /// 速度
    /// </summary>
    float v;
    /// <summary>
    /// 对应的animator
    /// </summary>
    public Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected GameObject speakText;
    /// <summary>
    /// 当前状态
    /// </summary>
    private NPCState currentState;


    public void Turn()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
    public void Speak(string word)
    {
        GameObject text = Resources.Load("System/SpeakText") as GameObject;
        speakText = GameObject.Instantiate(text, transform);
        speakText.GetComponent<Text>().text = word;
        PerformSystem.FadeInText(speakText);
    }
    [ContextMenu("ShutUp")]
    public void ShutUp()
    {
        if (speakText != null)
            PerformSystem.FadeOutText(speakText);
    }
    public void WalkTo(float x)
    {
        StartCoroutine(walkTo(x));
    }
    public IEnumerator walkTo(float x)
    {
        yield return 1;

        float distance = x - transform.position.x;
        if (distance< 0.1f && distance>-0.1f)
        {
            v = 0;
            yield return 0;
        }
        else if (Mathf.Abs(distance) < Mathf.Abs(v / 2))
        {
            v -= 0.05f * (distance > 0 ? 1 : -1);
            yield return walkTo(x);
        }
        else
        {
            v += 0.05f * (distance > 0 ? 1 : -1);
            if (v > 1) v = 1;
            if (v < -1) v = -1;
            yield return walkTo(x);
        }
    }





    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeState(new StandState());
    }
    private void Update()
    {
 //       v = Input.GetAxis("Horizontal");
        transform.position += Vector3.right * v * speed * Time.deltaTime;
        currentState.Excute(this);
    }
    protected void ChangeState(NPCState to)
    {
    //    print("【State Changed】 to " + to);
        if (currentState != null) currentState.Exit(this);
        currentState = to;
        currentState.Enter(this);
    }
    class StandState : NPCState
    {
        public override void Excute(NPC npc)
        {
            if (npc.v > 0.1f || npc.v < -0.1f) npc.ChangeState(new WalkState());
        }
    }
    class WalkState : NPCState
    {
        public override void Enter(NPC npc)
        {
            npc.spriteRenderer.flipX = (npc.v > 0) == npc.facingLeft;
            npc.animator.SetBool("isWalking", true);
        }
        public override void Excute(NPC npc)
        {
            if (npc.v < 0.1f && npc.v > -0.1f) npc.ChangeState(new StandState());
        }
        public override void Exit(NPC npc)
        {
            npc.animator.SetBool("isWalking", false);
        }
    }








    [ContextMenu("Speak")]
    void test1()
    {
        Speak("我是豆子！");
    }
    [ContextMenu("Walk to 10")]
    void test()
    {
        WalkTo(10);
    }


}
public class NPCState
{
    public virtual void Enter(NPC npc)
    {

    }
    public virtual void Excute(NPC npc)
    {

    }
    public virtual void Exit(NPC npc)
    {

    }
}
