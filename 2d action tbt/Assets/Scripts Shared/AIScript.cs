using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// barely works xD
public class AIScript : MonoBehaviour
{
    public float sightRange;
    public float attackRange;
    private float chargeTime;
    private int maxHealth;
    public int criticalHP;
    private GameObject player;
    private HealthSystem myHealth;
    private HealthBar myHealthBar;
    private bool doneCharging;
    private SpriteRenderer sr;
    private Vector3 setPos;
    private GameObject targetPlace;
    private bool targetChosen;
    private float coolDown;
    private bool Moving;
    private bool Attacking;
    private GameObject bc;

    public float attackRate;
    public GameObject target;
    public GameObject targetPrefab;
    public float moveSpeed;
    public float chargeSpeed;
    public float wait;
    private float invulnTimer;


    private MyHealth currentHealth;




    private void OnDrawGizmos()
    {
        // Draw circles around unit to show range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    // Start is called before the first frame update
    void Start()
    {
        // hitbox
        bc = transform.Find("Hitbox").gameObject;
        bc.SetActive(false);
        // set the timer to the rate of attack
        coolDown = attackRate;
        // enemy's health
        myHealth = new HealthSystem(maxHealth, invulnTimer);
        myHealthBar = GetComponent<HealthBar>();
        currentHealth = gameObject.GetComponent<MyHealth>();
        // target
        target = GameObject.Find("Player");
        // Debug
        sr = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Insight: " + InSight(target));
        //Debug.Log("InRange: " + InRange(target));

        // Death
        if(currentHealth.healthSystem.GetHealth() == 0)
        {
            Destroy(gameObject);
        }

        // Reassess
        if (!InRange(target))
        {
            Attacking = false;
        }

        // Reset coolDown
        if (coolDown != 0)
        {
            coolDown -= Time.deltaTime;
        }

        // Disengage
        if (!InSight(target))
        {
            Moving = false;
            Attacking = false;
        }

        // Vision circle
        if (InSight(target) && !InRange(target))
        {
            Moving = true;
            Attacking = false;
            Move();
        }

        // Attack Circle
        if (InRange(target) && coolDown <= 0)
        {
            Moving = false;
            Attacking = true;
            ChargeAttack();
            StartCoroutine(Attack(setPos));
        }
        else
            Attacking = false;

        // 
        if(IsLowHealth() && InRange(target))
        {
            // move until !InRange
            
        }

        if(IsLowHealth() && !InRange(target))
        {
            // Recover

            // Shoot
        }

    }


    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
    }

    void ChargeAttack()
    {
        // Indicator
        sr.color = Color.red;

        setPos = LockOn(target.transform.position);
    }

    IEnumerator Attack(Vector3 lockOnPos)
    {
        yield return new WaitForSeconds(wait);
        bc.SetActive(true);
        // Performs a charging attack
        // move to player
        transform.position = Vector3.MoveTowards(transform.position, lockOnPos, chargeSpeed * Time.deltaTime);
        //Debug.Log("Me: " + transform.position);
        //Debug.Log(lockOnPos);

        if (transform.position == lockOnPos)
        {
            targetChosen = false;
            Destroy(targetPlace);
            coolDown = attackRate;
            bc.SetActive(false);
        }
        yield return (transform.position == lockOnPos || Attacking == false);
        sr.color = Color.blue;
    }

    void Flee()
    {

    }

    void Recover()
    {

    }

    void Shoot()
    {

    }

    bool InSight(GameObject target)
    {
        float dX = transform.position.x - target.transform.position.x;
        float dY = transform.position.y - target.transform.position.y;

        //Debug.Log("dX: " + dX + " dY: " + dY);
        //Debug.Log("sightRange: " + sightRange);
        if (dX < sightRange && dY < sightRange)
        {
            return true;
        }

        else
            return false;
    }

    bool InRange(GameObject target)
    {
        float dX = transform.position.x - target.transform.position.x;
        float dY = transform.position.y - target.transform.position.y;

        if (dX < attackRange && dY < attackRange)
        {
            return true;
        }

        else
            return false;
    }

    bool IsLowHealth()
    {
        if (myHealth.GetHealthPercent() < criticalHP)
        {
            return true;
        }

        else
            return false;
    }


    private Vector3 LockOn(Vector3 pos)
    {
        if (!targetChosen)
        {
            // Create a placeholder for target position to charge to
            targetPlace = Instantiate(targetPrefab, pos, Quaternion.identity);

            targetChosen = true;
        }
        return targetPlace.transform.position;
    }
}
