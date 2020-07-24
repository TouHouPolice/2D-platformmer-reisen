using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {

    public Vector2 pushForce = new Vector2(25f, 4f); //force applied to player when player is on the right
    private Vector2 oppositePushForce;   //when player on the left
    
    // Use this for initialization
    void Start () {
        oppositePushForce = new Vector2(-pushForce.x, pushForce.y);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))//check if the collider is player
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController.flashTimeCountDown < 0) //check if player just took damage
            { 
                
                Rigidbody2D pRb2d = collision.gameObject.GetComponent<Rigidbody2D>();
                if (transform.position.x - collision.transform.position.x < 0)                     
                {                                                                                 //add force on player to right or left
                    pRb2d.velocity = pushForce;
                }
                else
                {
                    pRb2d.velocity = oppositePushForce;
                }
                playerController.TakingDamage();                         //make player respond to damage
                playerController.HP -= 1;
        }
        }
    }
}
