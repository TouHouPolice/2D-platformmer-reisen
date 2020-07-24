using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    private GameObject Player;
    public Transform BackGround1;
    public Transform BackGround2;          //get all the background objects
    public Transform BackGround3;
    public Transform BackGroundMiddle1;
    public Transform BackGroundMiddle2;
    public Transform BackGroundMiddle3;
    public float BackGroundSpeed = 3f;    // in this scene, the ones keep moving are the backgrounds instead of the player
    public float GroundSpeed = 8f;         //ground is also moving
    private IEnumerator coroutine;
    public Transform Ground1;
    public Transform Ground2;
    public Transform Ground3;

    public Slider GlobalVolume;
    private bool cheatMode;

    public GameObject cheatButton;
    public GameObject cancelButton;

    



    // Use this for initialization
    private void Awake()
    {
        Player = GameObject.Find("Player");
        
    }
    void Start () {
        
        cheatMode = Difficulty.Cheat;

        GlobalVolume.value = AudioListener.volume;  //set the slider position
        StartCoroutine(BackGroundLoop());           //make the background and ground loop
        StartCoroutine(GroundLoop());
        StartCoroutine(BackGroundMiddleLoop());
        if (cheatMode == true)      //this if statement is for UI element
        {
            cheatButton.SetActive(false);
            cancelButton.SetActive(true);
        }
        else
        {
            cheatButton.SetActive(true);
            cancelButton.SetActive(false);
        }
    }
    private void Update()
    {
        transform.position = Player.transform.position;
        BackGroundDeepMovement();                     //update background's position
        GroundMovement();          //update ground
        BackGroundMiddleMovement();  //update middle background

        
    }

    // Update is called once per frame
    void FixedUpdate () {
        
	}
    IEnumerator GroundLoop()             //its not necessary to use coroutine, I thought it might save some resources?
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);  // once per second

            if (Ground2.position.x < Player.transform.position.x)               //if player has passed current ground
            {
                Ground1.position = new Vector2(Ground3.position.x+30f , Ground1.position.y);        //place the previous ground to the front
            }
            if (Ground3.position.x < Player.transform.position.x)
            {
                Ground2.position = new Vector2(Ground1.position.x +30f, Ground2.position.y);
            }
            if (Ground1.position.x < Player.transform.position.x)
            {
                Ground3.position = new Vector2(Ground2.position.x +30f, Ground3.position.y);
            }
        }

    }
    void GroundMovement()  //basically keeps moveing the ground
    {
        Vector2 temp1;
        Vector2 temp2;
        Vector2 temp3;



        temp1.x = Ground1.position.x - (GroundSpeed * Time.deltaTime);
        temp1.y = Ground1.position.y;
        Ground1.position = temp1;

        temp2.x = Ground2.position.x - (GroundSpeed * Time.deltaTime);
        temp2.y = Ground2.position.y;
        Ground2.position = temp2;

        temp3.x = Ground3.position.x - (GroundSpeed * Time.deltaTime);
        temp3.y = Ground3.position.y;
        Ground3.position = temp3;
    }
    IEnumerator BackGroundLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            
            if (BackGround2.position.x < Player.transform.position.x)  //same as above
            {
                BackGround1.position = new Vector2(BackGround3.position.x + 45f, BackGround1.position.y);
            }
            if (BackGround3.position.x < Player.transform.position.x)
            {
                BackGround2.position = new Vector2(BackGround1.position.x + 45f, BackGround2.position.y);
            }
            if (BackGround1.position.x < Player.transform.position.x)
            {
                BackGround3.position = new Vector2(BackGround2.position.x + 45f, BackGround3.position.y);
            }
        }
        
    }
    IEnumerator BackGroundMiddleLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            if (BackGroundMiddle2.position.x+10f < Player.transform.position.x)  //same as above
            {
                BackGroundMiddle1.position = new Vector2(BackGroundMiddle3.position.x + 25f, BackGroundMiddle1.position.y);
            }
            if (BackGroundMiddle3.position.x+10f < Player.transform.position.x)
            {
                BackGroundMiddle2.position = new Vector2(BackGroundMiddle1.position.x + 25f, BackGroundMiddle2.position.y);
            }
            if (BackGroundMiddle1.position.x+10f < Player.transform.position.x)
            {
                BackGroundMiddle3.position = new Vector2(BackGroundMiddle2.position.x + 25f, BackGroundMiddle3.position.y);
            }
        }

    }
    void BackGroundDeepMovement()
    {
        Vector2 temp1;
        Vector2 temp2;
        Vector2 temp3;



        temp1.x = BackGround1.position.x - (BackGroundSpeed*0.5f * Time.deltaTime);
        temp1.y = BackGround1.position.y;
        BackGround1.position = temp1;

        temp2.x = BackGround2.position.x - (BackGroundSpeed* 0.5f * Time.deltaTime);
        temp2.y = BackGround2.position.y;
        BackGround2.position = temp2;

        temp3.x = BackGround3.position.x - (BackGroundSpeed * 0.5f * Time.deltaTime);
        temp3.y = BackGround3.position.y;
        BackGround3.position = temp3;
    }
    void BackGroundMiddleMovement()
    {
        Vector2 temp1;
        Vector2 temp2;
        Vector2 temp3;



        temp1.x = BackGroundMiddle1.position.x - (BackGroundSpeed * 2f* Time.deltaTime);
        temp1.y = BackGroundMiddle1.position.y;
        BackGroundMiddle1.position = temp1;

        temp2.x = BackGroundMiddle2.position.x - (BackGroundSpeed*2f * Time.deltaTime);
        temp2.y = BackGroundMiddle2.position.y;
        BackGroundMiddle2.position = temp2;

        temp3.x = BackGroundMiddle3.position.x - (BackGroundSpeed*2f * Time.deltaTime);
        temp3.y = BackGroundMiddle3.position.y;
        BackGroundMiddle3.position = temp3;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void ChangeGlobalVolume()          //attached to slider UI element to change volume
    {
        AudioListener.volume = GlobalVolume.value;
    }

    public void CheatEnable()  //attached to button UI element
    {
        Difficulty.Cheat = true; //change the variable in the static class
    }
    public void CheatDisable()
    {
        Difficulty.Cheat = false;
    }

    
}
