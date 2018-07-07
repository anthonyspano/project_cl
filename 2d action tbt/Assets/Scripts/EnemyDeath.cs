using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour {

    public GameObject enemyG;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void enemyDied ()
    {
        Debug.Log("Enemy has died");
    }

    void OnCollisionEnter2D(Collision2D enemyHit)
    {
        if (enemyHit.gameObject.tag == "attack")
            enemyDied();
    }
}
