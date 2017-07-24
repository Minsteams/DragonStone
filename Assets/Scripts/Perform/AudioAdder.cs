using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("演出/添加音效")]
/// <summary>
/// 用于增加音效,挂在任意物体上
/// </summary>
public class AudioAdder : MonoBehaviour
{
    [Header("音效名称")]
    public string Aname;
    [Header("音效")]
    public AudioClip Aaudio;
    private void Awake()
    {
        if (Aname != "" && Aaudio != null)
        {
            PerformSystem.AddSound(Aname, Aaudio);
        }
    }
}
