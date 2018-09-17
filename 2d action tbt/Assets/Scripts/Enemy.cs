using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for all enemies
// Variables to be changed on a mass scale
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
    protected float run;
    public float wait;    // wait period before attack (turns red)
    public float coolDown;  // central timer for attack cooldown
    public float nextAttack;   // time before enemy can attack again
    public float attackRate;
    public float attackRange;
    public int eHealth;


    //private void Awake()
    //{
    //    // Instantiating all components of the game object
    //    LoadSprite();

    //    gameObject.tag = "enemy";
    //    gameObject.layer = LayerMask.NameToLayer("Characters");
    //    gameObject.GetComponent<BoxCollider2D>();
    //    gameObject.GetComponent<Animator>();
    //    //anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("hood32_idle_0");  // find the controller in the asset library
    //}

    //private bool LoadSprite()
    //{
    //    sr = gameObject.GetComponent<SpriteRenderer>();
    //    sr.sprite = Resources.Load<Sprite>("Sprites/Enemies/badguy");
    //    if (Resources.Load<Sprite>("Sprites/Enemies/badguy"))
    //    {
    //        Debug.Log("Sprite loaded successfully!");
    //        return true;
    //    }

    //    else
    //    {
    //        Debug.Log("Sprite load failed!");
    //        return false;
    //    }

    //}

    //public virtual void Initialize(Vector3 position)
    //{
    //    transform.position = position;
    //}

    //protected abstract void Patrol();
}

public class Acolyte : Enemy    // TBI
{
    public float Cooldown
    {
        get { return coolDown; }
        set { coolDown = 3f; }
    }

    public float NextAttack
    {
        get { return nextAttack; }
        set { nextAttack = 0.5f; }
    }

    public float AttackRange
    {
        get { return attackRange; }
        set { attackRange = 2f; }
    }

    public int E_Health
    {
        get { return eHealth; }
        set { eHealth = 100; }
    }

    public float Wait
    {
        get { return wait; }
        set { wait = 2f; }
    }

    public float Run
    {
        get { return run; }
        set { run = 2f; }
    }

}
