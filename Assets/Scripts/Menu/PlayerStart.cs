using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//player start means player gameObject at the start scene
public class PlayerStart : MonoBehaviour
{

    public float ForwardSpeed = 5f;   //
    public float BackwardSpeed = 2f;  //backward speed is lower to make it more cinematic(?)

    private Vector2 temp;
    [HideInInspector] public bool jump = false;
    private Rigidbody2D rb2d;
    // Use this for initialization
    void Start()
    {
        temp = transform.position;
        rb2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if  (transform.position.y > 6f)            //restrict the player's height since there is no ground check in this scene
        { 
            transform.position = new Vector2(transform.position.x, 6f);      
        }
    }
    private void FixedUpdate()
    {
        HandleInput();
        if (jump)         //if jump is ture
        {
            
            rb2d.AddForce(new Vector2(0f, 218f));   //play jump animation and add upward force
            jump = false;             //reset the bool to false
        }
    }

    void HandleInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))      //basic movement below
        {
            temp = transform.position;
            temp.x -= BackwardSpeed * Time.fixedDeltaTime;
            transform.position = temp;


        }
        if (Input.GetKey(KeyCode.RightArrow))
        {

            temp = transform.position;
            temp.x += ForwardSpeed * Time.fixedDeltaTime;
            transform.position = temp;

        }

        if (Input.GetButtonDown("Jump"))
        {


            jump = true;


        }
    }
}
