using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Behavior script for acolyte enemies
public class AcolyteControl : MonoBehaviour
{
    private bool isMoving;
    //private Vector3 tPos;
    //public GameObject followTarget;
    //[SerializeField]
    private float wait;    // wait period before attack (turns red)
    //[SerializeField]
    private bool isAttacking;
    [SerializeField]
    private SpriteRenderer sr;
    //[SerializeField]
    private float coolDown;  // central timer for attack cooldown
    private bool canAttack;
    //[SerializeField]
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
    private Acolyte enemy;
    //private enum c_speed { };
    [SerializeField]
    private Enemy.moveSpeed c_speed;
    private Enemy.moveSpeed w_speed;
    private Enemy.moveSpeed speed;
    private Enemy test;

    void Start()
    {
        enemy = new Acolyte();
        sr = GetComponent<SpriteRenderer>();
        enemy.cRed = new Color(250f, 0f, 0f);
        enemy.cGrey = new Color(0f, 0f, 150f);
        enemy.healthSystem = new HealthSystem(enemy.eHealth);
        //enemy.healthBar.Setup(enemy.healthSystem);
        Hide();
        c_speed = Enemy.moveSpeed.charge;
        w_speed = Enemy.moveSpeed.walk;
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
        enemy.followTarget = player;
        enemy.tPos = enemy.followTarget.transform.position;

        if (isDead())
        {
            Destroy(gameObject);
        }

        dX = transform.position.x - player.transform.position.x;
        dY = transform.position.y - player.transform.position.y;

        if (attackReady()) // bool coolDown > 0
        {
            enemy.coolDown -= Time.deltaTime;
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
        // enemy.followTarget.transform.position -> enemy(script).player(gameobject).position -> position of player
        //enemy.tPos = enemy.followTarget.transform.position; // this line is fucked
        if (!inRange(dX, dY))
        {
            transform.position = Vector2.Lerp(transform.position, enemy.tPos, getSpeed() * Time.deltaTime);
        }

        else
        {
            if (canAttack)
                isAttacking = true;
        }

    }

    void Attack()
    {
        if (wait > 0 && canAttack && isAttacking)
        {
            // Charging up attack
            wait -= Time.deltaTime;
            sr.color = enemy.cRed;
            nextAttack = enemy.attackRate;
        }

        else
        {
            if (canAttack)
            {
                //attackCheck();    // for striking enemy types
                charge(enemy.tPos, c_speed);     // for charging enemy types
            }

            sr.color = enemy.cGrey;

            coolDown = enemy.attackRate;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.name == "Player1")
        {
            // Damage dealt by charging
            enemy.pControl.healthSystem.Damage(25);

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
        if (X * X + Y * Y < enemy.attackRange)
        {
            return true;
        }

        else
            return false;
    }

    private bool isDead()
    {
        if (enemy.healthSystem.GetHealthPercent() <= 0)
        {
            return true;
        }
        else
            return false;
    }

    private void attackCheck()
    {
        canAttack = false;
        coolDown = enemy.attackRate;
        strike.SetActive(true);
        float playerWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2f;
        float boxWidth = strike.GetComponent<SpriteRenderer>().bounds.size.x / 2f;

        range = (strike.transform.position.x - gameObject.transform.position.x) + boxWidth + playerWidth;
        distance = player.transform.position.x - gameObject.transform.position.x;

        if (distance < range)
        {
            //Debug.Log("Hit: " + distance);
            //Debug.Log("Range: " + range);
            enemy.pControl.healthSystem.Damage(50);
            Debug.Log("After hit: " + enemy.pControl.healthSystem.GetHealthPercent());
        }

        Hide();
        isAttacking = false;
        wait = enemy.attackRate;

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
        coolDown = enemy.attackRate;
        // Charging the player
        transform.position = Vector2.Lerp(transform.position, chargePos, getSpeed());


        isAttacking = false;
        wait = enemy.attackRate;     
    }
}