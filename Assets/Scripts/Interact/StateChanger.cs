using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 简单的在不同状态切换的小脚本，挂在代表不同状态的子物体们的父物体上
/// </summary>
[AddComponentMenu("交互/状态切换者")]
[RequireComponent(typeof(Information))]
public class StateChanger : MonoBehaviour {
    [Header("是否有可附身物体在里面")]
    public bool isAnyHauntableThingInIt = false;
    
    
    [Header("如果有，可附身物体在哪个状态里？")]
    [ConditionalHide("isAnyHauntableThingInIt", false)]
    public int hauntableState;
    [ConditionalHide("isAnyHauntableThingInIt", true)]
    public HauntableObject Object;

    [Header("状态列表")]
    public GameObject[] States;

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
            if (isAnyHauntableThingInIt && hauntableState == currentState && HauntSystem.currentHauntedObject == Object)
            {
                blackH = GameObject.Instantiate(PerformSystem.blackHole, transform.position, Quaternion.identity, transform);
                GameSystem.condition[0] = true;
                PerformSystem.FadeIn(blackH,0.1f);
            }
            States[currentState].SetActive(false);
            currentState++;
            if (currentState >= States.Length) currentState = 0;
            States[currentState].SetActive(true);
            if (isAnyHauntableThingInIt && hauntableState == currentState && HauntSystem.currentHauntedObject == Object)
            {
                if (blackH != null) PerformSystem.FadeOut(blackH);
            }
        }
    }
}
