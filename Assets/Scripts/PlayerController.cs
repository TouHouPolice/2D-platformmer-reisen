using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//some code is taken from tutorial or slides which I can't give URL credit
public class PlayerController : MonoBehaviour


{

    public bool cheatMode = false;  //if cheat mode is enabled
    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;
    [HideInInspector] public bool fire = false;
    [HideInInspector] public bool isDead = false;

    [HideInInspector] public bool doubleJumpAbility = false;     //have the ability or not 
    [HideInInspector] public float doubleJumpRemaining = 0f; //ability remaining time
  
    [HideInInspector] public bool isInvincible = false;  //same as above
    [HideInInspector] public float invincibleRemaining = 0f;
    [HideInInspector] public bool powerUp = false;   //same as above
    [HideInInspector] public float powerUpRemaining = 0f;


    public bool isTutorial = false;  // if is in tutorial mode
    public int HP = 3;  //player's health
    public float damageInvincibleTime = 1.2f;  //the amount of invincible time after taking damage
    public float flashTimeCountDown;          // invincible time count down after taking damage
    
    public int bulletDamage = 1;  //player's bullet's damage

    public Camera myCam;


    public GameObject bullet;
    public float moveforce = 100f;   //max acceleration
    public float maxSpeed = 3f;       
    public float jumpForce = 400f;
    public Transform groundCheck;
    public float firingRate=1f;
    public Transform shootStand;    // the vector 2 position to instantiate a bullet while standing
    public Transform shootSit;    // the vector 2 position to instantiate a bullet while crouching



    private bool doubleJump = false;           // if you have already double jumpped before landing
    
    private bool grounded = false;     // if standing on ground
    bool belted = false;                    // if the player is on a conveyor belt
    private Animator anim;
    private Rigidbody2D rb2d;
    float firingDelay;            //to define fire rate

    AudioSource shootAudio;
    AudioSource hitAudio;
    AudioSource jumpAudio;
    AudioSource killedAudio;
    AudioSource Run;
    //private float directionBulletOffset;
    ScriptGameManager GM;

    private float currentDirection = 1f;  //current facing
    // Use this for initialization
    void Start()
    {
        cheatMode = Difficulty.Cheat;  //get value from the static class
        GM = GameObject.Find("_GM").GetComponent<ScriptGameManager>();
        AudioSource[] audios = GetComponents<AudioSource>();
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        shootAudio = audios[0];
        hitAudio = audios[2];
        jumpAudio = audios[1];
        killedAudio = audios[3];
        Run = audios[4];
        if (cheatMode == true)  //if cheat enabled
        {
            isTutorial = true;
            HP = 999;
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isTutorial == false)       //if the player is not in tutorial mode, you will only have maximum of 3 HP
        {
            if (HP > 3)
            {
                HP = 3;
            }
        }
        if (HP <= 0)       //if no HP, die
        {
            isDead = true;
        }
        if (transform.position.y < -10f) //if fall out of map then die
        {
            isDead = true;
        }
        if (isDead)
        {
            AudioSource.PlayClipAtPoint(killedAudio.clip, transform.position);
        }

        GroundCheck();
        CharMovement();
        CountDown();
        DamagePowerUp();
        PlayerInvincible();

       
        
        //directionBulletOffset = 0.5f * transform.localScale.x;

    }
    void CountDown()
    {
        flashTimeCountDown -= Time.deltaTime; //here are all updating remaining time
        firingDelay -=  Time.deltaTime;
        invincibleRemaining -= Time.deltaTime;
        doubleJumpRemaining -= Time.deltaTime;
        powerUpRemaining -= Time.deltaTime;
        if (doubleJumpRemaining < 0)  //if remaining time is less than 0
        {
            doubleJumpAbility = false; //disable
        }
        if (invincibleRemaining < 0)
        {
            isInvincible = false;
        }
        if (powerUpRemaining < 0)
        {
            powerUp = false;
        }
        
    }

