using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy<T> where T : Enemy
{
    public GameObject GameObject;
    public T ScriptComponent;

    public Enemy(string name)
    {
        GameObject = new GameObject(name);
        ScriptComponent = GameObject.AddComponent<T>();
    }
}

public abstract class Enemy : MonoBehaviour
{
    public enum moveSpeed { walk, charge };
    //public moveSpeed movement;
    public bool isMoving;
    //public float moveSpeed;
    public Vector3 tPos;
    public GameObject followTarget;
    [SerializeField]
    public float wait;    // wait period before attack (turns red)
    [SerializeField]
    public bool isAttacking;
    public Color cRed;
    public Color cGrey;
    public SpriteRenderer sr;
    [SerializeField]
    public float coolDown;  // central timer for attack cooldown
    public bool canAttack;
    [SerializeField]
    public float nextAttack;   // time before enemy can attack again
    [SerializeField]
    public float attackRate;
    [SerializeField]
    public float attackRange;
    public HealthBar healthBar;
    public HealthSystem healthSystem;
    public float dX;
    public float dY;
    public float distance;
    public float range;
    public GameObject strike;
    public GameObject player;
    public PlayerController pControl;
    [SerializeField]
    public int eHealth;
    public Vector2 chargePos;
    private BoxCollider2D bc;
    private Animator anim;
    private RuntimeAnimatorController blank; // how to get animator controller ?

    private void Awake()
    {
        // Instantiating all components of the game object
        sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>("badguy");
        gameObject.tag = "enemy";
        gameObject.layer = LayerMask.NameToLayer("Characters");
        bc = gameObject.AddComponent<BoxCollider2D>();
        gameObject.AddComponent<Animator>();
        anim = gameObject.GetComponent<Animator>();
        anim.runtimeAnimatorController = blank; // find the controller in the asset library
    }


    public virtual void Initialize(Vector2 position)
    {
        transform.position = position;
    }

    protected abstract void Patrol();
}

public class Acolyte : Enemy {

    public float Cooldown
    {
        get { return coolDown; }
        set { coolDown = 3f; }
    }
    

    private void FixedUpdate()
    {
        Patrol();
    }

    protected override void Patrol()
    {
        // To be implemented...
        
    }

}
