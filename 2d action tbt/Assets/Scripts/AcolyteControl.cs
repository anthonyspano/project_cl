using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Behavior script for acolyte enemies
/* Known issues
 * Lerp accelerates enemy toward player. Find alternative.
 * Always following
 * Never in range?
 * Check all inherited values?
 * */
public class AcolyteControl : MonoBehaviour
{
    private bool isMoving;
    private bool canAttack;
    //private Vector3 tPos;
    public GameObject followTarget;
    //[SerializeField]
    private bool isAttacking;
    [SerializeField]
    private SpriteRenderer sr;
    // Object-specific timers
    [SerializeField]
    private float wait;    // wait period before attack (turns red)
    [SerializeField]
    private float coolDown;  // central timer for attack cooldown  
    [SerializeField]
    private float nextAttack;   // time before enemy can attack again
    //[SerializeField]
    //private float attackRate;
    //[SerializeField]
    //private float attackRange;
    //public HealthBar healthBar;
    //public HealthSystem healthSystem;
    private float dX;
    private float dY;
    private float distance;
    private float range;
    public GameObject strike;
    public GameObject player;
    //public PlayerController pControl;
    //[SerializeField]
    //private int eHealth;
    //private Vector2 chargePos;
    private Acolyte acolyte;
    //private enum c_speed { };
    [SerializeField]
    private Enemy.moveSpeed c_speed;
    private Enemy.moveSpeed w_speed;
    private Enemy.moveSpeed speed;
    private Enemy test;


    void Start()
    {
        EnemySetup();
        sr = GetComponent<SpriteRenderer>();
        acolyte.cRed = new Color(250f, 0f, 0f);
        acolyte.cGrey = new Color(0f, 0f, 150f);
        acolyte.healthSystem = new HealthSystem(acolyte.eHealth);
        //acolyte.healthBar.Setup(acolyte.healthSystem);
        Hide();
        c_speed = Enemy.moveSpeed.charge;
        w_speed = Enemy.moveSpeed.walk;
    }

    void EnemySetup()
    {
        acolyte = new Acolyte();
        acolyte.coolDown = 3f;
        acolyte.attackRate = 2f;

        acolyte.attackRange = 1.5f;
        //float gameObjectWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2f;
        //float boxWidth = strike.GetComponent<SpriteRenderer>().bounds.size.x / 2f;
        //acolyte.attackRange = (strike.transform.position.x - gameObject.transform.position.x) + boxWidth + gameObjectWidth;

        
    }

    float getSpeed()
    {
        switch(speed)
        {
            case Enemy.moveSpeed.walk: return 2;
            case Enemy.moveSpeed.charge: return 5;
        }

        return 0;
    }

    void Update()
    {
        followTarget = player;
        acolyte.tPos = followTarget.transform.position;

        if (isDead())
        {
            Destroy(gameObject);
        }

        dX = transform.position.x - player.transform.position.x;
        dY = transform.position.y - player.transform.position.y;
        //Debug.Log(acolyte.attackRange);
        //Debug.Log(inRange(dX,dY));

        if (attackReady()) // bool coolDown > 0
        {
            coolDown -= Time.deltaTime;
        }

        else
        {
            canAttack = true;
        }

        if (!isAttacking)
        {
            Walk();
        }

        if (inRange(dX, dY))
        {
            //Debug.Log("I'm in range");
            if (canAttack)
                Attack();
        }

        else
        {
            isAttacking = false;
        }

        if (coolingDown()) // nextAttack > 0
        {
            nextAttack -= Time.deltaTime;
        }

        

    }

    void Walk()
    {
        // enemy.tPos -> Vector 3 in enemy script
        // acolyte.followTarget.transform.position -> enemy(script).player(gameobject).position -> position of player
        //acolyte.tPos = enemy.followTarget.transform.position; // this line is fucked
        if (!inRange(dX, dY))
        {
            // lerp gameObject position to tPos
            transform.position = Vector2.Lerp(transform.position, acolyte.tPos, getSpeed() * Time.deltaTime);
        }

        else
        {
            if (canAttack)
            {
                isAttacking = true;
                Debug.Log("isAttacking: " + isAttacking);
            }

        }
    }

    void Attack()
    {
        if (wait > 0 && canAttack && isAttacking)
        {
            // Charging up attack
            wait -= Time.deltaTime;
            sr.color = acolyte.cRed;
            nextAttack = acolyte.attackRate;
        }

        else
        {
            if (canAttack)
            {
                attackCheck();    // bar should show up
                //charge(acolyte.tPos, c_speed);     // for charging enemy types
            }

            sr.color = acolyte.cGrey;

            coolDown = acolyte.attackRate;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.name == "Player1")
        {
            // Damage dealt by charging
            acolyte.pControl.healthSystem.Damage(25);

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
        if (X * X + Y * Y < acolyte.attackRange)
        {
            return true;
        }

        else
            return false;
    }

    private bool isDead()
    {
        if (acolyte.healthSystem.GetHealthPercent() <= 0)
        {
            return true;
        }
        else
            return false;
    }

    private void attackCheck()
    {
        canAttack = false;
        coolDown = acolyte.attackRate;
        strike.SetActive(true);
        float playerWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2f;
        float boxWidth = strike.GetComponent<SpriteRenderer>().bounds.size.x / 2f;

        range = (strike.transform.position.x - gameObject.transform.position.x) + boxWidth + playerWidth;
        distance = player.transform.position.x - gameObject.transform.position.x;

        if (distance < range)
        {
            //Debug.Log("Hit: " + distance);
            //Debug.Log("Range: " + range);
            acolyte.pControl.healthSystem.Damage(50);
            Debug.Log("After hit: " + acolyte.pControl.healthSystem.GetHealthPercent());
        }

        Hide();
        isAttacking = false;
        wait = acolyte.attackRate;

    }

    private void Hide()
    {
        strike.SetActive(false);
    }

    private bool coolingDown()
    {
        if (nextAttack > 0)
            return true;
        else
            return false;
    }

    private void charge(Vector3 chargePos, Enemy.moveSpeed speed) // charge through player
    {
        canAttack = false;
        coolDown = acolyte.attackRate;
        // Charging the player
        transform.position = Vector2.Lerp(transform.position, chargePos, getSpeed());


        isAttacking = false;
        wait = acolyte.attackRate;     
    }
}