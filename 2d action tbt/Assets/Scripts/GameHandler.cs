using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Management of the game states and storage
// Changing levels and saving/loading
public class GameHandler : MonoBehaviour {

    private GameObject badguy;
    private GameObject clone;

	private void Start ()
    {
        

        Spawn();
        //enemySpawn = new Transform(5, 5);
        //enemySpawn.position.x = 7f;


        //Debug.Log("Health: " + healthSystem.GetHealth());
        //healthSystem.Damage(10);
        //Debug.Log("Health: " + healthSystem.GetHealthPercent());
        //healthSystem.Heal(10);
        //Debug.Log("Health: " + healthSystem.GetHealth());
    }

    public T GetT<T>(T param)
    {
        return param;
    }

    private void Spawn()
    {
        // doesn't work
        //clone = Instantiate(badguy, enemySpawn);


    }
    
}

