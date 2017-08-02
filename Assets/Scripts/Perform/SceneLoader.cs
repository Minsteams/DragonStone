using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
/// <summary>
/// LoadingScene中的场景加载器
/// </summary>
public class SceneLoader : MonoBehaviour {
    /// <summary>
    /// 加载中的背景的父物体
    /// </summary>
    public GameObject BackGround;
    /// <summary>
    /// 进度条
    /// </summary>
    public GameObject Loader;
    /// <summary>
    /// 是否加载完成
    /// </summary>
    static bool isLoaded = false;
    /// <summary>
    /// 准备退出的场景
    /// </summary>
    static public string toUnload;
    /// <summary>
    /// 准备加载的场景
    /// </summary>
    static public string toLoad;
    /// <summary>
    /// 实际加载进度
    /// </summary>
    static private AsyncOperation loadingProgress;
    /// <summary>
    /// 虚拟加载进度
    /// </summary>
    static float fakeProgress = 0;

    /// <summary>
    /// 进度条效果
    /// </summary>
    /// <param name="progress">当前加载的进度</param>
    void LoaderPerform(float progress)
    {
        Vector3 t = Loader.transform.localScale;
        t.x = progress;
        Loader.transform.localScale = t;
    }
    /// <summary>
    /// 加载过程
    /// </summary>
    /// <returns></returns>
    private void Start()
    {
        StartCoroutine(Unloading());
    }
    
    IEnumerator Unloading()
    {
        //淡出
        BackGround.SetActive(false);
        float seconds = PerformSystem.Hide();
        yield return new WaitForSeconds(seconds);
        //淡出完毕,淡入
        PerformSystem.FocusOn(0, 0);
        if(SceneManager.GetSceneByName(toUnload)!=null) loadingProgress = SceneManager.UnloadSceneAsync(toUnload);
        if (SceneManager.GetSceneByName(toLoad) != null) loadingProgress = SceneManager.LoadSceneAsync(toLoad,LoadSceneMode.Additive);
        BackGround.SetActive(true);
        StartCoroutine(Loading());
        seconds = PerformSystem.FadeIn();
        yield return new WaitForSeconds(seconds);
        //淡入完毕，开始检测Loading进度
        yield return LoadingChecker();
    }
    IEnumerator Loading()
    {
        float progress = loadingProgress.progress;
        if (progress > fakeProgress)
        {
            progress = fakeProgress;
            fakeProgress += 0.01f;
        }
        print("Loading......"+ progress * 100 +"%");
        //循环检测loading进度
        if (progress < 1)
        {
            LoaderPerform(progress);
            yield return 1;
            yield return Loading();
        }
        else
        {
            print("Loading Complete!");
            //结束加载
            isLoaded = true;
            yield return 0;
        }
    }
    IEnumerator LoadingChecker()
    {
        if (isLoaded) yield return AfterLoading();
        else
        {
            yield return 1;
            yield return LoadingChecker();
        }
    }
    IEnumerator AfterLoading()
    {
        //淡出
        float seconds = PerformSystem.Hide();
        yield return new WaitForSeconds(seconds);
        //淡出结束,淡入
        seconds = PerformSystem.FadeIn();
        BackGround.SetActive(false);
        yield return new WaitForSeconds(seconds);
        //淡入完毕
        SceneManager.UnloadSceneAsync("LoadingScene");
        yield return 0;
    }
    /// <summary>
    /// 命令加载器开始加载
    /// </summary>
    static public void Load()
    {
        SceneManager.LoadScene("LoadingScene", LoadSceneMode.Additive);
        isLoaded = false;
    }
}
