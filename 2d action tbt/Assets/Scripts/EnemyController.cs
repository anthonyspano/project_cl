using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{

    private bool isMoving;
    public float moveSpeed;
    private Vector3 tPos;
    public GameObject followTarget;
    private float timer;
    private float parryWindow;
    private bool isAttacking;
    private bool parry;
    Color cRed;
    Color cGrey;
    public SpriteRenderer sr;
    private bool reloading;
    private float waitToReload;
    private float firstAttack;
    private bool canAttack;
    private float nextAttack;
    private float attackRate;
    private float attackRange;
    public HealthBar healthBar;
    public HealthSystem healthSystem;

    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        cRed = new Color(250f, 0f, 0f);
        cGrey = new Color(0f, 0f, 150f);
        parryWindow = 3f;
        timer = parryWindow;
        waitToReload = 2.0f;
        firstAttack = 1.0f;
        nextAttack = firstAttack;
        attackRate = 1.5f;
        attackRange = 0.8f;
        healthSystem = new HealthSystem(100);
        healthBar.Setup(healthSystem);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead())
        {
            Destroy(gameObject);
        }

        if (firstAttack > 0)
        {
            firstAttack -= Time.deltaTime;
        }

        else
        {
            canAttack = true;
        }

        if (!isAttacking) // moving
        {
            Walk();
        }

        float dX = transform.position.x - tPos.x;
        float dY = transform.position.y - tPos.y;
        if (dX * dX + dY * dY < attackRange)
        {
            if (canAttack)
                Attack();
            
        }

        parry = false;

        if (reloading)
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
        //timeToMoveCounter -= Time.deltaTime;
        tPos = followTarget.transform.position;

        if ((transform.position.x > tPos.x + 0.8f || transform.position.y > tPos.y + 0.8f) ||
              (transform.position.x < tPos.x - 0.8f || transform.position.y < tPos.y - 0.8f))
        {
            transform.position = Vector2.Lerp(transform.position, tPos, moveSpeed * Time.deltaTime);
            //Debug.Log("I'm moving");
        }

        else
        {
            //sr.color = cRed;
            isAttacking = true;
            if (parry)
                Parry();
        }

    }

    void Attack()
    {
        //Debug.Log("Time.time: " + Time.time);
        //Debug.Log("Timer: " + timer);
        if (timer < Time.time && canAttack && isAttacking)
        {
         
            // Counting down / how long the attack takes
            timer -= Time.deltaTime;
            sr.color = cRed;
            nextAttack = attackRate;
        }

        else
        {
            canAttack = false;
            //Debug.Log("Done");
            isAttacking = false;
            sr.color = cGrey;

            if (nextAttack > 0)
            {
                nextAttack -= Time.deltaTime;
            }

            else
            {
                firstAttack = attackRate;
                timer = Time.time + parryWindow;
            }
        }
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.name == "Player1")
        {
            // Death and respawn
            //other.gameObject.SetActive(false);
            //reloading = true;

            //StartCoroutine(LoadNewLevel());
        }

    }

    public void Parry()
    {
        Debug.Log("Parry Successful");
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

}