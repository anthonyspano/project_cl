using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Behavior script for hooded enemies
/* Known issues:
 * Animate normal attack (like a stab)
 * Raycast multiple directions
 */

// Handles actions of hooded enemy (prototype)
public class AcolyteControl : MonoBehaviour
{
    public enum moveSpeed { walk, charge };
    public bool isMoving;
    public Vector3 tPos;
    [SerializeField]
    public float wait;    // wait period before attack (turns red)
    [SerializeField]
    public bool isAttacking;
    public Color cRed;
    public Color cGrey;
    public SpriteRenderer sr;
    // Object-specific timers
    [SerializeField]
    private float coolDown;  // central timer for attack cooldown
    public bool canAttack;
    [SerializeField]
    public float nextAttack;   // time before enemy can attack again
    [SerializeField]
    public float attackRate;
    [SerializeField]
    public float attackRange;
    public HealthBar healthBar;
    public HealthSystem healthSystem;
    public float dX;
    public float dY;
    public float distance;
    public float range;
    public GameObject strikeN;
    public GameObject strikeS;
    public GameObject strikeE;
    public GameObject strikeW;
    public GameObject leftBounds;
    public GameObject rightBounds;
    public GameObject player;
    public PlayerController pControl;
    [SerializeField]
    public int eHealth;
    public Vector2 chargePos;
    private BoxCollider2D bc;
    private Animator anim;
    public GameObject followTarget;
    [SerializeField]
    private moveSpeed c_speed;
    private moveSpeed w_speed;
    private moveSpeed speed;
    [SerializeField]
    private float sightRange;
    private int counter;
    public HealthBar eHealthBar;
    private GameObject testObject;
    // target to lock on to
    public GameObject targetPrefab;
    // Charge
    private Vector3 setPos;
    private bool targetChosen;
    private GameObject targetPlace;
    public bool chargeDamage;
    public float e_range;


    void Start()
    {
        EnemySetup();
        sr = GetComponent<SpriteRenderer>();
        Hide();                                     // Deactivate hitboxes, bc
        c_speed = moveSpeed.charge;
        w_speed = moveSpeed.walk;
    }

    void EnemySetup()
    {
        //coolDown = 0.5f;
        attackRate = coolDown;
        attackRange = 2f;
        cRed = new Color(250f, 0f, 0f);
        cGrey = new Color(0f, 0f, 150f);
        // Health System
        healthSystem = new HealthSystem(eHealth);
        eHealthBar.Setup(healthSystem);
        Debug.Log("Enemy health: " + eHealthBar.healthSystem.GetHealth());
        //float gameObjectWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2f;
        //float boxWidth = strike.GetComponent<SpriteRenderer>().bounds.size.x / 2f;
        //attackRange = (strike.transform.position.x - gameObject.transform.position.x) + boxWidth + gameObjectWidth;
    }

    float getSpeed()
    {
        switch(speed)
        {
            case moveSpeed.walk: return 2;
            case moveSpeed.charge: return 0.5f;
        }

        return 0;
    }

    

    void Update()
    {
        // Debug.Log
        realtimeDebug();
        // actively set params for walk()
        followTarget = player;
        tPos = followTarget.transform.position;
        // despawn gameObject if health < 0
        if (isDead())
        {
            Destroy(gameObject);
        }
        // Distance between enemy and player
        dX = transform.position.x - player.transform.position.x;
        dY = transform.position.y - player.transform.position.y;
        // bool coolDown > 0 
        if (attackReady()) 
        {
            coolDown -= Time.deltaTime;
        }
        else
        {
            canAttack = true;
        }
        // Primary walk trigger
        if (!isAttacking)
        {
            Walk();
        }
        // Primary attack trigger
        if (inRange(dX, dY))
        {
            if (canAttack)
            {
                // calculating charge position
                // need to keep charge position static as soon as enemy turns red
                //Vector3 cPos = player.transform.position;
                //Debug.Log("Charge position: " + cPos);
                StartCoroutine(Attack());
            }
        }
        // If not attacking, clear decision state for reanalysis
        else
        {
            clearDecisionState();
        }
        // Cooldown in between attacks
        if (coolingDown()) // nextAttack > 0 
        {
            nextAttack -= Time.deltaTime;
        }
    }

