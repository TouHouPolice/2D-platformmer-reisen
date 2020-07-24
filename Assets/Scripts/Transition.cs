using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//this script is a compilation of functions used by UI buttons and etc
//all are very simple to understand
public class Transition : MonoBehaviour {
    public ScriptGameManager gameManager;
    public GameObject deathCanvas;
    public GameObject winCanvas;
    public GameObject screenCanvas;
    public GameObject startWall;
    

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("_GM").GetComponent<ScriptGameManager>();
        deathCanvas = GameObject.Find("DeathCanvas");
        winCanvas = GameObject.Find("WinCanvas");
        screenCanvas = GameObject.Find("ScreenCanvas");
        startWall = GameObject.Find("StartWall");

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale=1;
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ScreenCanvasDeactive()
    {
        screenCanvas.SetActive(false);
    }
    public void DeathCanvasActive()
    {
        deathCanvas.SetActive(true);
    }
    public void WinCanvasActive()
    {
        winCanvas.SetActive(true);
    }
    public void SetInActive(GameObject canvas)
    {
        canvas.SetActive(false);
    }

    public void SelfDeactive()
    {
        gameObject.SetActive(false);
    }
    public void StartTimer()
    {
        gameManager.StartCoroutine(gameManager.coroutine);
    }
    public void DestroyStartWall()
    {
        Destroy(startWall.gameObject);
    }
    public void LoadMainGame()
    {
        SceneManager.LoadScene("Main");
    }

   
}
