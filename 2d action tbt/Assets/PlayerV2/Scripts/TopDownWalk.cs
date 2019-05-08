using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Moving a character in a top-down game
// flip the player
public class TopDownWalk : MonoBehaviour
{
    private Animator myAnim;
    private Vector2 lastMove;
    private bool playerMoving;
    private Vector3 moveDir;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float deadZone;

    private Transform hitBox;
    private Transform hitBoxRef;
    private BoxCollider2D bc;

    private float hb_positionOffset;

    
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        // start player facing down
        lastMove.y = -1.0f;

        hitBox = transform.Find("Strike");
        hitBoxRef = transform.Find("StrikeRef");
        // hitbox position offset from center of player (depending on dir facing)
        hb_positionOffset = hitBoxRef.GetComponent<BoxCollider2D>().bounds.extents.x;
        // get player hitbox for reference
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
    }

    private void Walk()
    {
        playerMoving = false;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveDir = new Vector3(horizontal * Time.deltaTime * moveSpeed, vertical * Time.deltaTime * moveSpeed, 0f);
        transform.Translate(moveDir);

        // HORIZONTAL
        if (horizontal > deadZone || horizontal < -deadZone)
        {
            playerMoving = true;

            // If Left
            if (isAxis(-horizontal))
            {
                horizontal = -1;
                if (lastMove != new Vector2(horizontal, 0f))
                {
                    hitBox.position = gameObject.transform.position - new Vector3(hb_positionOffset, 0, 0);
                    hitBox.eulerAngles = new Vector3(0, 0, 0);
                }
            }

            // If Right
            if (isAxis(horizontal))
            {
                horizontal = 1;
                if (lastMove != new Vector2(horizontal, 0f))
                {
                    hitBox.position = gameObject.transform.position + new Vector3(hb_positionOffset, 0, 0);
                    hitBox.eulerAngles = new Vector3(0, 0, 0);
                }
            }

            lastMove = new Vector2(horizontal, 0f);            
        }

        // VERTICAL
        if (vertical > deadZone || vertical < -deadZone)
        {
            playerMoving = true;

            // If Down
            if (isAxis(-vertical))
            {
                // floor the value
                vertical = -1;
                // pivot downwards
                if (lastMove != new Vector2(0f, vertical))
                {
                    // reposition the hitbox depending on the direction player is facing
                    hitBox.position = gameObject.transform.position - new Vector3(0, hb_positionOffset, 0);

                    // rotate hitbox 90 degrees
                    hitBox.eulerAngles = new Vector3(0, 0, 90);
                }

            }

            // If Up
            else if (isAxis(vertical))
            {
                // floor the value
                vertical = 1;
                // pivot downwards
                if (lastMove != new Vector2(0f, vertical))
                {
                    hitBox.position = gameObject.transform.position + new Vector3(0, hb_positionOffset, 0);
                    hitBox.eulerAngles = new Vector3(0, 0, 90);
                }

            }

            lastMove = new Vector2(0f, vertical);
            
        }

        // Animation controls
        myAnim.SetFloat("MoveX", horizontal);
        myAnim.SetFloat("MoveY", vertical);
        myAnim.SetBool("PlayerMoving", playerMoving);
        myAnim.SetFloat("LastMoveX", lastMove.x);
        myAnim.SetFloat("LastMoveY", lastMove.y);

    }

    bool isAxis(float x)
    {
        return (x > 0 ? true : false);
    }
}
