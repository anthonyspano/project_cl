using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignDamage : MonoBehaviour
{
    public string tagName;
    public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tagName))
        {
            collision.gameObject.GetComponentInChildren<HealthBar>().healthSystem.Damage(damage);
            Debug.Log("Enemy hit");
        }

        //if (collision.gameObject.CompareTag("Enemy"))
        //{
        //    collision.gameObject.GetComponentInChildren<HealthBar>().healthSystem.Damage(damage);
        //    //Debug.Log(collision.gameObject.GetComponentInChildren<HealthBar>().healthSystem.GetHealthPercent());
        //}
    }
}
