using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    /// 是否允许互动，用于演出时禁用交互
    /// </summary>
    static public bool isInteractingAllowed = true;
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
    /// 火炬是否能被熄灭
    /// </summary>
    static public bool isTorchActivated = false;
    /// <summary>
    /// 熄灭的火炬数
    /// </summary>
    static public int torchNum = 0;

    /// <summary>
    /// 生命
    /// </summary>
    static public int health = 9;
    static public Text tipWord1;
    static public Text tipWord2;
    /// <summary>
    /// 选择的贡品编号
    /// </summary>
    static public int tributeNum = 0;
    /// <summary>
    /// 是否选择好了天气
    /// </summary>
    static public bool isWeatherChoosen;
    static public float currentTime;
    [Header("时间流逝速度，小时/秒")]
    /// <summary>
    /// 时间流逝速度，小时/秒
    /// </summary>
    public float timeSpeed;
    /// <summary>
    /// 判断是否处于时间区间内
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    static public bool isTimeIn(float from, float to)
    {
        return currentTime >= from && currentTime <= to;
    }
    /// <summary>
    /// 条件
    /// </summary>
    static public bool[] condition = new bool[16];


    /************************
     *   以下是初始化方法   *
     ************************/
    /// <summary>
    /// 游戏初始化
    /// </summary>
    [RuntimeInitializeOnLoadMethod]
    static void GameInit()
    {
  //      SceneManager.LoadScene("Basic");
    }
    [Header("用于测试场景")]
    [Range(0,1)]
    public int sceneNum;
    private void Awake()
    {
        //初始化演出
        system = gameObject;
        PerformSystem.PerformInit();
        //初始化状态机并在状态机中进一步初始化
        switch (sceneNum)
        {
            case 0: ChangeState(new InitState()); break;
            case 1: ChangeState(new State14()); break;
        }

    }


    /************************
     *以下是流程控制方法属性*
     ************************/
    /// <summary>
    /// 当前状态
    /// </summary>
    State currentState;
    /// <summary>
    /// 切换状态
    /// </summary>
    /// <param name="to"></param>
    void ChangeState(State to)
    {
        print("【State Changed】 to " + to);
        if (currentState != null) currentState.Exit(this);
        currentState = to;
        currentState.Enter(this);
    }
    private void Update()
    {
        if(currentState!=null) currentState.Excute(this);
    }
    /// <summary>
    /// 状态机父类
    /// </summary>
    class State
    {
        public virtual void Enter(GameSystem system)
        {

        }
        public virtual void Excute(GameSystem system)
        {

        }
        public virtual void Exit(GameSystem system)
        {

        }
    }
    /// <summary>
    /// 初始化状态
    /// </summary>
    class InitState : State
    {
        public override void Enter(GameSystem system)
        {
            base.Enter(system);
            //判断是否是第一次进入游戏
            if (SaveSystem.LoadDayNum() > 0) system.ChangeState(new StartMenuState());
            else
            {
                //初始化
                currentProgress = Progress.neverStarted;
                //开始游戏
                system.ChangeState(new State10());
            }
        }
    }
    /// <summary>
    /// 开始菜单状态
    /// </summary>
    class StartMenuState : State
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
    IEnumerator Loading(IEnumerator toLoad)
    {
        if (SceneLoader.isLoaded)
        {
            print("Coroutine Loaded" + toLoad);
            yield return toLoad;
        }
        else
        {
            yield return new WaitForEndOfFrame();
            yield return Loading(toLoad);
        }
    }


    //第一天
    /// <summary>
    /// 第一天数据
    /// </summary>
    public static Data10.DailyData10 dailyData10;
    /// <summary>
    /// 开头演出
    /// </summary>
    class State10 : State
    {
        public override void Enter(GameSystem system)
        {
            //场景载入
            PerformSystem.LoadScene("Scene10");
            //其他
            system.LoadCoroutine(system.Scene10());
        }
    }
    IEnumerator Scene10()
    {
        dailyData10.title.SetActive(false);
        dailyData10.black.SetActive(true);
        PerformSystem.ShowTextToPerformForSeconds("「爷爷，为什么要给一块石头建一座庙呀？」", 5);
        yield return new WaitForSeconds(5);
        PerformSystem.ShowTextToPerformForSeconds("「那可不是普通的石头。那是龙的化身！」", 4);
        yield return new WaitForSeconds(4);
        dailyData10.title.SetActive(true);
        PerformSystem.FadeIn(dailyData10.title, 1.5f, 1);
        yield return new WaitForSeconds(2);
        PerformSystem.FadeOut(dailyData10.black, 4);
        yield return new WaitForSeconds(5);
        PerformSystem.FadeOut(dailyData10.title, 1.5f);
        yield return new WaitForSeconds(1);
        PerformSystem.ShowTextToPerformForSeconds("「它灵吗？」", 3, new Vector2(4, 0));
        yield return new WaitForSeconds(2);
        PerformSystem.ShowTextToPerformForSeconds("「当然了，心够诚，龙就能听到你的心愿。」", 5, new Vector2(6, -2));
        yield return new WaitForSeconds(4);
        PerformSystem.FadeOut(dailyData10.theGuy, 1);
        yield return new WaitForSeconds(2);
        ChangeState(new State11());
    }
    /// <summary>
    /// 引导熄灭火炬
    /// </summary>
    class State11 : State
    {
        float num = 0;
        GameObject tipText;
        public override void Enter(GameSystem system)
        {
            isTorchActivated = true;
            tipText = PerformSystem.ShowTextToPerform("点火炬熄灭之~");
            PerformSystem.FocusOn(dailyData10.firstTorch.position.x);
        }
        public override void Excute(GameSystem system)
        {
            if (torchNum > num)
            {
                num++;
                if (num > 3) system.ChangeState(new State12());
                else
                {
                    PerformSystem.FadeIn(dailyData10.backGround, 0.3f, 1 - 0.3f * num);
                    PerformSystem.FocusOn(dailyData10.firstTorch.position.x + dailyData10.torchDistance * num);
                }
            }
        }
        public override void Exit(GameSystem system)
        {
            torchNum = 0;
            isTorchActivated = false;
            PerformSystem.FadeOutText(tipText);
        }
    }
    /// <summary>
    /// 菜单
    /// </summary>
    class State12 : State
    {
        public override void Enter(GameSystem system)
        {
            system.StartCoroutine(system.Scene12());
        }
        public override void Excute(GameSystem system)
        {
            if (tributeNum != 0) system.ChangeState(new State13());
        }
    }
    IEnumerator Scene12()
    {
        dailyData10.menu.SetActive(true);
        dailyData10.Menu1.SetActive(true);
        dailyData10.Menu2.SetActive(false);
        float s = PerformSystem.Hide();
        yield return new WaitForSeconds(s);
        PerformSystem.FocusOn(dailyData10.menu.transform.position);
        PerformSystem.Hide(dailyData10.backGround, 0.3f);
        yield return new WaitForSeconds(1);
        PerformSystem.FadeIn(1);
        tributeNum = 0;
        tipWord1 = dailyData10.tipWord1;
        tipWord2 = dailyData10.tipWord2;
    }
    class State13 : State
    {
        public override void Enter(GameSystem system)
        {
            system.LoadCoroutine(system.Scene13());
            isWeatherChoosen = false;
        }
        public override void Excute(GameSystem system)
        {
            if (isWeatherChoosen) system.ChangeState(new State14());
        }
    }
    IEnumerator Scene13()
    {
        isInteractingAllowed = false;
        yield return new WaitForSeconds(1.5f);
        dailyData10.black2.SetActive(true);
        float seconds = PerformSystem.FadeIn(dailyData10.black2);
        yield return new WaitForSeconds(seconds);
        dailyData10.Menu1.SetActive(false);
        dailyData10.Menu2.SetActive(true);
        PerformSystem.Hide(dailyData10.black2);
        isInteractingAllowed = true;
        yield return 0;
    }


    /// <summary>
    /// 第一天数据
    /// </summary>
    public static Data11.DailyData11 dailyData11;
    class State14 : State
    {
        public override void Enter(GameSystem system)
        {
            PerformSystem.LoadScene("Scene11");
            currentTime = 0;
        }
        public override void Excute(GameSystem system)
        {
            currentTime += system.timeSpeed * Time.deltaTime;
            if (HauntSystem.currentHauntedObject != null) system.ChangeState(new State15());
        }
    }
    class State15 : State
    {
        public override void Enter(GameSystem system)
        {
            system.LoadCoroutine(system.Scene15());
        }
        public override void Excute(GameSystem system)
        {
            currentTime += system.timeSpeed * Time.deltaTime;
        }
    }
    IEnumerator Scene15()
    {
        yield return dailyData11.DouZi.walkTo(16);
        PerformSystem.FadeOut(dailyData11.DouZi.gameObject);
        PerformSystem.FadeOut(dailyData11.Bowl);
        while (true)
        {
            yield return new WaitForSeconds(15);
            string words = "";
            switch ((int)Random.Range(0, 5))
            {
                case 0:
                    words = "变喽……时代变喽……";
                    break;
                case 1:
                    words = "新鲜事物多喽……老头子都跟不上喽……";
                    break;
                case 2:
                    words = "还是以前的日子舒服唷……";
                    break;
                case 3:
                    words = "闲无事在敌楼我亮一亮琴音，我面前缺少个知音的人 ——";
                    break;
                case 4:
                    words = "就剩这间戏院喽……";
                    break;
            }
            yield return MyEvent.Speak(dailyData11.LaoHu, words, 5);
        }
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
    [ContextMenu("gameStatus")]
    void rGameStatus()
    {
        print("Game Status: " + gameStatus);
    }
    [ContextMenu("Time")]
    void rCurrentTime()
    {
        print("Current Time:" + currentTime);
    }
    [Header("用于设置时间")]
    [ContextMenuItem("SetTime", "SetTime")]
    public float timeToSet;
    void SetTime()
    {
        currentTime = timeToSet;
    }
}


