//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using System;

//// Handles actions of player
///* Known issues:
// * Implement controller
// * 
// */
//public class PlayerController : MonoBehaviour
//{

//    public float vstick;
//    public float hstick;
//    public float moveSpeed;
//    private Vector2 vSpeed;
//    private Animator anim;
//    private bool playerMoving;
//    private Vector2 lastMove;
//    public GameObject strikeN;
//    public GameObject strikeS;
//    public GameObject strikeE;
//    public GameObject strikeW;
//    [SerializeField]
//    private float spawnTimer;
//    public GameObject enemy;
//    public float range;
//    public float distance;
//    public SpriteRenderer e_sr;
//    public Color eRed;
//    private bool attackingDown;
//    public Rigidbody2D rb;
//    public HealthBar pHealthBar;
//    public HealthSystem healthSystem;
//    //public EnemyController eControl;
//    public AcolyteControl eControl;
//    private float attackTimer;
//    [SerializeField]
//    private float attackRate;
//    [SerializeField]
//    private int playerHP;
//    private bool reloading;
//    [SerializeField]
//    private float waitToReload;
//    public enum moving { walk, run };
//    public moving walking;
//    //private GameObject[] enemies;
//    [SerializeField]
//    private int boostSpeed;
//    public HealthBar eHealthBar;

//    public event EventHandler confirmed;

//    public GameObject[] enemies;

//    int size;

//    private Vector3 moveDir;


//    void Start()
//    {
//        //enemies = GameObject.FindGameObjectsWithTag("enemy");
//        //enemy = GameObject.Find("badguy");
//        attackTimer = attackRate;
//        anim = GetComponent<Animator>();
//        lastMove.y = -1.0f; // start player facing down
//        Hide();
//        e_sr = GetComponent<SpriteRenderer>();
//        eRed = new Color(250f, 0f, 0f);
//        rb = GetComponent<Rigidbody2D>();
//        healthSystem = new HealthSystem(playerHP);
//        pHealthBar.Setup(healthSystem);
//        Debug.Log("Player base health: " + pHealthBar.healthSystem.GetHealth());
//        size = enemies.Length;
//        rb.isKinematic = false;

//    }

//    void Update()
//    {
        
//        SixWalk();

//        vstick = Input.GetAxisRaw("Vertical");
//        hstick = Input.GetAxisRaw("Horizontal");

//        //Walk();
//        canAttack();
//        if (canAttack())
//            Attack();

//        if (reloading)  // for respawning player
//        {
//            waitToReload -= Time.deltaTime;
//            if (waitToReload < 0)
//            {
//                SceneManager.LoadScene("world_map", LoadSceneMode.Single);
//            }
//        }


//        //playerDeath();  

//        if (Input.GetKeyDown(KeyCode.Q)) close();

//    }


//    public void Walk()
//    {
//        // deprecated
//        //playerMoving = false;   // Not until key press
//    }

//    IEnumerator AttackDir(GameObject strike)
//    {
//        strike.SetActive(true);
//        yield return new WaitForSeconds(0.3f);
//        strike.SetActive(false);
//        // TODO: implement animations for other directions
//        // strike downward animation
//        attackingDown = false;
//    }

//    private void Hide()
//    {
//        strikeN.SetActive(false);
//        strikeS.SetActive(false);
//        strikeE.SetActive(false);
//        strikeW.SetActive(false);
//    }

//    private void Attack()
//    {
//        anim.SetBool("attackingDown", attackingDown);
//        if (Input.GetKeyDown(KeyCode.K) || Input.GetButton("X"))
//        {
//            if (lastMove.x == -1f)
//            {
//                // Spawn hitbox
//                StartCoroutine(AttackDir(strikeW));
//                AttackCheck(strikeW);
//                // Adds dash to attack
//                AttackMotion(-1, 0);
//            }
//            else if (lastMove.x == 1f)
//            {
//                StartCoroutine(AttackDir(strikeE));
//                AttackCheck(strikeE);
//                AttackMotion(1, 0);
//            }
//            else if (lastMove.y == 1f)
//            {
//                StartCoroutine(AttackDir(strikeN));
//                AttackCheck(strikeN);
//                AttackMotion(0, 1);
//            }
//            else
//            {
//                attackingDown = true;
//                StartCoroutine(AttackDir(strikeS));
//                AttackCheck(strikeS);
//                AttackMotion(0, -1);
//            }
//        }
//    }

//    private void AttackCheck(GameObject strike)
//    {
//        //float boxWidth = strike.transform.localScale.x / 2f;
//        //float playerWidth = gameObject.transform.localScale.x / 2f;

//        float playerWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2f;
//        float boxWidth = strike.GetComponent<SpriteRenderer>().bounds.size.x / 2f;

//        range = (strike.transform.position.x - gameObject.transform.position.x) + boxWidth + playerWidth;
//        distance = enemy.transform.position.x - gameObject.transform.position.x;

//        if (distance < range)
//        {
//            Debug.Log("Hit: " + distance);
//            Debug.Log("Range: " + range);

//            eHealthBar.healthSystem.Damage(20);

//            Debug.Log("After hit: " + eHealthBar.healthSystem.GetHealth());
//            //Debug.Log(eControl.healthSystem.)

//            // return all instantiated materials of enemy
//            if (e_sr.color == eRed)
//            {
//                //Debug.Log("Seven-sided strike!");
//            }
//        }
//        attackTimer = attackRate;
//    }

//    private bool canAttack()
//    {
//        if (attackTimer < 0)
//            return true;

//        else
//        {
//            attackTimer -= Time.deltaTime;
//            return false;
//        }
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.gameObject.tag == "enemy")
//        {

//            pHealthBar.healthSystem.Damage(25);
//            healthSystem.Damage(25);
//            //Debug.Log("Player was hit! Health: " + pHealthBar.healthSystem.GetHealth());


//        }
//    }

//    private void AttackMotion(int x, int y)
//    {
//        // sudden forward motion when attacking; 20 seems like a good number
//        int boostX = x * boostSpeed;
//        int boostY = y * boostSpeed;
//        rb.velocity = new Vector2(boostX, boostY);
//    }

//    private void SixWalk()
//    {
//        playerMoving = false;

//        // receive axis input values
//        float updown = Input.GetAxis("Vertical");
//        //updown *= -1f

//        // rigidbody move
//        //rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, updown * moveSpeed);
//        moveDir = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed, Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed, 0f);
//        transform.Translate(moveDir);
        

//        if (Input.GetAxis("Horizontal") > 0.2f || Input.GetAxis("Horizontal") < -0.2f)
//        {
//            playerMoving = true;
//            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
//        }


//        if (Input.GetAxis("Vertical") > 0.3f || Input.GetAxis("Vertical") < -0.3f)
//        {
//            playerMoving = true;
//            lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));

//        }

//        // Animation controls
//        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
//        anim.SetFloat("MoveY", updown);
//        anim.SetBool("PlayerMoving", playerMoving);
//        anim.SetFloat("LastMoveX", lastMove.x);
//        anim.SetFloat("LastMoveY", lastMove.y);

        
//    }

//    //private void playerDeath()
//    //{
//    //    if (healthSystem.GetHealth() == 0)
//    //    {
//    //        reloading = true;
//    //    }
//    //}

//    private void close()
//    {
//#if UNITY_EDITOR
//        UnityEditor.EditorApplication.isPlaying = false;
//#endif

//    }


//}
