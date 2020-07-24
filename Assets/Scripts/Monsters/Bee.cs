using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour {
    public GameObject explosionEffect;
    public float patrolRange = 1.5f;   //patrol radius
    public float speed =1.5f;
    public GameObject Bullet;
    static GameObject Player;
    public float detectDistance=5f;  //detect player distance
    public int HP=2;
    public float fireInterval = 2.5f;
    public Vector2 pushForce = new Vector2(25f, 4f);
    private Vector2 oppositePushForce;
    private float maxY;         //patrol max y
    private float minY;             //partol min y

    private bool upwards = true; //for direction change
    private bool isInRange;       //player is in range

    private IEnumerator coroutine;
    private AudioSource myAudio;

	// Use this for initialization
	void Start () {
        oppositePushForce = new Vector2(-pushForce.x, pushForce.y);
        Player = GameObject.Find("Player");
        maxY = transform.position.y + patrolRange;                    //define patrol range according to initial position
        minY = transform.position.y - patrolRange;
        coroutine = WaitAndFire(fireInterval);                        //shoot after interval
        StartCoroutine(coroutine);
        myAudio = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Patrolling();
	}
    private void Update()
    {

        DistanceCheck();                 //check if the player is in range
        if (HP <= 0)   //if no health then
        {
            Instantiate(explosionEffect,transform.position, Quaternion.identity);   //instantiate explosion animation
            AudioSource.PlayClipAtPoint(myAudio.clip, transform.position);
            Destroy(gameObject);
        }
        if (Player.GetComponent<PlayerController>().isDead == true)  //if player is dead stop shooting
        {
            StopCoroutine(coroutine);
        }
    }

    void Patrolling()
    {
        if (transform.position.y > maxY)   //if reached the peak
        {
            upwards = false;
        }
        if (transform.position.y < minY)  //if reached the bottom
        {
            upwards = true;
        }
        Vector2 temp;
        if(upwards==true&& transform.position.y < maxY)  //basic movement
        {
            
            temp.y=  transform.position.y + speed * Time.fixedDeltaTime;
            temp.x = transform.position.x;
            transform.position = temp;
        }
        else
        {
            temp.y = transform.position.y - speed * Time.fixedDeltaTime;
            temp.x = transform.position.x;
            transform.position = temp;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer == 9)  //if hit player
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController.flashTimeCountDown<0 && playerController.isInvincible == false) //if player is not still in damage flash and invincible ability is not active
            
            {

                Rigidbody2D pRb2d = other.gameObject.GetComponent<Rigidbody2D>();
                if (transform.position.x - other.transform.position.x < 0)
                {
                    pRb2d.velocity = pushForce;          //add force to player
                }
                else
                {
                    pRb2d.velocity = oppositePushForce;
                }
                playerController.HP -= 1;  //player lose health
            }
        }
    }
    void DistanceCheck()
    {
        float targetDisatance = Mathf.Abs((transform.position - Player.transform.position).magnitude); //length between this object and player
        if (targetDisatance <= detectDistance)
        {
            isInRange =  true;
        }
        else
        {
            isInRange= false;
        }
    }
    IEnumerator WaitAndFire(float fireInterval)    //wait a while before next fire
    {
        
        while (true)
        {
            yield return new WaitForSeconds(fireInterval);
            if (isInRange)
            {
                Instantiate(Bullet, transform.position, Quaternion.identity);
            }
            
            

        }
    }

    
}
