using UnityEngine;

[CreateAssetMenu()]
public class Enemy : ScriptableObject
{

    public enum moveSpeed { walk, charge };
    public  moveSpeed movement;
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



}