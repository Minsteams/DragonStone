using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 核心游戏数据
/// </summary>
public class GameSystem : MonoBehaviour
{
    /************************
     *   以下是全局的属性   *
     ************************/
    static GameObject system;
    /// <summary>
    /// 用于记录游戏状态
    /// </summary>
    public enum status
    {
        startmenu, inBasement, outside, pause
    }
    /// <summary>
    /// 当前游戏状态
    /// </summary>
    static public status gameStatus = status.outside;
    /// <summary>
    /// 黑幕
    /// </summary>
    public GameObject blackBoard;
    /// <summary>
    /// 用于记录游戏进度
    /// </summary>
    public enum Progress
    {
        neverStarted, day1
    }
    /// <summary>
    /// 当前游戏进度
    /// </summary>
    static public Progress currentProgress = Progress.neverStarted;
    /// <summary>
    /// 文本实例
    /// </summary>
    public GameObject textObject;


    /************************
     *   以下是初始化方法   *
     ************************/
    /// <summary>
    /// 游戏初始化
    /// </summary>
    [RuntimeInitializeOnLoadMethod]
    [ContextMenu("Init")]//不知道为啥这个指令没用，也许是静态的原因
    static void GameInit()
    {

    }
    private void Awake()
    {
        PerformSystem.textObject = textObject;
        system = gameObject;
        PerformSystem.blackBoard = blackBoard;
        PerformSystem.PerformInit();//初始化演出

        //判断是否是第一次进入游戏
        if (SaveSystem.LoadDayNum() > 0) ToStartMenu();
        else
        {
            //初始化
            currentProgress = Progress.neverStarted;
            //开始游戏
            GameStart();
        }
    }


    /************************
     *以下是流程控制方法属性*
     ************************/
    public void GameStart()
    {
        Debug.Log("游戏开始啦!");
        switch (currentProgress)
        {
            case Progress.neverStarted:

                //场景载入
                PerformSystem.LoadScene("Scene00");
                //其他
                LoadCoroutine(Scene00());
                break;
        }
    }
    public void ToStartMenu()
    {

    }
    /// <summary>
    /// 延迟开始协程
    /// </summary>
    /// <param name="toLoad"></param>
    void LoadCoroutine(IEnumerator toLoad)
    {
        StartCoroutine(Loading(toLoad));
    }
    /// <summary>
    /// 加载时用于检测是否加载完成
    /// </summary>
    /// <param name="toLoad"></param>
    /// <returns></returns>
    IEnumerator Loading (IEnumerator toLoad)
    {
        if (SceneLoader.isLoaded)
        {
            print("CoroutineLoaded");
            yield return toLoad;
        }
        else
        {
            yield return 1;
            yield return Loading(toLoad);
        }
    }
    /// <summary>
    /// 第一天数据
    /// </summary>
    [System.Serializable]
    struct DailyData1
    {
        public Transform parent;
        public GameObject title;
        public GameObject background;
    }
    [SerializeField]
    DailyData1 dailyData1;
    private IEnumerator Scene00()
    {
        yield return new WaitForSeconds(1);
        dailyData1.parent = GameObject.Find("SceneParent").transform;
        PerformSystem.ShowTextToPerform("「爷爷，为什么要给一块石头建一座庙呀？」", 5);
        yield return new WaitForSeconds(5);
        PerformSystem.ShowTextToPerform("「那可不是普通的石头。那是龙的化身！」", 4);
        yield return new WaitForSeconds(4);
        GameObject title = GameObject.Instantiate(dailyData1.title, dailyData1.parent);
        PerformSystem.FadeIn(title, 1.5f);
        yield return new WaitForSeconds(2);
        GameObject back = GameObject.Instantiate(dailyData1.background, dailyData1.parent);
        PerformSystem.FadeIn(back, 4);
        yield return new WaitForSeconds(5);
        PerformSystem.FadeOut(title, 1.5f);
        yield return new WaitForSeconds(1);
        PerformSystem.ShowTextToPerform("「它灵吗？」", 3, new Vector2(300, 150));
        yield return new WaitForSeconds(2);
        PerformSystem.ShowTextToPerform("「当然了，心够诚，龙就能听到你的心愿。」", 5, new Vector2(300, 90));
    }


    /************************
     *    以下是测试方法    *
     ************************/
    [ContextMenu("FadeIn")]
    void FadeIn()
    {
        PerformSystem.FadeIn();
    }
    [ContextMenu("FadeOut")]
    void FadeOut()
    {
        PerformSystem.FadeOut();
    }
    [ContextMenu("Hide")]
    void Hide()
    {
        PerformSystem.Hide();
    }
}
