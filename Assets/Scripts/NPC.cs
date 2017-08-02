using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NPC父类
/// </summary>
[DisallowMultipleComponent]
public class NPC : MonoBehaviour {
    [Header("以下是NPC通用属性")]
    [Header("行走速度，Units/Seconds")]
    public float speed = 4;
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
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    /// <summary>
    /// 当前状态
    /// </summary>
    private NPCState currentState;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeState(new StandState());
    }

    public void WalkTo(float x)
    {

    }

    virtual public void Update()
    {
        v = Input.GetAxis("Horizontal");
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
