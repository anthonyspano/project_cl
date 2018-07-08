using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EControl : MonoBehaviour
{
    private bool isMoving;
    public float moveSpeed;
    private Vector3 tPos;
    public GameObject followTarget;
    [SerializeField]
    private float wait;    // wait period before attack (turns red)
    [SerializeField]
    private bool isAttacking;
    Color cRed;
    Color cGrey;
    public SpriteRenderer sr;
    private bool reloading;
    [SerializeField]
    private float waitToReload;
    [SerializeField]
    private float coolDown;  // central timer for attack cooldown
    private bool canAttack;
    [SerializeField]
    private float nextAttack;   // time before enemy can attack again
    [SerializeField]
    private float attackRate;
    [SerializeField]
    private float attackRange;
    public HealthBar healthBar;
    public HealthSystem healthSystem;
    public float dX;
    public float dY;
    private float distance;
    private float range;
    public GameObject strike;
    public GameObject player;
    public PlayerController pControl;
    [SerializeField]
    private int eHealth;


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        cRed = new Color(250f, 0f, 0f);
        cGrey = new Color(0f, 0f, 150f);
        healthSystem = new HealthSystem(eHealth);
        healthBar.Setup(healthSystem);
        Hide();
    }


    void Update()
    {
        if (isDead())
        {
            Destroy(gameObject);
        }

        dX = transform.position.x - tPos.x;
        dY = transform.position.y - tPos.y;

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
            if (canAttack)
                Attack();
        }

        else
        {
            isAttacking = false;
        }

        if (nextAttack > 0)
        {
            nextAttack -= Time.deltaTime;
        }

        if (reloading)  // for respawning player
        {
            waitToReload -= Time.deltaTime;
            if (waitToReload < 0)
            {
                SceneManager.LoadScene("added_controllers", LoadSceneMode.Single);
            }
        }

    }

    void Walk()
    {
        tPos = followTarget.transform.position;
        if (!inRange(dX, dY))
        {
            transform.position = Vector2.Lerp(transform.position, tPos, moveSpeed * Time.deltaTime);
        }

        else
        {
            if (canAttack)
                isAttacking = true;
        }

    }

    void Attack()
    {
        if (coolDown > 0 && canAttack && isAttacking)
        {
            // Charging up attack
            coolDown -= Time.deltaTime;
            sr.color = cRed;
            nextAttack = attackRate;
        }

        else
        {             
            if (canAttack)  
                attackCheck();

            sr.color = cGrey;

            coolDown = attackRate;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.name == "Player1")
        {
            // Damage dealt by charging
            pControl.healthSystem.Damage(25);

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
        if (healthSystem.GetHealthPercent() <= 0)
        {
            return true;
        }
        else
            return false;
    }

    private void attackCheck()
    {
        Debug.Log("Swing!");
        canAttack = false;
        coolDown = attackRate;
        strike.SetActive(true);
        float playerWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2f;
        float boxWidth = strike.GetComponent<SpriteRenderer>().bounds.size.x / 2f;

        range = (strike.transform.position.x - gameObject.transform.position.x) + boxWidth + playerWidth;
        distance = player.transform.position.x - gameObject.transform.position.x;

        if (distance < range)
        {
            //Debug.Log("Hit: " + distance);
            //Debug.Log("Range: " + range);
            pControl.healthSystem.Damage(50);
            Debug.Log("After hit: " + pControl.healthSystem.GetHealthPercent());
        }

        Hide();
        isAttacking = false;

    }

    private void Hide()
    {
        strike.SetActive(false);
    }

}