using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 简单的在不同状态切换的小脚本，挂在代表不同状态的子物体们的父物体上
/// </summary>
[AddComponentMenu("交互/状态切换者")]
[RequireComponent(typeof(Information))]
public class StateChanger : MonoBehaviour {
    [Header("是否有可附身物体在里面？")]
    public bool isAnyHauntableThingInIt = false;
    
    
    [Header("如果有，可附身物体在哪个状态里？物体是哪个？")]
    [ConditionalHide("isAnyHauntableThingInIt")]
    public int hauntableState;
    [ConditionalHide("isAnyHauntableThingInIt")]
    public HauntableObject Object;

    [Space(40)]
    [Header("是否会触发条件？")]
    public bool isConditionTrigged = false;

    [Header("如果触发，在哪个状态中触发哪个条件？")]
    [ConditionalHide("isConditionTrigged")]
    public int state;
    [ConditionalHide("isConditionTrigged")]
    public int condition;
    [ConditionalHide("isConditionTrigged")]
    [Header("切换状态是否取消触发")]
    public bool isRound;

    [Space(40)]
    [Header("是否有特定状态不能操作？")]
    public bool isAnyStateUnable = false;

    [Header("如果有，是哪个状态？")]
    [ConditionalHide("isAnyStateUnable")]
    public int stateUnable;

    [Space(40)]
    [Header("状态列表")]
    public GameObject[] States;

    [HideInInspector]
    public bool isWork = true;
    int currentState = 0;
    GameObject blackH;
    private void Awake()
    {
        foreach(GameObject g in States)
        {
            g.SetActive(false);
        }
        States[0].SetActive(true);
        currentState = 0;
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && GameSystem.isInteractingAllowed)
        {
            if (isAnyStateUnable && currentState == stateUnable) return;
            ChangeState();
        }
    }
    public void ChangeState()
    {
        if (!isWork) return;
        if (isAnyHauntableThingInIt && hauntableState == currentState && HauntSystem.currentHauntedObject == Object)
        {
            blackH = GameObject.Instantiate(PerformSystem.blackHole, transform.position, Quaternion.identity, transform);
            GameSystem.condition[0] = true;
            PerformSystem.FadeIn(blackH, 0.1f);
        }
        if (isConditionTrigged && currentState == state && isRound) GameSystem.condition[condition] = false;
        if (States[currentState] != null) States[currentState].SetActive(false);




        currentState++;
        if (currentState >= States.Length) currentState = 0;
        if (States[currentState] != null) States[currentState].SetActive(true);
        if (isAnyHauntableThingInIt && hauntableState == currentState && HauntSystem.currentHauntedObject == Object)
        {
            if (blackH != null) PerformSystem.FadeOut(blackH);
        }
        if (isConditionTrigged && currentState == state) GameSystem.condition[condition] = true;
    }
}
