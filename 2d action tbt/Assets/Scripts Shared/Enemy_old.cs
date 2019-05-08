//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

///* TODO: 
// * 
// * 
// */

//public abstract class Enemy : MonoBehaviour
//{
//    //public BoxCollider2D bc;
//    //public GameObject player;

//    //protected float dX;
//    //protected float dY;

//    //private Vector3 tPos;
//    //private Vector3 setPos;
//    //private bool targetChosen;

//    //private GameObject targetPlace;
//    //private GameObject targetPrefab;

//    //private SpriteRenderer sr;

//    //public HealthSystem e_health;
//    //private int HP;
//    //private HealthBar e_healthBar;

//    protected virtual void Start()
//    {
//        //bc = GetComponent<BoxCollider2D>();
//        //player = GameObject.FindGameObjectWithTag("Player");
//        //sr = GetComponent<SpriteRenderer>();

//        //targetPrefab = Resources.Load<GameObject>("Prefabs/SpawnPoint");

//        //HP = 100;
//        ////e_health = new HealthSystem(HP);
//        //e_healthBar.Setup(e_health);




//    }

//    private void Update()
//    {

//    }

//    //protected virtual void Walk()
//    //{
//    //    //// move gameObject at a constant speed towards player
//    //    //transform.position = Vector2.MoveTowards(transform.position, tPos, 3f * Time.deltaTime);


//    //}



//    //protected void clearDecisionState()
//    //{
//    //    //// Attack()
//    //    //NextAttack = AttackRate;
//    //    //IsAttacking = false;
//    //    //Cooldown = AttackRate;
//    //    //// attackCheck()
//    //    //Wait = AttackRate;
//    //    //// Visual representation of decision state availability
//    //    ////sr.color = cGrey;
//    //}

//    //protected bool attackReady(ref float cd)
//    //{
//    //    if (cd <= 0)
//    //        return true;
        
//    //    else
//    //        return false;      
//    //}

//    //protected void Delay(ref float cd)
//    //{
//    //    //cd -= Time.deltaTime;
//    //}

//    //IEnumerator Attack()
//    //{
//    //    //Debug.Log(player.transform.position);
//    //    // "charge" attack if wait > 0 and able to attack
//    //    if (Wait > 0 && CanAttack && IsAttacking)
//    //    {
//    //        // Lock onto target
//    //        //sr.color = cRed;
//    //        // Determine target charge location
//    //        //setPos = LockOn(tPos);
//    //        //Debug.Log("Charge position: " + setPos);
//    //        yield return new WaitForSeconds(Wait -= Time.deltaTime);
//    //    }
//    //    else
//    //    {
//    //        if (CanAttack)
//    //        {
//    //            //Attack now
//    //            MoveToward(ChargeSpeed);
//    //        }
//    //        //clearDecisionState();
//    //    }
//    //}

//    //protected Vector3 LockOn(Vector3 cPos)
//    //{
//    //    if (!targetChosen)
//    //    {
//    //        // Create a placeholder for target position to charge to
//    //        targetPlace = Instantiate(targetPrefab, cPos, Quaternion.identity);
//    //        //Debug.Log("Target: " + targetPlace.transform.position);
//    //        //Debug.Log("cPos: " + cPos);
//    //        targetChosen = true;
//    //        //setPos = targetPrefab.transform.position;
//    //        //return targetPrefab.transform.position;
//    //    }

//    //    return targetPlace.transform.position;
//    //}

//    //protected virtual void MoveToward(float speed)
//    //{
//    //    // set lock on position
//    //    setPos = LockOn(tPos);

//    //    // travel to location
//    //    transform.position = Vector2.MoveTowards(transform.position, setPos, speed * Time.deltaTime);

//    //    Debug.Log("MoveToward");
//    //    //Debug.Log(transform.position);
//    //    clearDecisionState();
//    //}

//    //private void OnCollisionEnter2D(Collision2D collision)
//    //{
//    //   if (collision.gameObject.tag == "player")
//    //   {
//    //        e_health.Damage(25);
//    //   }
//    //}

//    //protected void LocatePlayer()
//    //{
//    //    tPos = player.transform.position;
//    //}

//    //protected float MoveSpeed { get; set; }

//    //protected float Wait { get; set; }

//    //protected float Cooldown { get; set; }

//    //protected bool CanAttack { get; set; }

//    //protected bool IsAttacking { get; set; }

//    //protected float AttackRate { get; set; }

//    //protected float AttackRange { get; set; }

//    //protected float EHealth { get; set; }

//    //protected float NextAttack { get; set; }

//    //protected float SightRange { get; set; }

//    //protected bool AttackNow { get; set; }

//    //protected float ChargeSpeed { get; set; }


//}
