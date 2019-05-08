using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorHotkeys : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) Stop();
    }

    private void Stop()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #endif

    }
}
