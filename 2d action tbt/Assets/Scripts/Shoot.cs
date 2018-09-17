//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Shoot : MonoBehaviour {

//    public GameObject PlayerStrike;
//    public GameObject bullet;
//    public Rigidbody2D bulletRB;
//    //public Collider2D enemyWound;
//    private float shootSpeed = 40.0f;
//    // script
//    //public EnemyDeath EnemyDeath;
//    public GameObject enemyGuy;
    
//	// Use this for initialization
//	void Start () {
//        //enemyWound = GetComponent<Collider2D>();

//    }
	
//	// Update is called once per frame
//	void Update () {
//        Fire();
//	}
    
//    void Fire()
//    {
//        if (Input.GetKeyDown(KeyCode.F))
//        {
//            Vector2 bulletSpawn = gameObject.transform.position; 
//            bulletSpawn.x += 1.2f;
//            // obj, v2, quaternion
//            bullet = Instantiate(PlayerStrike, bulletSpawn, transform.rotation) as GameObject;
//            // *avoid null reference exception (add script with refs)
//            bullet.AddComponent<EnemyDeath>();
//            // set the new component to the enemy object in the scene
//            // get component
//            EnemyDeath eRegister = bullet.GetComponent<EnemyDeath>();
//            // set component
//            eRegister.enemyG = enemyGuy;
            
//            // projectile launch
//            bulletRB = bullet.GetComponent<Rigidbody2D>();
//            bulletRB.velocity = new Vector2(shootSpeed,0);
                
            
//            //Destroy(strikeClone);
            
            
            
//        }
//    }

   
//}
