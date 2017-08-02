using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("交互/火炬")]
[RequireComponent(typeof(BoxCollider))]
public class Torch : MonoBehaviour
{
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && GameSystem.isTorchActivated)
        {
            GameSystem.torchNum++;

            //to do

            Destroy(gameObject);
        }
    }
}
