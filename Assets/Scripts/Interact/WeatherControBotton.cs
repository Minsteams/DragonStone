using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 天气控制按钮
/// </summary>
[DisallowMultipleComponent]
[AddComponentMenu("交互/天气控制按钮")]
[RequireComponent(typeof(BoxCollider))]
public class WeatherControBotton : MonoBehaviour
{
    void Start()
    {
        PerformSystem.HideImmediately(gameObject);
    }
    private void OnMouseEnter()
    {
        PerformSystem.FadeIn(gameObject, 0.1f);
    }
    private void OnMouseExit()
    {
        PerformSystem.Hide(gameObject, 0.1f);
    }
    private void OnMouseOver()
    {
        if (GameSystem.isInteractingAllowed && Input.GetMouseButtonDown(0)) GameSystem.isWeatherChoosen = true;
    }
}
