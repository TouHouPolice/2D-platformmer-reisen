using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGround : MonoBehaviour {
    public GameObject explosionEffect;
    public float speed = 1f;
    public int HP = 3;
    public Transform WallCheck;   //used to check if there is wall in front of the monster
    bool wallInFront = false;        
    bool facingRight = false;                            
    float currentDirection = -1;
    public Vector2 pushForce = new Vector2(35f, 4f);
    private Vector2 oppositePushForce;
    private AudioSource myAudioSource;
    // Use this for initialization
    void Start () {
        oppositePushForce = new Vector2(-pushForce.x, pushForce.y);
        myAudioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        wallInFront= Physics2D.Linecast(transform.position, WallCheck.position, 1 << LayerMask.NameToLayer("Ground")); //similar to ground check, this to check if there is wall infront
        if (wallInFront == true)
        {
            Flip();
        }
        if (HP <= 0) //if being killed
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);  //create explosion effect
            AudioSource.PlayClipAtPoint(myAudioSource.clip,transform.position);
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        transform.position += new Vector3(speed * Time.fixedDeltaTime * currentDirection,0,0); // the movement is not using physics
    }
    void Flip()  //basic movement
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        currentDirection *= -1;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer == 9)  //same as other monster scripts
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController.flashTimeCountDown < 0 && playerController.isInvincible == false)  //if player is not invincible
                
            {
                
                Rigidbody2D pRb2d = other.gameObject.GetComponent<Rigidbody2D>();
                if (transform.position.x - other.transform.position.x < 0)  //add force accordingly
                {
                    pRb2d.velocity = pushForce;
                }
                else
                {
                    pRb2d.velocity = oppositePushForce;
                }
                playerController.TakingDamage();  //player react
                playerController.HP -= 1;
            }
        }
    }

}
