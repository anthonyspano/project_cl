using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    HealthSystem myHealth;
    bool reloadScene;
    float waitToReload;


    // Start is called before the first frame update
    void Start()
    {
        myHealth = GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // for respawning player
        if (reloadScene) 
        {
            waitToReload -= Time.deltaTime;
            if (waitToReload < 0)
            {
                SceneManager.LoadScene("world_map", LoadSceneMode.Single);
            }
        }
    }

    void Death()
    {
        if(myHealth.GetHealth() == 0)
        {
            reloadScene = true;
        }
    }
}
