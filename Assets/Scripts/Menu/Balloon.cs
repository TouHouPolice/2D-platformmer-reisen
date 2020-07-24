using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//this script is only used in menu for level selection
public class Balloon : MonoBehaviour {

    Animator anim;
    public float easySpeed = 0.8f;      //the speed of Death in easy mode
    public float difficultSpeed = 1.3f;  //the speed of Death in difficult mode
    public static int levelSelection = 1;    //load different level according to this variable
    
	// Use this for initialization
	void Start () {
        print(gameObject.name);
        anim = GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void DestroyBalloon()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetTrigger("Explode");
        if (gameObject.name == "balloonTutorial")
        {
            Difficulty.level = 0;               //Tutorial level won't have anything chasing the player so no speed
        }
        if (gameObject.name == "balloonStartEasy")
        {
            Difficulty.Speed = easySpeed;        //change the variable in a static class called Difficulty
                              //speed refers to the Death that keeps chasing the player, if speed is higher, the more difficult 
        }
        else if (gameObject.name == "balloonStartDifficult")
        {
            Difficulty.Speed =difficultSpeed;         //as above
            
        }
        
    }
    private void OnDestroy()
    {
        if (Difficulty.level==0)
        {
            SceneManager.LoadScene("Tutorial");        //when balloon explodes, load the level
        }
        else if (Difficulty.level == 1)
        {
            SceneManager.LoadScene("Level1");
        }
        else if(Difficulty.level == 2)
        {
            SceneManager.LoadScene("Main");
        }
    }

    public void SelectLevel1()
    {
        Difficulty.level = 1;
    }
    public void SelectLevel2()
    {
        Difficulty.level = 2;
    }

    
}
