//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//// Manager for handling all enemies in the game
//public class EnemySpawn : Enemy
//{

//    public Transform pfHealthBar;


//    private void Awake()
//    {
//        Enemy<Acolyte> badguy = new Enemy<Acolyte>("badguy");
//        badguy.ScriptComponent.Initialize(

//            position: new Vector3(7, 0, 0)

//            );

//        HealthSystem healthSystem = new HealthSystem(100);
//        Transform healthBarTransform = Instantiate(pfHealthBar, new Vector3(0, 10), Quaternion.identity);
//        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
//        healthBar.Setup(healthSystem);
//    }

//    protected override void Patrol()
//    {

//    }

//}
