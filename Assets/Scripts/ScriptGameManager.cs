using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


using System;
//lots of code here are UI related

public class ScriptGameManager : MonoBehaviour {
    public GameObject Openning;
    bool PlayerIsDead=false;
    public GameObject player;
    public GameObject destination;       //refer to destination of the game
    private float timer;           //refer to amount of time passed since start
    private float distanceLeft = 0;   //refer to distance between player and destination
    public GameObject deathCanvas;   //UI element
    public GameObject winCanvas;   //UI element
    [HideInInspector]public float coinCollected = 0;
    private PlayerController playerController;
    private bool doubleJumpAbility;   //these refer to if the player's ability is active
    private bool isInvincible;
    private bool powerUp;
    private int HP;
    private AudioSource[] audios;
    AudioSource oppenning;
    private AudioSource BGM;
    AudioSource Run;
    
    
    public GameObject explosion;
    public Camera myCam;
    public GameObject Death;
    [HideInInspector]public float deathDistance;  //refer to distance between player and death


    //below for GUI display
    public GameObject screenCanvas;
   

    public GameObject healthBW1; //all are UI elements
    public GameObject healthBW2;
    public GameObject healthBW3;
    public GameObject portrait1;
    public GameObject portrait2;
    public GameObject portrait3;
    public GameObject wings;
    public GameObject PW;
    public GameObject star;
    public GameObject warning;
    public Slider GlobalVolume;



    public Text deathDistanceText;

    public Text distanceCount;
    public Text coinCount;
    public Text timeCount;


    public Text coinCountDead;
    public Text distanceCountDead;

    public Text coinCountWin;
    public Text timeCountWin;

    [HideInInspector]public IEnumerator coroutine;
    private void Awake()
    {
        
    }
    // Use this for initialization
    void Start () {
        GlobalVolume.value = AudioListener.volume;  //set the slider position
        player = GameObject.Find("Player");
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        audios = GetComponents<AudioSource>();
        oppenning = audios[0];                  //get all the audio sources
        Run = audios[1];
        BGM = audios[2];
        BGM.volume = 0.4f;         //lower the volume of bgm
        AudioSource.PlayClipAtPoint(oppenning.clip, player.transform.position);
        

        Openning.SetActive(true); //active the openning aniamtion
        timer = 0;
        coroutine = Timer();  //to record the amount of time passed
        PlayerIsDead = false;
        destination = GameObject.FindGameObjectWithTag("Destination");
        
        
        
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position; //make the GM game object follow the player(for audio purpose)
        GetStatus();
        HealthBarDisplay();
        ActiveItemDisplay();
        UpdateText();
        FindDistance();
        PlayerIsDead = player.GetComponent<PlayerController>().isDead;
        CheckDeathDistance();
        BGMCheck();

        if (PlayerIsDead == true) //if the player is dead, then do something to those UI
        {
            
            myCam.transform.parent = null;
            player.SetActive(false);
            StopCoroutine(coroutine);
            screenCanvas.SetActive(false);
            deathCanvas.SetActive(true);
            SetTextDead();
        }
        
        if (distanceLeft < 1.4f)   //if the player has reached the destination, do thigns to ui
        {

            
            myCam.transform.parent = null;
            player.SetActive(false);
            StopCoroutine(coroutine);
            screenCanvas.SetActive(false);
            winCanvas.SetActive(true);
            SetTextWin();
            Death.SetActive(false);

        }

	}
   public IEnumerator Timer()  //amount of time passed
    {
        while (player != null)
        {
            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;
        }
    }

    void GetStatus()  //display player's status on UI
    {
        HP = playerController.HP;
        isInvincible = playerController.isInvincible;
        powerUp = playerController.powerUp;
        doubleJumpAbility = playerController.doubleJumpAbility;
    }

    
    void FindDistance()  //update distance between player and destination
    {
        distanceLeft = Mathf.Abs((destination.transform.position - player.transform.position).magnitude);
    }

