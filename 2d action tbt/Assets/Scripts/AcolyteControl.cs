using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcolyteControl : MonoBehaviour
{
    //private bool isMoving;
    //public float moveSpeed;
    //private Vector3 tPos;
    //public GameObject followTarget;
    //[SerializeField]
    //private float wait;    // wait period before attack (turns red)
    //[SerializeField]
    //private bool isAttacking;
    //Color cRed;
    //Color cGrey;
    //public SpriteRenderer sr;
    //[SerializeField]
    //private float coolDown;  // central timer for attack cooldown
    //private bool canAttack;
    //[SerializeField]
    //private float nextAttack;   // time before enemy can attack again
    //[SerializeField]
    //private float attackRate;
    //[SerializeField]
    //private float attackRange;
    //public HealthBar healthBar;
    //public HealthSystem healthSystem;
    //public float dX;
    //public float dY;
    //private float distance;
    //private float range;
    //public GameObject strike;
    //public GameObject player;
    //public PlayerController pControl;
    //[SerializeField]
    //private int eHealth;
    //private Vector2 chargePos;
    //[SerializeField]
    //private float chargeSpeed;
    private Acolyte enemy;
    //private enum c_speed { };
    [SerializeField]
    private Enemy.moveSpeed c_speed;
    private Enemy.moveSpeed w_speed;
    private Enemy.moveSpeed speed;

    void Start()
    {
        enemy = new Acolyte();
        enemy.sr = GetComponent<SpriteRenderer>();
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
        if (isDead())
        {
            Destroy(gameObject);
        }

        enemy.dX = transform.position.x - enemy.tPos.x;
        enemy.dY = transform.position.y - enemy.tPos.y;

        if (attackReady()) // bool coolDown > 0
        {
            enemy.coolDown -= Time.deltaTime;
        }

        else
        {
            enemy.canAttack = true;
        }

        if (!enemy.isAttacking)
        {
            Walk();
        }

        if (inRange(enemy.dX, enemy.dY))
        {
            if (enemy.canAttack)
                Attack();
        }

        else
        {
            enemy.isAttacking = false;
        }

        if (coolingDown()) // nextAttack > 0
        {
            enemy.nextAttack -= Time.deltaTime;
        }

        

    }

    void Walk()
    {
        // enemy.tPos -> Vector 3 in enemy script
        // enemy.followTarget.transform.position -> enemy(script).player(gameobject).position -> position of player
        //enemy.tPos = enemy.followTarget.transform.position; // this line is fucked
        if (!inRange(enemy.dX, enemy.dY))
        {
            transform.position = Vector2.Lerp(transform.position, enemy.tPos, getSpeed() * Time.deltaTime);
        }

        else
        {
            if (enemy.canAttack)
                enemy.isAttacking = true;
        }

    }

    void Attack()
    {
        if (enemy.wait > 0 && enemy.canAttack && enemy.isAttacking)
        {
            // Charging up attack
            enemy.wait -= Time.deltaTime;
            enemy.sr.color = enemy.cRed;
            enemy.nextAttack = enemy.attackRate;
        }

        else
        {
            if (enemy.canAttack)
            {
                //attackCheck();    // for striking enemy types
                charge(enemy.tPos, c_speed);     // for charging enemy types
            }

            enemy.sr.color = enemy.cGrey;

            enemy.coolDown = enemy.attackRate;
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
        if (enemy.coolDown > 0)
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
        enemy.canAttack = false;
        enemy.coolDown = enemy.attackRate;
        enemy.strike.SetActive(true);
        float playerWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2f;
        float boxWidth = enemy.strike.GetComponent<SpriteRenderer>().bounds.size.x / 2f;

        enemy.range = (enemy.strike.transform.position.x - gameObject.transform.position.x) + boxWidth + playerWidth;
        enemy.distance = enemy.player.transform.position.x - gameObject.transform.position.x;

        if (enemy.distance < enemy.range)
        {
            //Debug.Log("Hit: " + distance);
            //Debug.Log("Range: " + range);
            enemy.pControl.healthSystem.Damage(50);
            Debug.Log("After hit: " + enemy.pControl.healthSystem.GetHealthPercent());
        }

        Hide();
        enemy.isAttacking = false;
        enemy.wait = enemy.attackRate;

    }

    private void Hide()
    {
        enemy.strike.SetActive(false);
    }

    private bool coolingDown()
    {
        if (enemy.nextAttack > 0)
            return true;
        else
            return false;
    }

    private void charge(Vector3 chargePos, Enemy.moveSpeed speed) // charge through player
    {
        enemy.canAttack = false;
        enemy.coolDown = enemy.attackRate;
        // Charging the player
        transform.position = Vector2.Lerp(transform.position, chargePos, getSpeed());


        enemy.isAttacking = false;
        enemy.wait = enemy.attackRate;     
    }
}