    void Walk()
    {
        if (!inRange(dX,dY) && inSight(dX,dY))
        {
            // move gameObject at a constant speed towards player
            transform.position = Vector2.MoveTowards(transform.position, tPos, getSpeed() * Time.deltaTime);
        }      
        else    // trigger attack sequence (Update())
        {
            if (canAttack)
            {
                isAttacking = true;
            }
        }
    }
    
    IEnumerator Attack()
    {
        //Transform target = player.transform;
        Vector3 target = player.transform.position;
        //Debug.Log(player.transform.position);
        // "charge" attack if wait > 0 and able to attack
        if (wait > 0 && canAttack && isAttacking)
        {
            // Lock onto target
            sr.color = cRed;
            // Determine target charge location
            setPos = LockOn(target);
            //Debug.Log("Charge position: " + setPos);
            yield return new WaitForSeconds(wait -= Time.deltaTime);
        }
        else
        {
            if (canAttack)
            {
                // Determine where the player is
                // Put boundaries on the sides that mark how to decide direction
                if (player.transform.position.x < leftBounds.transform.position.x) // left of left boundary - attack left
                {
                    attackClear();
                    RayCheck(1); // West
                    //attackCheck(strikeW);
                }

                // right of right boundary - attack right
                if (player.transform.position.x > rightBounds.transform.position.x)
                {
                    attackClear();
                    RayCheck(2);    // East
                    //attackCheck(strikeE);
                }

                if ((player.transform.position.x > leftBounds.transform.position.x) &&
                    (player.transform.position.x < rightBounds.transform.position.x) &&
                    (player.transform.position.y > gameObject.transform.position.y))     // up
                {
                    attackClear();
                    RayCheck(3);    // North
                    //attackCheck(strikeN);
                }

                if ((player.transform.position.x > leftBounds.transform.position.x) &&
                    (player.transform.position.x < rightBounds.transform.position.x) &&
                    (player.transform.position.y < gameObject.transform.position.y))     // down
                {
                    attackClear();
                    RayCheck(4);    // South
                    //attackCheck(strikeS);
                }
     
            }
            clearDecisionState();
        }      
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player1")
        {
            // Damage player
            pControl.healthSystem.Damage(25);
            // Reload the level
            //other.gameObject.SetActive(false);
            //reloading = true;
            //StartCoroutine(LoadNewLevel());
        }
    }

