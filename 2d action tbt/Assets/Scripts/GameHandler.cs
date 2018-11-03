using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: 
/* Spawn lots of enemies
 * Point system for killing
 * 
*/

// Management of the game states and storage
// Changing levels and saving/loading
public class GameHandler : MonoBehaviour {

    public GameObject[] enemies;
    private int enemyCount;
    //private GameObject clone;
    public Transform enemySpawn;


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
        // Spawn an array of enemies
        Vector3 spawnPos = enemySpawn.position;
        for (int i = 0; i < enemies.Length; i++)
        {
            spawnPos += new Vector3(4, 0, 0);
            Debug.Log(spawnPos);
            Instantiate(enemies[i], spawnPos, Quaternion.identity);
        }

    }


}

