using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    [ContextMenu("Test")]
    void Test()
    {
        PerformSystem.LoadSceneFromTo("test2", "test");
    }
    [ContextMenu("Test2")]
    void Test2()
    {
        PerformSystem.LoadSceneFromTo("test", "test2");
    }
}
