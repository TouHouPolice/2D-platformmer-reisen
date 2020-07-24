using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    static GameObject Player;
    private Vector2 targetDirection;    //the direction to shoot the bullet
    public float bulletVelocity = 2f;   //speed of the bullet
    public Vector2 pushForce = new Vector2(25f, 4f);
    private Vector2 oppositePushForce;   
    private Rigidbody2D rb2d;


	// Use this for initialization
	void Start () {
        oppositePushForce = new Vector2(-pushForce.x, pushForce.y);
        Player = GameObject.Find("Player");
        rb2d=GetComponent<Rigidbody2D>();

        targetDirection = (Player.transform.position - transform.position).normalized; //get target direction
        rb2d.AddForce(targetDirection * bulletVelocity*Time.fixedDeltaTime); //push itself to the player

        Invoke("SelfDestroy", 3f);       //self destroy after a while to limit the range of bullet 
        
    }
	
	// Update is called once per frame
	
    void SelfDestroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9 ) //if hit player 
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController.flashTimeCountDown < 0 && playerController.isInvincible == false) //if not in any invincible status

            {

                Rigidbody2D pRb2d = collision.gameObject.GetComponent<Rigidbody2D>();   //same as other monster scripts
                if (transform.position.x - collision.transform.position.x < 0)  //add force to player to right or left accordingly
                {
                    pRb2d.velocity = pushForce;
                }
                else
                {
                    pRb2d.velocity = oppositePushForce;
                }
                playerController.TakingDamage();   //player respond to damage
                playerController.HP -= 1;
                SelfDestroy();  //if hit the player destroy the bullet it self
            }
            
        }
        else if(collision.gameObject.layer == 11)
        {
            SelfDestroy();
        }
    }
}
