using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 放在贡品上
/// </summary>
[DisallowMultipleComponent]
[AddComponentMenu("交互/贡品")]
[RequireComponent(typeof(BoxCollider))]
public class Tribute : MonoBehaviour
{
    [Header("标记贡品号码及调查文字")]
    [Range(1, 3)]
    public int number = 1;
    [Multiline(5)]
    public string text;
    static int numSelected = 0;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && GameSystem.isInteractingAllowed && GameSystem.gameStatus != GameSystem.status.pause)
        {
            print("Tribue Selected");
            if (numSelected != number)
            {
                numSelected = number;
                GameSystem.tipWord2.text = text;
            }
            else
            {
                numSelected = 0;
                GameSystem.tributeNum = number;
                GameSystem.tipWord2.text = "受人恩惠，替人行事。";
            }
        }
    }

}
