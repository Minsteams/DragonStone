using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("交互/可附身的标记")]
[DisallowMultipleComponent]
[RequireComponent(typeof(Information))]
/// <summary>
/// 用于标记可被附身的物体
/// </summary>
public class HauntableObject : MonoBehaviour {
    [Header("【这个组件用来标记可被附身的物体】")]
    public bool isHauntable = true;
    [Header("【可选，物体编号】")]
    public int Num;
    [Header("附身后摄像机相对这个物体的x坐标")]
    public float cameraX;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && isHauntable && GameSystem.isInteractingAllowed)
        {
            HauntSystem.Haunt(this);
        }
    }
}
