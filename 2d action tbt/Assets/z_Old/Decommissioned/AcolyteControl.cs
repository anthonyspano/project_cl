//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//// Behavior script for hooded enemies
///* Known issues:
// * Create a state of charging
// * not really consistent
// * 
// * 
// */

//// Handles actions of hooded enemy (prototype)
//public class DelayedAttackAI : MonoBehaviour
//{
//    public bool isMoving;
//    public Vector3 tPos;
//    public float wait;    // wait period before attack (turns red)
//    public bool isAttacking;
//    public Color cRed;
//    public Color cGrey;
//    private SpriteRenderer sr;
//    // Object-specific timers
//    [SerializeField]
//    private float coolDown;  // central timer for attack cooldown
//    public bool canAttack;
//    private float nextAttack;   // time before enemy can attack again
//    [SerializeField]
//    public float attackRate;
//    [SerializeField]
//    public float attackRange;
//    private HealthBar healthBar;
//    private HealthSystem healthSystem;
//    private float dX;
//    private float dY;
//    public float range;
//    public GameObject player;
//    private HealthBar pHealthBar;
//    [SerializeField]
//    private int health;
//    public Vector2 chargePos;
//    private BoxCollider2D bc;
//    private Animator anim;
//    public GameObject followTarget;
//    [SerializeField]
//    private float sightRange;
//    // target to lock on to
//    public GameObject targetPrefab;
//    // Charge
//    [SerializeField]
//    private Vector3 setPos;
//    private bool targetChosen;
//    private GameObject targetPlace;
//    [SerializeField]
//    private float normalSpeed;
//    [SerializeField]
//    private float chargeSpeed;
//    public GameObject hitbox;

//    public Transform targetDebug;




//    void Start()
//    {
//        healthBar = GetComponentInChildren<HealthBar>();
//        sr = GetComponent<SpriteRenderer>();
//        player = GameObject.FindGameObjectWithTag("Player");
//        pHealthBar = player.GetComponent<HealthBar>();
//        clearDecisionState();
//        //healthSystem = new HealthSystem(health);
//        healthBar.Setup(healthSystem);
//        cRed = Color.red;
//        cGrey = Color.grey;
//        canAttack = true;
//        hitbox.SetActive(false);
//    }

//    void Update()
//    {
//        // actively set params for walk()
//        followTarget = player;
//        tPos = followTarget.transform.position;

//        // Distance between enemy and player
//        dX = transform.position.x - player.transform.position.x;
//        dY = transform.position.y - player.transform.position.y;

//        //Debug.Log("dX: " + dX + " dY: " + dY);
//        // Setup cooldown
//        if (coolDown > 0)
//        {
//            coolDown -= Time.deltaTime;
//        }

//        else
//            canAttack = true;

//        // Primary walk trigger
//        if (!isAttacking)
//        {
//            Walk();
//        }

//        // Primary attack trigger
//        if (inRange(dX, dY))
//        {
//            if (canAttack)
//            {
//                // calculating charge position
//                // need to keep charge position static as soon as enemy turns red
//                //Vector3 cPos = player.transform.position;
//                StartCoroutine(Attack());
//            }
//        }

//        else
//            clearDecisionState();

//        // Cooldown in between attacks
//        if (coolingDown()) // nextAttack > 0 
//        {
//            nextAttack -= Time.deltaTime;
//        }

//        // despawn gameObject if health < 0
//        if (isDead())
//        {
//            Destroy(gameObject);
//        }

//    }

//    void Walk()
//    {

//        if (!inRange(dX,dY) && inSight(dX,dY))
//        {
//            // move gameObject at a constant speed towards player
//            transform.position = Vector2.MoveTowards(transform.position, tPos, normalSpeed * Time.deltaTime);
//        }  
        
//        else    // trigger attack sequence (Update())
//        {
//            if (canAttack)
//            {
//                isAttacking = true;
//            }


//        }
//    }
    
//    IEnumerator Attack()
//    {
//        Vector3 target = player.transform.position;
//        //Debug.Log(player.transform.position);
//        // "charge" attack if wait > 0 and able to attack
//        if (wait > 0 && canAttack && isAttacking)
//        {
//            // Lock onto target
//            sr.color = cRed;
//            // Determine target charge location
//            setPos = LockOn(target);
//            //Debug.Log("Charge position: " + setPos);
//            yield return new WaitForSeconds(wait -= Time.deltaTime);
//        }
//        else
//        {
//            if (canAttack)
//            {
//                // enable hitbox
//                hitbox.gameObject.SetActive(true);
//                StartCoroutine(Charging(setPos));
//            }

//            clearDecisionState();
//        }      
//    }

//    void OnCollisionEnter2D(Collision2D other)
//    {
//        if (other.gameObject.CompareTag("Player"))
//        {
//            // Damage player
//            other.gameObject.GetComponentInChildren<HealthBar>().healthSystem.Damage(25);
//            // Reload the level
//            //other.gameObject.SetActive(false);
//            //reloading = true;
//            //StartCoroutine(LoadNewLevel());
//        }
//    }

