using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 演出系统
/// <para>包括摄像机控制,音效控制等</para>
/// </summary>
public class PerformSystem : MonoBehaviour
{

    /************************
     *   以下是全局的属性   *
     ************************/
    /// <summary>
    /// 主相机，别调这个参数，没用的
    /// </summary>
    static public CameraController mainCamera;
    /// <summary>
    /// 用于将声音与名字对应的类
    /// </summary>
    public class sound
    {
        public string name;
        public AudioClip audio;
        public sound(string n, AudioClip a)
        {
            name = n;
            audio = a;
        }
    }
    /// <summary>
    /// 已有音效表列
    /// </summary>
    static public List<sound> soundList = null;



    /************************
     *   以下是初始化方法   *
     ************************/
    [RuntimeInitializeOnLoadMethod]//指定在游戏开始时运行
    static private void InitGame()
    {
        Debug.Log("游戏开始啦!");
    }


    /************************
     * 以下是内部的静态方法 *
     ************************/
    static private AudioClip NameToAudioClip(string name)
    {
        AudioClip audioToReturn = null;
        foreach (sound s in soundList)
        {
            if (s.name == name) audioToReturn = s.audio;
        }
        return audioToReturn;
    }
    static private IEnumerator PLayAndDestroyAudioDelayed(AudioSource toDestroy, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(toDestroy);
        yield return 0;
    }



    /************************
     * 以下是提供的静态方法 *
     ************************/
    /// <summary>
    /// 用于将摄像机聚焦到指定x坐标
    /// </summary>
    static public void FocusOn(float x)
    {
        mainCamera.focusPoint = new Vector2(x, mainCamera.focusPoint.y);
    }
    /// <summary>
    /// 用于将摄像机聚焦到指定坐标
    /// </summary>
    static public void FocusOn(float x, float y)
    {
        mainCamera.focusPoint = new Vector2(x, y);
    }
    static public void FocusOn(Vector2 point)
    {
        mainCamera.focusPoint = point;
    }
    /// <summary>
    /// 用于将摄像机聚焦到指定坐标
    /// </summary>
    static public void FocusOn(Vector3 point)
    {
        mainCamera.focusPoint = point;
    }
    /// <summary>
    /// 用于添加音效
    /// </summary>
    static public void AddSound(string name,AudioClip audio)
    {
        if (NameToAudioClip(name) == null) soundList.Add(new sound(name, audio));
    }
    /// <summary>
    /// 根据名字播放声音
    /// </summary>
    /// <param name="soundName">声音名字</param>
    static public void Play(string soundName)
    {
        if (soundName == "") return;
        AudioClip audio = NameToAudioClip(soundName);
        if (audio == null) return;
        AudioSource tempAudioSource = mainCamera.gameObject.AddComponent<AudioSource>();
        tempAudioSource.clip = audio;
        mainCamera.StartCoroutine(PLayAndDestroyAudioDelayed(tempAudioSource, audio.length));
    }
    /// <summary>
    /// 播放声音
    /// </summary>
    /// <param name="audio"></param>
    static public void Play(AudioClip audio)
    {
        if (audio == null) return;
        AudioSource tempAudioSource = mainCamera.gameObject.AddComponent<AudioSource>();
        tempAudioSource.clip = audio;
        mainCamera.StartCoroutine(PLayAndDestroyAudioDelayed(tempAudioSource, audio.length));
    }
}