    private bool attackReady()
    {
        if (coolDown > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool inRange(float X, float Y)
    {
        if (X * X + Y * Y < attackRange)
        {
            return true;
        }
        else
            return false;
    }

    private bool isDead()
    {
        if (eHealthBar.healthSystem.GetHealthPercent() <= 0)
        {
            return true;
        }
        else
            return false;
    }

    private void attackClear() 
    {
        // establish attack completion
        canAttack = false;
        coolDown = attackRate;
        //strike.SetActive(true);   // enable hitbox method
        //DistanceCheck(strike);
        Hide(); // deactivate hitbox
        clearDecisionState();
    }

    private void Hide()
    {
        strikeN.SetActive(false);
        strikeS.SetActive(false);
        strikeE.SetActive(false);
        strikeW.SetActive(false);
    }

    private bool coolingDown()
    {
        if (nextAttack > 0)
            return true;
        else
            return false;
    }

    private bool inSight(float X, float Y)
    {
        // if player distance is greater than sightRange, don't move
        if (X < sightRange && Y < sightRange) // 6 is a good number
            return true;
        else
            return false;
    }

    private void realtimeDebug()
    {
        // State of attacking        
        //Debug.Log("isAttacking: " + isAttacking);
        //Debug.Log("canAttack: " + canAttack);

        // Attack Range of enemy
        //Debug.Log(attackRange);
        //Debug.Log(inRange(dX,dY));
        // Distance of sight before aggro
        //Debug.Log("Is in sight: " + inSight(dX,dY));
        //Debug.Log("(dx,dy): (" + dX + "," + dY + ")");
    }

    private void clearDecisionState()
    {
        // Attack()
        nextAttack = attackRate;
        isAttacking = false;
        coolDown = attackRate;
        // attackCheck()
        wait = attackRate;
        // Visual representation of decision state availability
        sr.color = cGrey;
    }

    private Vector3 LockOn(Vector3 cPos)
    {
        if (!targetChosen)
        {
            // Create a placeholder for target position to charge to
            targetPlace = Instantiate(targetPrefab, cPos, Quaternion.identity);
            Debug.Log("Target: " + targetPlace.transform.position);
            Debug.Log("cPos: " + cPos);
            targetChosen = true;
            //setPos = targetPrefab.transform.position;
            //return targetPrefab.transform.position;
        }
        return targetPlace.transform.position;
    }

    private void DistanceCheck(GameObject strike)
    {
        float playerWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2f;
        float boxWidth = strike.GetComponent<SpriteRenderer>().bounds.size.x / 2f;
        range = (strike.transform.position.x - gameObject.transform.position.x) + boxWidth + playerWidth;
        distance = player.transform.position.x - gameObject.transform.position.x;
        if (distance < range)   // assign damage
        {
            //Debug.Log("Hit: " + distance);
            //Debug.Log("Range: " + range);
            //pControl.pHealthBar.healthSystem.Damage(20);
            //Debug.Log("Player health after hit: " + pControl.pHealthBar.healthSystem.GetHealth());
        }
    }

    void RayCheck(int direction)
    {
        float minDepth = -Mathf.Infinity;
        float maxDepth = Mathf.Infinity;
        int layer = LayerMask.NameToLayer("Players");
       

        Debug.Log("Ray firing");
        Debug.Log("Range: " + e_range);
        Debug.Log(Physics2D.Raycast(new Vector2(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y), new Vector2(-1, 0), e_range, LayerMask.NameToLayer("Players")).collider);
        // Debug.Log works, incorporate in all directions
        switch (direction)
        {
            case 1:     // West
                if (Physics2D.Raycast(new Vector2(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y), new Vector2(-1, 0), e_range, LayerMask.NameToLayer("Players")).collider)
                {
                    // Assign damage
                    pControl.pHealthBar.healthSystem.Damage(20);
                    Debug.Log("Player health after hit: " + pControl.pHealthBar.healthSystem.GetHealth());


                }
                break;

            case 2:     // East
                if (Physics2D.Raycast(new Vector2(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y), new Vector2(1, 0), e_range, LayerMask.NameToLayer("Players")).collider)
                {
                    // Assign damage
                    pControl.pHealthBar.healthSystem.Damage(20);
                    Debug.Log("Player health after hit: " + pControl.pHealthBar.healthSystem.GetHealth());


                }
                break;

            case 3:     // North
                if (Physics2D.Raycast(new Vector2(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y), new Vector2(0, 1), e_range, LayerMask.NameToLayer("Players")).collider)
                {
                    // Assign damage
                    pControl.pHealthBar.healthSystem.Damage(20);
                    Debug.Log("Player health after hit: " + pControl.pHealthBar.healthSystem.GetHealth());


                }
                break;

            case 4:     // South
                if (Physics2D.Raycast(new Vector2(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y), new Vector2(0, -1), e_range, LayerMask.NameToLayer("Players")).collider)
                {
                    // Assign damage
                    pControl.pHealthBar.healthSystem.Damage(20);
                    Debug.Log("Player health after hit: " + pControl.pHealthBar.healthSystem.GetHealth());


                }
                break;




        }

    }

    

}

