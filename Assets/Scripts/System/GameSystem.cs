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
        PerformSystem.PerformInit();//初始化演出
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