    public void LoadMenu()   //functions below are used by UI element
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuiteGame()
    {
        Application.Quit();
    }
    public void StartTimer()
    {
        
        StartCoroutine(Timer());
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    void HealthBarDisplay()  //displayer player health
    {
        switch (HP)
        {
            case 3:
                portrait1.SetActive(true);
                portrait2.SetActive(false);
                portrait3.SetActive(false);
                healthBW1.SetActive(false);
                healthBW2.SetActive(false);
                healthBW3.SetActive(false);
                break;

            case 2:
                portrait1.SetActive(false);
                portrait2.SetActive(true);
                portrait3.SetActive(false);
                healthBW1.SetActive(false);
                healthBW2.SetActive(false);
                healthBW3.SetActive(true);
                break;

            case 1:
                portrait1.SetActive(false);
                portrait2.SetActive(false);
                portrait3.SetActive(true);
                healthBW1.SetActive(false);
                healthBW2.SetActive(true);
                healthBW3.SetActive(true);
                break;

            case 0:
                
                healthBW1.SetActive(true);
                healthBW2.SetActive(true);
                healthBW3.SetActive(true);
                break;
        }
    }
    void ActiveItemDisplay()  //displayer active ability
    {
        if (powerUp)
        {
            PW.SetActive(true);
        }
        else
        {
            PW.SetActive(false);
        }

        if (doubleJumpAbility)
        {
            wings.SetActive(true);

        }

        else
        {
            wings.SetActive(false);

        }

        if (isInvincible)
        {
            star.SetActive(true);

        }

        else
        {
            star.SetActive(false);

        }
    }
    void UpdateText()  //text in UI
    {
        coinCount.text = "X" +" "+ coinCollected.ToString();

        
        float roundTime = Mathf.Round(timer * 10.0f) * 0.1f;  //1 decimal place
        timeCount.text = "X" + " " + roundTime.ToString();
        float roundDistance = Mathf.Round(distanceLeft * 1.0f); //leave integer
        distanceCount.text =" "+ roundDistance.ToString();
    }
    void SetTextDead()  // text you will see when dead
    {
        coinCountDead.text = "X" + " " + coinCollected.ToString();
        float roundDistance = Mathf.Round(distanceLeft * 1.0f);
        distanceCountDead.text = " " + roundDistance.ToString();
    }
    void SetTextWin()  //text you will see when win
    {
        coinCountWin.text = "X" + " " + coinCollected.ToString();
        float roundTime = Mathf.Round(timer * 10.0f) * 0.1f;
        timeCountWin.text = "X" + " " + roundTime.ToString();
    }
    public void LoadMainGame()
    {
        if (Difficulty.level == 1)
        {
            SceneManager.LoadScene("Level1");
        }
        else if (Difficulty.level == 2)
        {
            SceneManager.LoadScene("Main");
        }
    }
    void CheckDeathDistance()  //check the distance between player and death
    {
        deathDistance = Mathf.Abs((player.transform.position - Death.transform.position).magnitude);
        float roundDistance = Mathf.Round(deathDistance * 10.0f) * 0.1f;
        if (deathDistance < 15f)  //if death is near
        {
            warning.SetActive(true);   //show distance to player
            deathDistanceText.text = roundDistance.ToString();
            if (Run.isPlaying == false)  //player intense bgm, pause original bgm
            {
                Run.Play();
                BGM.Pause();
            }
        }
        else
        {
            warning.SetActive(false);  //if got away, stop intense bgm
            Run.Pause();

        }
    }
    void BGMCheck()  //check bgm status, make sure there is always bgm playing
    {
        if (Run.isPlaying == false && BGM.isPlaying == false)
        {
            BGM.Play();
        }
    }
        
    
    public void ChangeGlobalVolume() //attached to slider ui element
    {
        AudioListener.volume = GlobalVolume.value;
    }

}