    void GroundCheck()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")); //check if on ground or belt
        belted= Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Belt")); 
        anim.SetBool("IsGrounded", grounded);
        if (belted == true)  //if on the belt, player can run faster
        {
            maxSpeed = 20f;
        }
        else
        {
            maxSpeed = 3f;
        }
    }

    void CharMovement()
    {
        if (Input.GetButtonDown("Jump") && doubleJumpAbility==true ) //press jump when you have double jump ability
        {
            if (grounded)    //if on the ground
            {
                jump = true;
                doubleJump = true;
            }
            else  //if not on the ground
            {
                if (doubleJump) //and your double jump is avaliable
                {
                    doubleJump = false;               //set to it unavaliable
                    rb2d.velocity = new Vector2(rb2d.velocity.x,0); //double jump
                    rb2d.AddForce(new Vector2(0, jumpForce));
                    jumpAudio.Play();
                }
            }

        }

        else if (Input.GetButtonDown("Jump") && grounded)  //if dont have double jump ability
        {
           
                jump = true;
                

        }
        if (Input.GetButton("Horizontal") && grounded)  //below is to set animation stat according to user input
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }

        if (Input.GetKey("down") && grounded)
        {
            anim.SetBool("IsSitting", true);
            //
        }
        else
        {
            anim.SetBool("IsSitting", false);
            //
        }
        if (Input.GetKeyDown("z") && firingDelay <= 0f)  
        {
            
            anim.SetTrigger("Fire");
            firingDelay = 1f / firingRate;
            Fire();
            Invoke("Fire", 0.2f);  //two bullet will be shot in one fire animation
        }
    }
    private void FixedUpdate()
    {
        HandleInput();

       
    }
    void HandleInput()
    {
        float h = Input.GetAxis("Horizontal");                      //change the value of h when press left or right arrow key
        anim.SetFloat("Speed", Mathf.Abs(h));                       //make h an absolute number and change the speed of the animator according to the number


        anim.SetFloat("VerticalVelocity", rb2d.velocity.y);

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)               //if reaches max speed
        {
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);   //make velocity constant according to it current direction

        }
        else if (h * rb2d.velocity.x < maxSpeed)                     //if does not reach the max speed
        {
            rb2d.AddForce(Vector2.right * h * moveforce);     //accelerate
        }

        if (h > 0 && !facingRight)         //if the character is moving right but not facing right                
        {
            Flip();                     //flip
        }
        else if (h < 0 && facingRight)        //opposite from above
        {

            Flip();
        }
        if (jump)         //if jump is ture
        {
            anim.SetTrigger("Jump");
            rb2d.AddForce(new Vector2(0f, jumpForce));   //play jump animation and add upward force
            jump = false;             //reset the bool to false
            jumpAudio.Play();
        }
    }

    void Fire ()
    {
        Vector3 forward = transform.position;
        forward.x += currentDirection;
        GameObject bulletInstance;
        if (anim.GetBool("IsSitting"))
        {
            
            
           bulletInstance = Instantiate(bullet,  shootSit.position, Quaternion.identity); //shoot when crouching
            
        }


        else
        {
           bulletInstance = Instantiate(bullet, shootStand.position, Quaternion.identity); //shoot when standing
        }

        Vector3 localScale = bulletInstance.transform.localScale; //set the orientation of bullet
        localScale.x *= currentDirection;
        bulletInstance.transform.localScale = localScale;
        shootAudio.Play();



    }

    void Flip() //basic
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        currentDirection *= -1;
        

    }
    private void OnDestroy() //normally it won't happen
    {
        myCam.transform.parent = null;
        isDead = true;

    }

    public void TakingDamage() //when taking damage
    {

        flashTimeCountDown = damageInvincibleTime; //refresh the damage invincible tiem
        StartCoroutine(DamageFlash());         //start to flash
        AudioSource.PlayClipAtPoint(hitAudio.clip, transform.position);
    }


    IEnumerator DamageFlash()//REF:https://www.youtube.com/watch?v=Z21fmziDu5c
    {
        
        {
            
            for (int i = 0; i < 6; i++)    //changing transparency repeatedly to flash

            {
                yield return new WaitForSeconds(0.1f);
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f); //Red, Green, Blue, Alpha/Transparency
                yield return new WaitForSeconds(0.1f);
                GetComponent<SpriteRenderer>().color = Color.white; //default color
                
            }
        }
    }
        /* void damageFlash()
         {
             if (flashActive)
             {
                 if (flashCounter > flashLength * (2f / 3f))
                 {
                     playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
                 }
                 else if (flashCounter > flashLength * (1f / 3f))
                 {
                     playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
                 }
                 else if (flashCounter > 0)
                 {
                     playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
                 }
                 else
                 {
                     playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
                     flashActive = false;
                 }
                 flashCounter -= Time.deltaTime;
             }


         }*/

    private void OnTriggerEnter2D(Collider2D collision)
        {
            /*if (collision.gameObject.layer == 10)
            {
                if (flashTimeCountDown < 0 && isInvincible==false)
                {
                TakingDamage();
                }
            }*/
        }

    void DamagePowerUp()   //ability to give player double damage
    {
        if (powerUp == true)
        {
            bulletDamage = 2;
        }
        else
        {
            bulletDamage = 1;
        }
    }
    void PlayerInvincible() //ablitiy to make player invincible and change color to half transparent
    {
        if (isInvincible == true)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    void CheckDeathDistance()  //when death is near, player the music
    {
        if (GM.deathDistance < 10f)
        {
            Run.Play();
        }
    }
    

}

    

    
