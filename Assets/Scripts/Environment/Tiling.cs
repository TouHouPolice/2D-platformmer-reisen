using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script was learnt online however as you can see it's been greatly modified to fit my game
//REF: https://www.youtube.com/watch?v=CwGjwnjmg2w


//this script is mainly used for background looping
public class Tiling : MonoBehaviour {

    public int offsetX = 2;   //detect a little bit before the exact point

    //check if need to instantiate new object
    public bool hasARightBuddy = false;
    public bool hasALeftBuddy = false;
    /* public bool reverseScale = false;  */  //jused if the object is not tilable

    public Transform previousBuddy;
    public Transform nextBuddy;
        
   /* private float spriteWidth = 0f;  */ //the width of element
                                      // Use this for initialization

    private Camera cam;
    private Transform myTransform;

    public float spawnDistance = 20f;

    private void Awake()
    {
        cam = Camera.main;
        myTransform = transform;
    }

    void Start () {
       /* SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();  

        spriteWidth = sRenderer.sprite.bounds.size.x; //get width
        print(spriteWidth);*/
	}
	
	// Update is called once per frame
	void Update () {
        //check if need to spawn
		if(hasALeftBuddy == false || hasARightBuddy==false)
        {
            //calculate the camras extend which is half width of the cam to get what the cam can see
            /*float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height; */ //the result is the distance between the centre of the cam to the border of the cam

            // calculate the x position where the cam can see the edge of the sprite
            /* float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtend;
             float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtend;*/

            //check if we can see the edge of the object and instantiate new
            /*if(cam.transform.position.x >=edgeVisiblePositionRight - offsetX && hasARightBuddy==false)*/
            if (cam.transform.position.x >= myTransform.position.x && hasARightBuddy == false)
            {
                MakeNewBuddy(1,nextBuddy);
                hasARightBuddy = true;
            }
            /*else if (cam.transform.position.x <= edgeVisiblePositionRight + offsetX && hasALeftBuddy == false)*/
            else if  (cam.transform.position.x <= myTransform.position.x && hasALeftBuddy == false)
            {
                MakeNewBuddy(-1,previousBuddy);
                hasALeftBuddy = true;
            }
        }
	}

    //instantiate object on the correct side
    void MakeNewBuddy(int rightOrLeft,Transform buddy)
    {
        /*Vector3 newPosition = new Vector3(myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);*/
        Vector3 newPosition = new Vector3((myTransform.position.x + spawnDistance)*rightOrLeft, myTransform.position.y, myTransform.position.z);

        //instantiate new buddy and store
        Transform newBuddy= Instantiate(buddy, newPosition, myTransform.rotation) as Transform;


        /*if (reverseScale == true)
        {
            //not really necessary since my sprite is not perfect, this line reverse the sprite
            newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1,newBuddy.localScale.y,newBuddy.localScale.z);
        }*/
        newBuddy.parent = myTransform.parent;
        if (rightOrLeft > 0)
        {
            newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;   //set hasAXBuddy of the new object to prevent endless instance
        }
        else
        {
            newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
        }
    }
}
