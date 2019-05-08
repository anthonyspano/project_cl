using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour {

    public string levelToLoad;

    void OnCollisionEnter2D(Collision2D other)
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
