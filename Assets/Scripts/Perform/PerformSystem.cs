using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    ///  全局高度缩放指数，当前分辨率高度/1080
    /// </summary>
    static public float scaleGlobal = 1;
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
    /// <summary>
    /// 文本实例
    /// </summary>
    static public GameObject textObject;
    /// <summary>
    /// 当前游戏场景
    /// </summary>
    static private string currentGameScene;


    /************************
     *   以下是初始化方法   *
     ************************/
    /// <summary>
    /// 演出初始化
    /// </summary>
    static public void PerformInit()
    {
        scaleGlobal = Screen.height / 1080f;
        print("scaleGloabal = " + scaleGlobal);
        //计算黑幕高度缩放值
        float heightScale = mainCamera.GetComponent<Camera>().orthographicSize * 100 * 2;
        //缩放赋值
        blackBoard.transform.localScale = new Vector3(heightScale * Screen.width / Screen.height, heightScale, 1);
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
    /// <summary>
    /// 世界坐标转换为相对屏幕坐标
    /// </summary>
    /// <param name="world"></param>
    /// <returns></returns>
    static private Vector2 WorldToCanvas(Vector2 world)
    {
        Vector2 middlePoint = mainCamera.transform.position;
        return (world - middlePoint) * Screen.height / 2 / mainCamera.GetComponent<Camera>().orthographicSize;
    }
    /// <summary>
    /// 淡入淡出文本框
    /// </summary>
    /// <param name="inS">淡入时间</param>
    /// <param name="outS">淡出时间</param>
    /// <returns></returns>
    static private IEnumerator FadeText(GameObject tOb, float seconds, float inS = 0.5f, float outS = 1.0f)
    {
        TextFade fade = tOb.AddComponent<TextFade>();
        fade.FadeIn(inS);
        yield return new WaitForSeconds(inS);
        yield return new WaitForSeconds(seconds - inS - outS > 0 ? seconds - inS - outS : 0);
        TextFade fade2 = tOb.AddComponent<TextFade>();
        fade2.FadeOut(outS);
        yield return new WaitForSeconds(outS+0.1f);
        Destroy(tOb);
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
        print("Faded In");
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
        print("Hided");
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
        currentGameScene = to;
        SceneLoader.Load();
    }
    /// <summary>
    /// 智能切换场景
    /// </summary>
    /// <param name="to"></param>
    static public void LoadScene(string to)
    {
        LoadSceneFromTo(currentGameScene, to);
    }
    /// <summary>
    /// 自定义显示文字
    /// </summary>
    /// <param name="text">文字内容</param>
    /// <param name="seconds">文字持续秒数</param>
    /// <param name="fontSize">字体大小</param>
    /// <param name="apl">一行多少字</param>
    /// <param name="canvasPosition">相对屏幕位置</param>
    /// <param name="color">文字颜色</param>
    static public void ShowText(string text, float seconds, int fontSize, int apl, Vector2 canvasPosition, Color color, float inS = 0.5f, float outS = 1.0f)
    {
        GameObject tOb = GameObject.Instantiate(textObject, canvasPosition, Quaternion.identity, GameObject.FindGameObjectWithTag("UICanvas").transform);
        //生成完毕完毕，开始初始化赋值
        Text t = tOb.GetComponent<Text>();
        t.color = color;
        t.fontSize = fontSize;
        t.text = text;
        tOb.GetComponent<RectTransform>().anchoredPosition = canvasPosition * scaleGlobal;
        tOb.GetComponent<RectTransform>().sizeDelta = new Vector2(apl * fontSize, 200);
        tOb.GetComponent<RectTransform>().localScale = new Vector3(scaleGlobal, scaleGlobal, 1);
        mainCamera.StartCoroutine(FadeText(tOb, seconds, inS, outS));
    }
    /// <summary>
    /// 显示演出文字
    /// </summary>
    /// <param name="text"></param>
    /// <param name="seconds"></param>
    static public void ShowTextToPerform(string text, float seconds)
    {
        ShowText(text, seconds, 40, 20, Vector2.zero, Color.white);
    }
    /// <summary>
    /// 显示演出文字
    /// </summary>
    /// <param name="text"></param>
    /// <param name="seconds"></param>
    static public void ShowTextToPerform(string text, float seconds, Vector2 canvasPosition)
    {
        ShowText(text, seconds, 40, 20, canvasPosition, Color.white);
    }
}
