using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Handles movement of player
public class PlayerController : MonoBehaviour {

    // enumerate timer values???
    public float moveSpeed;
    private Vector2 vSpeed;
    private Animator anim;
    private bool playerMoving;
    private Vector2 lastMove;
    public GameObject strikeN;
    public GameObject strikeS;
    public GameObject strikeE;
    public GameObject strikeW;
    [SerializeField]
    private float spawnTimer;
    public GameObject enemy;
    public float range;
    public float distance;
    public SpriteRenderer e_sr;
    public Color eRed;
    private bool attackingDown;
    public Rigidbody2D rb;
    public HealthBar healthBar;
    public HealthSystem healthSystem;
    //public EnemyController eControl;
    public AcolyteControl eControl;
    private float attackTimer;
    [SerializeField]
    private float attackRate;
    [SerializeField]
    private int playerHP;
    private bool reloading;
    [SerializeField]
    private float waitToReload;
    public enum moving { walk, run };
    public moving walking;
    [SerializeField]
    private Enemy e_class;
    public HealthBar enemyHealth;
    private GameObject[] enemies;
    [SerializeField]
    private int boostSpeed;


    void Start () {
        //enemies = GameObject.FindGameObjectsWithTag("enemy");
        enemy = GameObject.Find("badguy");
        attackTimer = attackRate;
        anim = GetComponent<Animator>();
        lastMove.y = -1.0f; // start player facing down
        Hide();
        //enemy = GameObject.FindGameObjectWithTag("enemy");
        e_sr = enemy.GetComponent<SpriteRenderer>();
        eRed = new Color(250f, 0f, 0f);
        rb = GetComponent<Rigidbody2D>();
        healthSystem = new HealthSystem(playerHP);
        healthBar.Setup(healthSystem);
        enemyHealth = enemy.GetComponent<HealthBar>();
    }

	void Update () {
        Walk();
        canAttack();
        if (canAttack())
            Attack();


        if (reloading)  // for respawning player
        {
            waitToReload -= Time.deltaTime;
            if (waitToReload < 0)
            {
                SceneManager.LoadScene("world_map", LoadSceneMode.Single);
            }
        }

        //playerDeath();
        
	}

    public void Walk()
    {
        playerMoving = false;

        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            //transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);
            playerMoving = true;
            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"),0f);
        }

        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        {
            //transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
            rb.velocity = new Vector2(rb.velocity.x, Input.GetAxisRaw("Vertical") * moveSpeed);
            playerMoving = true;
            lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
        }

        if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
        {
            //transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
        {
            //transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }

        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
    }

    IEnumerator AttackDir(GameObject strike)
    {
        
        strike.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        strike.SetActive(false);
        // strike downward
        attackingDown = false;


    }

    private void Hide()
    {
        strikeN.SetActive(false);
        strikeS.SetActive(false);
        strikeE.SetActive(false);
        strikeW.SetActive(false);

    }

    private void Attack()
    {
        anim.SetBool("attackingDown", attackingDown);
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (lastMove.x == -1f)
            {
                StartCoroutine(AttackDir(strikeW));
                AttackCheck(strikeW);
                AttackMotion(-1, 0);
            }
            else if (lastMove.x == 1f)
            {
                StartCoroutine(AttackDir(strikeE));
                AttackCheck(strikeE);
                AttackMotion(1, 0);
            }
            else if (lastMove.y == 1f)
            {
                StartCoroutine(AttackDir(strikeN));
                AttackCheck(strikeN);
                AttackMotion(0, 1);
            }
            else
            {
                attackingDown = true;
                StartCoroutine(AttackDir(strikeS));
                AttackCheck(strikeS);
                AttackMotion(0, -1);
            }
        }
    }

    private void AttackCheck(GameObject strike)
    {
        //float boxWidth = strike.transform.localScale.x / 2f;
        //float playerWidth = gameObject.transform.localScale.x / 2f;

        float playerWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2f;
        float boxWidth = strike.GetComponent<SpriteRenderer>().bounds.size.x / 2f;

        range = (strike.transform.position.x - gameObject.transform.position.x) + boxWidth + playerWidth;
        distance = enemy.transform.position.x - gameObject.transform.position.x;
        
        if (distance < range)
        {
            //Debug.Log("Hit: " + distance);
            //Debug.Log("Range: " + range);
            // enemyHealth = enemy.GetComponent<HealthBar>();
            enemyHealth.healthSystem.Damage(20);
          
            Debug.Log("After hit: " + e_class.healthSystem.GetHealthPercent());
            //Debug.Log(eControl.healthSystem.)



            // return all instantiated materials of enemy
            if (e_sr.color == eRed)
            {
                //Debug.Log("Seven-sided strike!");
            }
        }
        attackTimer = attackRate;
    }

    private bool canAttack()
    {
        if (attackTimer < 0)
            return true;

        else
        {
            attackTimer -= Time.deltaTime;
            return false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            healthSystem.Damage(25);
            Debug.Log("Health: " + healthSystem.GetHealth());
        }
    }

    private void AttackMotion(int x, int y)
    {
        // sudden forward motion when attacking; 20 seems like a good number
        int boostX = x * boostSpeed;
        int boostY = y * boostSpeed;
        rb.velocity = new Vector2(boostX, boostY);
        


    }

    //private void playerDeath()
    //{
    //    if (healthSystem.GetHealth() == 0)
    //    {
    //        reloading = true;
    //    }
    //}

}
