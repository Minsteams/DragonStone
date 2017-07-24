using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 挂在主摄像机上，或者其副物体上，用于移动视角
/// <para>只能存在一个哦</para>
/// </summary>
[AddComponentMenu("演出/摄像机移动")]
[DisallowMultipleComponent]
public class CameraController : MonoBehaviour {
    /// <summary>摄像机移动的平滑度【越低越平滑】</summary>
    [Range(0.01f, 1.0f)]
    [Header("平滑度【越低越平滑】")]
    public float smoothness = 0.1f;
    /// <summary>摄像机的焦点</summary>
    [Header("焦点")]
    public Vector2 focusPoint = new Vector2(0, 0);

    //初始化，与演出系统建立联系
    private void Awake()
    {
        PerformSystem.mainCamera = this;
    }
    //平滑移动
    void Update () {
        transform.position += smoothness * (new Vector3(focusPoint.x, focusPoint.y, transform.position.z) - transform.position);
	}
}