using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* TODO:
 * add confirmed variable to player
 * 
 * 
 */

public class NPC : MonoBehaviour {

    [SerializeField]
    private Canvas[] messages;

    //public PlayerController pc;
    private bool dialogue;


    // Use this for initialization
    void Start () {

        messages = gameObject.GetComponentsInChildren<Canvas>();
        ToggleMessage(0);

        //pc.confirmed += Confirmed;
        dialogue = false;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ToggleMessage(1);

            // go to next message -- event
            
        }

        
    }

    private void ToggleMessage(int on)
    {
        if (on == 1)
        {
            
            for (int m = 0; m < messages.Length; m++)
            {
                messages[m].enabled = true;
                if (dialogue)
                {
                    messages[m].enabled = false;
                    continue;
                }
                
            }
        }

        else if (on == 0)
        {
            dialogue = false;
            for (int m = 0; m < messages.Length; m++)
            {
                messages[m].enabled = false;
            }
        }

        else
            Debug.LogError("Please use 1 or 0 for toggle message!");

    }

    //private void TurnOnMessage()
    //{
    //    for (int m = 0; m < messages.Length; m++)
    //    {
    //        messages[m].enabled = true;
    //    }
    //}

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ToggleMessage(0);
        }
    }

    //private void TurnOffMessage()
    //{
    //    for (int m = 0; m < messages.Length; m++)
    //    {
    //        messages[m].enabled = false;
    //    }

    //}


    private void Confirmed(object sender, System.EventArgs e)
    {
        dialogue = true;
        Debug.Log("Confirmed");
    }


}