//    private bool attackReady()
//    {
//        if (coolDown > 0)
//        {
//            return true;
//        }
//        else
//        {
//            return false;
//        }
//    }

//    public bool inRange(float X, float Y)
//    {
//        if (dX < attackRange && dY < attackRange)
//        {
//            return true;
//        }
//        else
//            return false;
//    }

//    private bool isDead()
//    {
//        if (healthBar.healthSystem.GetHealthPercent() <= 0)
//        {
//            return true;
//        }
//        else
//            return false;
//    }

//    private bool coolingDown()
//    {
//        if (nextAttack > 0)
//            return true;
//        else
//            return false;
//    }

//    private bool inSight(float X, float Y)
//    {
//        // if player distance is greater than sightRange, don't move
//        if (X < sightRange && Y < sightRange) // 6 is a good number
//            return true;
//        else
//            return false;
//    }

//    private void clearDecisionState()
//    {

//        // Attack()
//        nextAttack = attackRate;

//        isAttacking = false;
//        coolDown = attackRate;

//        // attackCheck()
//        wait = attackRate;
//        // Visual representation of decision state availability
//        sr.color = cGrey;
//    }

//    private Vector3 LockOn(Vector3 cPos)
//    {
//        if (!targetChosen)
//        {
//            // Create a placeholder for target position to charge to
//            targetPlace = Instantiate(targetPrefab, cPos, Quaternion.identity);
//            //Debug.Log("Target: " + targetPlace.transform.position);
//            //Debug.Log("cPos: " + cPos);
//            targetChosen = true;
//            //setPos = targetPrefab.transform.position;
//            //return targetPrefab.transform.position;
//        }
//        return targetPlace.transform.position;
//    }


//    IEnumerator Charging(Vector3 lockOnPos)
//    {
//        // move to player
//        transform.position = Vector3.MoveTowards(transform.position, lockOnPos, chargeSpeed * Time.deltaTime);
//        if(transform.position == lockOnPos) { targetChosen = false; }
//        yield return (transform.position == lockOnPos);
//    }

//    private void DistanceCheck(GameObject strike)
//    {
//        //float playerWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2f;
//        //float boxWidth = strike.GetComponent<SpriteRenderer>().bounds.size.x / 2f;
//        //range = (strike.transform.position.x - gameObject.transform.position.x) + boxWidth + playerWidth;
//        //distance = player.transform.position.x - gameObject.transform.position.x;
//        //if (distance < range)   // assign damage
//        //{
//        //    //Debug.Log("Hit: " + distance);
//        //    //Debug.Log("Range: " + range);
//        //    //pControl.pHealthBar.healthSystem.Damage(20);
//        //    //Debug.Log("Player health after hit: " + pControl.pHealthBar.healthSystem.GetHealth());
//        //}
//    }

//    void RayCheck(int direction)
//    {
//        //float minDepth = -Mathf.Infinity;
//        //float maxDepth = Mathf.Infinity;
//        //int layer = LayerMask.NameToLayer("Players");
       

//        //Debug.Log("Ray firing");
//        //Debug.Log("Range: " + e_range);
//        //Debug.Log(Physics2D.Raycast(new Vector2(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y), new Vector2(-1, 0), e_range, LayerMask.NameToLayer("Players")).collider);
//        // Debug.Log works, incorporate in all directions
//        //switch (direction)
//        //{
//        //    case 1:     // West
//        //        if (Physics2D.Raycast(new Vector2(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y), new Vector2(-1, 0), e_range, LayerMask.NameToLayer("Players")).collider)
//        //        {
//        //            // Assign damage
//        //            pControl.pHealthBar.healthSystem.Damage(20);
//        //            Debug.Log("Player health after hit: " + pControl.pHealthBar.healthSystem.GetHealth());


//        //        }
//        //        break;

//        //    case 2:     // East
//        //        if (Physics2D.Raycast(new Vector2(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y), new Vector2(1, 0), e_range, LayerMask.NameToLayer("Players")).collider)
//        //        {
//        //            // Assign damage
//        //            pControl.pHealthBar.healthSystem.Damage(20);
//        //            Debug.Log("Player health after hit: " + pControl.pHealthBar.healthSystem.GetHealth());


//        //        }
//        //        break;

//        //    case 3:     // North
//        //        if (Physics2D.Raycast(new Vector2(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y), new Vector2(0, 1), e_range, LayerMask.NameToLayer("Players")).collider)
//        //        {
//        //            // Assign damage
//        //            pControl.pHealthBar.healthSystem.Damage(20);
//        //            Debug.Log("Player health after hit: " + pControl.pHealthBar.healthSystem.GetHealth());


//        //        }
//        //        break;

//        //    case 4:     // South
//        //        if (Physics2D.Raycast(new Vector2(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y), new Vector2(0, -1), e_range, LayerMask.NameToLayer("Players")).collider)
//        //        {
//        //            // Assign damage
//        //            pControl.pHealthBar.healthSystem.Damage(20);
//        //            Debug.Log("Player health after hit: " + pControl.pHealthBar.healthSystem.GetHealth());


//        //        }
//        //        break;




//        //}

//    }


    

//}

