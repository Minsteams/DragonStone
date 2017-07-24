using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 附身系统
/// </summary>
public class HauntSystem : MonoBehaviour {
    static HauntableObject currentHauntedObject=null;//当前被附身的物体

    /// <summary>
    /// 附身于某物体
    /// </summary>
    /// <param name="target">附身目标</param>
    static public void Haunt(HauntableObject target)
    {
        currentHauntedObject = target;
        PerformSystem.FocusOn(currentHauntedObject.transform.position.x);
    }
}
