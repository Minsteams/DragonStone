using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 根据分辨率等比缩放
/// </summary>
[DisallowMultipleComponent]
[AddComponentMenu("演出/Scaler")]
public class Scaler : MonoBehaviour {
	void Start () {
        transform.localScale *= PerformSystem.scaleGlobal;
	}
}
