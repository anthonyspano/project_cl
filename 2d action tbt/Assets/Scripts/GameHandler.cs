using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

    public HealthBar healthBar;
    private GameObject badguy;
    private GameObject clone;
    private Transform enemySpawn;

	private void Start ()
    {
        HealthSystem healthSystem = new HealthSystem(100);
        healthBar.Setup(healthSystem);

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
        clone = Instantiate(badguy, enemySpawn);


    }
    
}

