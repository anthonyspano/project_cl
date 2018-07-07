using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private readonly float jumpPower = 8.0f;
    private Rigidbody2D rb;

    [SerializeField] public bool isGrounded;
    //charactercontroller vs rigidbody

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        Walk();
        Jump();
    }

    void Walk()
    {
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Translate(Vector3.right * Time.deltaTime * playerSpeed);
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Translate(Vector3.left * Time.deltaTime * playerSpeed);
    }

    void Jump()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space)) // rigidbody vs translate
                rb.AddForce(transform.up * jumpPower, ForceMode2D.Impulse); // * Time.deltaTime
        }

    }


    //void OnCollisionEnter(Collision2D theCollision)
    //{
    //    if (theCollision.gameObject.tag == "ground")
    //    {
    //        Debug.Log("on the ground");
    //        isGrounded = true;
    //    }

    //}

    //void OnCollisionExit2D(Collision2D theCollision)
    //{
    //    if (theCollision.gameObject.tag == "ground")
    //        isGrounded = false;
    //}
}
