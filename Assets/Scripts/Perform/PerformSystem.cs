using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 演出系统
/// <para>包括摄像机控制,音效控制，物体淡入淡出，场景切换等</para>
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
    static private List<sound> soundList = null;
    /// <summary>
    /// 黑幕样例
    /// </summary>
    static public GameObject blackBoard;
    /// <summary>
    /// 当前存在的黑幕
    /// </summary>
    static private GameObject currentBlackBoard;


    /************************
     *   以下是初始化方法   *
     ************************/
    /// <summary>
    /// 演出初始化
    /// </summary>
    static public void PerformInit()
    {
        blackBoard = GameObject.FindGameObjectWithTag("GameSystem").GetComponent<GameSystem>().blackBoard;
        float heightScale = mainCamera.GetComponent<Camera>().orthographicSize * 100 * 2;
        blackBoard.transform.localScale = new Vector3(heightScale * Screen.width / Screen.height, heightScale, 1);
        Debug.Log("游戏开始啦!");
    }


    /************************
     * 以下是内部的静态方法 *
     ************************/
    /// <summary>
    /// 通过名字查找音效切片
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    static private AudioClip NameToAudioClip(string name)
    {
        AudioClip audioToReturn = null;
        foreach (sound s in soundList)
        {
            if (s.name == name) audioToReturn = s.audio;
        }
        return audioToReturn;
    }
    /// <summary>
    /// 延迟销毁某物体
    /// </summary>
    /// <param name="toDestroy">目标</param>
    /// <param name="seconds">延迟的时间</param>
    /// <returns></returns>
    static private IEnumerator DestroyDelayed(Object toDestroy, float seconds)
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
    static public void AddSound(string name, AudioClip audio)
    {
        if (NameToAudioClip(name) == null) soundList.Add(new sound(name, audio));
    }
    /// <summary>
    /// 根据音效名字播放声音
    /// </summary>
    /// <param name="soundName">声音名字</param>
    static public void Play(string soundName)
    {
        if (soundName == "") return;
        AudioClip audio = NameToAudioClip(soundName);
        if (audio == null) return;
        AudioSource tempAudioSource = mainCamera.gameObject.AddComponent<AudioSource>();
        tempAudioSource.clip = audio;
        tempAudioSource.Play();
        mainCamera.StartCoroutine(DestroyDelayed(tempAudioSource, audio.length));
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
        mainCamera.StartCoroutine(DestroyDelayed(tempAudioSource, audio.length));
    }
    /// <summary>
    /// 使一个带spriteRenderer的物体以淡入的形式出现
    /// </summary>
    /// <param name="target">物体</param>
    /// <param name="seconds">淡出经历时间【可选】</param>
    static public float FadeIn(GameObject target, float seconds = 0.4f)
    {
        Fade fo = target.GetComponent<Fade>();
        if (fo != null) Destroy(fo);
        Fade f = target.AddComponent<Fade>();
        f = target.AddComponent<Fade>();
        f.FadeIn(seconds);
        return seconds;
    }
    /// <summary>
    /// 使一个带spriteRenderer的物体淡出并摧毁
    /// </summary>
    /// <param name="target">物体</param>
    /// <param name="seconds">淡出经历时间【可选】</param>
    static public float FadeOut(GameObject target, float seconds = 0.4f)
    {
        Fade fo = target.GetComponent<Fade>();
        if (fo != null) Destroy(fo);
        Fade f = target.AddComponent<Fade>();
        f.FadeOut(seconds);
        mainCamera.StartCoroutine(DestroyDelayed(target, seconds + 0.1f));
        return seconds;
    }
    /// <summary>
    /// 使一个带spriteRenderer的物体淡出但不摧毁
    /// </summary>
    /// <param name="target">物体</param>
    /// <param name="seconds">淡出经历时间【可选】</param>
    static public float Hide(GameObject target, float seconds = 0.4f)
    {
        Fade fo = target.GetComponent<Fade>();
        if (fo != null) Destroy(fo);
        Fade f = target.AddComponent<Fade>();
        f.FadeOut(seconds);
        return seconds;
    }
    /// <summary>
    /// 全屏淡入
    /// </summary>
    /// <param name="seconds"></param>
    static public float FadeIn(float seconds = 0.8f)
    {
        if (currentBlackBoard == null) currentBlackBoard = GameObject.Instantiate(blackBoard, mainCamera.focusPoint, Quaternion.identity, mainCamera.transform);
        FadeOut(currentBlackBoard, seconds);
        return seconds;
    }
    /// <summary>
    /// 全屏淡出
    /// </summary>
    /// <param name="seconds"></param>
    static public float FadeOut(float seconds = 0.8f)
    {
        if (currentBlackBoard != null) Destroy(currentBlackBoard);
        currentBlackBoard = GameObject.Instantiate(blackBoard, mainCamera.focusPoint, Quaternion.identity, mainCamera.transform);
        FadeIn(currentBlackBoard, seconds);
        mainCamera.StartCoroutine(DestroyDelayed(currentBlackBoard, seconds + 0.1f));
        return seconds;
    }
    /// <summary>
    /// 全屏淡出&保留黑幕
    /// </summary>
    /// <param name="seconds"></param>
    static public float Hide(float seconds = 0.8f)
    {
        if (currentBlackBoard != null) Destroy(currentBlackBoard);
        currentBlackBoard = GameObject.Instantiate(blackBoard, mainCamera.focusPoint, Quaternion.identity, mainCamera.transform);
        FadeIn(currentBlackBoard, seconds);
        return seconds;
    }
    /// <summary>
    /// 从某一场景切换到另一个
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    static public void LoadSceneFromTo(string from, string to)
    {
        SceneLoader.toUnload = from;
        SceneLoader.toLoad = to;
        SceneLoader.Load();
    }
}
