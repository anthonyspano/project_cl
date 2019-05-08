using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    BoxCollider2D bc;
    GameObject player;
    SpriteRenderer sr;
    GameObject targetPrefab;
    HealthSystem e_health;
    HealthBar e_healthBar;
    bool CanAttack;
    Vector3 tPos;
    float NextAttack;
    bool IsAttacking;
    bool targetChosen;
    GameObject targetPlace;
    Vector3 setPos;

    public int HP;
    public float AttackRate;
    public float Cooldown;
    public float ChargeSpeed;
    public float Wait;

    protected virtual void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        sr = GetComponent<SpriteRenderer>();

        targetPrefab = Resources.Load<GameObject>("Prefabs/SpawnPoint");

        HP = 100;
        //e_health = new HealthSystem(HP);
        e_healthBar.Setup(e_health);

    }

    private void Update()
    {
        if (CanAttack)
            Debug.Log("ISAttacking");
    }

    protected virtual void Walk()
    {
        // move gameObject at a constant speed towards player
        transform.position = Vector2.MoveTowards(transform.position, tPos, 3f * Time.deltaTime);
        Debug.Log(tPos);
        //Debug.Log("Cooldown: " + Cooldown);
    }



    protected void clearDecisionState()
    {
        // Attack()
        NextAttack = AttackRate;
        IsAttacking = false;
        Cooldown = AttackRate;
        // attackCheck()
        Wait = AttackRate;
        // Visual representation of decision state availability
        //sr.color = cGrey;
    }

    protected bool attackReady(ref float cd)
    {
        if (cd <= 0)
            return true;

        else
            return false;
    }

    protected void Delay(ref float cd)
    {
        cd -= Time.deltaTime;
    }

    IEnumerator Attack()
    {
        //Debug.Log(player.transform.position);
        // "charge" attack if wait > 0 and able to attack
        if (Wait > 0 && CanAttack && IsAttacking)
        {
            // Lock onto target
            //sr.color = cRed;
            // Determine target charge location
            //setPos = LockOn(tPos);
            //Debug.Log("Charge position: " + setPos);
            yield return new WaitForSeconds(Wait -= Time.deltaTime);
        }
        else
        {
            if (CanAttack)
            {
                //Attack now
                MoveToward(ChargeSpeed);
            }
            //clearDecisionState();
        }
    }

    protected Vector3 LockOn(Vector3 cPos)
    {
        if (!targetChosen)
        {
            // Create a placeholder for target position to charge to
            targetPlace = Instantiate(targetPrefab, cPos, Quaternion.identity);
            //Debug.Log("Target: " + targetPlace.transform.position);
            //Debug.Log("cPos: " + cPos);
            targetChosen = true;
            //setPos = targetPrefab.transform.position;
            //return targetPrefab.transform.position;
        }

        return targetPlace.transform.position;
    }

    protected virtual void MoveToward(float speed)
    {
        // set lock on position
        setPos = LockOn(tPos);

        // travel to location
        transform.position = Vector2.MoveTowards(transform.position, setPos, speed * Time.deltaTime);

        Debug.Log("MoveToward");
        //Debug.Log(transform.position);
        clearDecisionState();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            e_health.Damage(25);
        }
    }

    protected void LocatePlayer()
    {
        tPos = player.transform.position;
    }
}
