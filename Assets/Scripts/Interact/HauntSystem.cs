using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 附身系统
/// </summary>
public class HauntSystem : MonoBehaviour
{
    /// <summary>
    /// 当前被附身的物体
    /// </summary>
    static public HauntableObject currentHauntedObject = null;
    /// <summary>
    ///当前被附身的物体编号
    /// </summary>
    static public int Num;

    /// <summary>
    /// 附身于某物体
    /// </summary>
    /// <param name="target">附身目标</param>
    static public void Haunt(HauntableObject target)
    {
        currentHauntedObject = target;
        Num = target.Num;
        PerformSystem.FocusOn(currentHauntedObject.transform.position.x + currentHauntedObject.cameraX);
    }
}
