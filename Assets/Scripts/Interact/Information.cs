using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[AddComponentMenu("交互/基本信息")]
[DisallowMultipleComponent]
[RequireComponent(typeof(BoxCollider))]
/// <summary>
/// 物体基本交互信息
/// </summary>
public class Information : MonoBehaviour
{
    [Header("【这个组件里包含基本交互信息】")]
    [Space(10)]
    public bool 点击时播放音效 = false;
    [ConditionalHide("点击时播放音效")]
    public bool 根据名称播放 = false;

    [System.Serializable]
    public class Data
    {
        [ConditionalHide("根据名称播放", true, false)]
        public AudioClip audio;

        [Multiline(3)]
        [ConditionalHide("根据名称播放")]
        public string soundName;
    }
    [ConditionalHide("点击时播放音效")]
    public Data 音效数据;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (点击时播放音效)
            {
                if (根据名称播放) PerformSystem.Play(音效数据.soundName);
                else PerformSystem.Play(音效数据.audio);
            }
        }
    }
}

