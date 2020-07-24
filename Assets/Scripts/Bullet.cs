using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//player's bullet
public class Bullet : MonoBehaviour {
    
    private bool facingRight;
    public float speed = 1f;
    public int bulletDamage;
    Rigidbody2D rb2d;
    Vector2 velocity;
    GameObject Player;
    public GameObject hitEffect;  //an animation
    
    // Use this for initialization
    private void Awake()
    {
        Player = GameObject.Find("Player");
        
    }
    void Start () {
        bulletDamage = Player.GetComponent<PlayerController>().bulletDamage; 
        facingRight = transform.localScale.x > 0f;  //check facing according to scale
        rb2d = GetComponent<Rigidbody2D>();
        speed = speed * Time.fixedDeltaTime;
        Invoke("DestroyBullet", 2f);           //self destroy after 2s
        velocity = Vector2.right * speed;

        if (facingRight)     //bullet fly to diffent direction according to player facing
        {
            rb2d.AddForce(velocity);
        }
        else
        {
            rb2d.AddForce(-velocity);
        }
    }

    private void FixedUpdate()
    {
        
        
    }
    // Update is called once per frame
    void Update () {
		
	}
    void DestroyBullet()

    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer == 8 || other.gameObject.layer == 10)  //if hit ground or monsters
        {
            if (other.CompareTag("MonsterAir"))
            {
                other.gameObject.GetComponent<Bee>().HP -= bulletDamage; //reduce monsters' hp
            }
            if (other.CompareTag("MonsterGround"))
            {
                other.gameObject.GetComponent<MonsterGround>().HP -= bulletDamage;
            }
            
            Instantiate(hitEffect, transform.position, Quaternion.identity);//create animaiton
            DestroyBullet();
        }
    }

    
}
