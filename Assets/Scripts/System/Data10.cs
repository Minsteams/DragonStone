using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class Data10 : MonoBehaviour {
    [System.Serializable]
    public struct DailyData10
    {
        [Header("拖动SceneParent到这")]
        public Transform parent;
        [Header("龙石标题")]
        public GameObject title;
        [Header("黑幕，未激活状态")]
        public GameObject black;
        [Header("序人")]
        public GameObject theGuy;
        [Header("背景")]
        public GameObject backGround;
        [Header("火炬")]
        public Transform firstTorch;
        [Header("火炬间距离")]
        public float torchDistance;
        [Header("菜单")]
        public GameObject menu;
        [Header("提示文字")]
        public Text tipWord1;
        public Text tipWord2;
        [Header("菜单第一部分")]
        public GameObject Menu1;
        [Header("间幕【未激活】")]
        public GameObject black2;
        [Header("菜单第二部分【未激活】")]
        public GameObject Menu2;
    }
    public DailyData10 data;
    private void Awake()
    {
        GameSystem.dailyData10 = data;
    }
}
