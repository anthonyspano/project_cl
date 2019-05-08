using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject p;

    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {



    }

    private void Awake()
    {
        p = GameObject.Find("Player");
        // difference between the current enemy position and the player
        direction = p.transform.position - transform.position;
        Debug.Log(direction);
        StartCoroutine(Lifetime());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction = direction.normalized;

        transform.Translate(direction);
        
    }

    IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }
}
