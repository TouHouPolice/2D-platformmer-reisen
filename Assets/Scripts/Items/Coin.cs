using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    private GameObject GM;     
    private GameObject CoinsFolder;
    private AudioSource myAudio;

	// Use this for initialization
	void Start () {
        myAudio = GetComponent<AudioSource>();
        CoinsFolder = GameObject.Find("Coins");                 
        GM =GameObject.Find("_GM");
        gameObject.transform.parent = CoinsFolder.transform;        //get the coin game object into the folder so it's less messy
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GM.GetComponent<ScriptGameManager>().coinCollected += 1;                  //Displayed number of coins +1
            AudioSource.PlayClipAtPoint(myAudio.clip, transform.position);        //play coin pick up sound
            Destroy(gameObject);
        }
    }
}
