using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool despawning;
    public GameObject enemy;
    public float range;
    public float distance;
    public SpriteRenderer e_sr;
    public Color eRed;
    private bool attackingDown;
    public Rigidbody2D rb;
    public HealthBar healthBar;
    public HealthSystem healthSystem;
    private bool dealDamage;
    //public EnemyController eControl;
    public EControl eControl;
    private float attackTimer;
    [SerializeField]
    private float attackRate;
    [SerializeField]
    private int playerHP;



    void Start () {
        attackTimer = attackRate;
        anim = GetComponent<Animator>();
        lastMove.y = -1.0f; // start player facing down
        Hide();
        enemy = GameObject.FindGameObjectWithTag("enemy");
        e_sr = enemy.GetComponent<SpriteRenderer>();
        eRed = new Color(250f, 0f, 0f);
        rb = GetComponent<Rigidbody2D>();
        healthSystem = new HealthSystem(playerHP);
        healthBar.Setup(healthSystem);
    }

	void Update () {
        Walk();
        canAttack();
        if (canAttack())
            Attack();
          

        if (despawning)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer < 0)
            {
                //Destroy(strike);
            }
        }
        
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
            }
            else if (lastMove.x == 1f)
            {
                StartCoroutine(AttackDir(strikeE));
                AttackCheck(strikeE);
            }
            else if (lastMove.y == 1f)
            {
                StartCoroutine(AttackDir(strikeN));
                AttackCheck(strikeN);
            }
            else
            {
                attackingDown = true;
                StartCoroutine(AttackDir(strikeS));
                AttackCheck(strikeS);
                
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
            //dealDamage = true;
            Debug.Log("Before hit: " + eControl.healthSystem.GetHealthPercent());
            eControl.healthSystem.Damage(20);
            Debug.Log("After hit: " + eControl.healthSystem.GetHealthPercent());
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
 
}
