using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("演出/摇椅")]
public class Shake : MonoBehaviour {
    [Header("重力加速度")]
    public float g = 1.6f;
    [Header("初始角度")]
    public float originalTheta = 13;
    float theta;
    float a;
    float v;
	// Use this for initialization
	void Start () {
        a = 0;
        v = 0;
        theta = originalTheta;
    }
	
	// Update is called once per frame
	void Update () {
        a = -g * Mathf.Sin(theta/360)/10;
        v += a;
        theta += v;
        transform.rotation = Quaternion.Euler(0, 0, theta);
	}
}
