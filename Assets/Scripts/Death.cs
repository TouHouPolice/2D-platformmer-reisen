using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Death is the thing chasing the player
public class Death : MonoBehaviour {
    public float speed;
    public GameObject player;
    PlayerController playerController;
    public Vector2 myPosition;
	// Use this for initialization
	void Start () {
        speed = Difficulty.Speed; //get speed from static class
        playerController = player.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Movement();
        myPosition=transform.position;
	}
    private void Update()
    {
        if ((player.transform.position - transform.position).magnitude < 1.4f)    //use distance calculation instead of ontriggerEnter is to prevent unkonwn error
                                                                                //that error cause player to die without reason
        {
            playerController.isDead = true;
        }
    }
    void Movement()  //basically keep tracking the player
    {
        Vector2 temp;
        Vector2 targetDirection = (player.transform.position - transform.position).normalized;//get unit vector
        temp.x = transform.position.x +targetDirection.x* speed * Time.fixedDeltaTime;
        temp.y = transform.position.y +targetDirection.y*speed*Time.fixedDeltaTime;
        transform.position = temp;
    }

    
}
