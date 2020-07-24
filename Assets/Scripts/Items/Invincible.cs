using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincible : MonoBehaviour {
    private GameObject itemsFolder;
    public float lastDuration = 10f;   //same as other items' scripts
    AudioSource myAudio;
    // Use this for initialization
    void Start () {
        itemsFolder = GameObject.Find("Items");
        gameObject.transform.parent = itemsFolder.transform;   //send into folder
        myAudio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController Player = collision.gameObject.GetComponent<PlayerController>();
            Player.isInvincible = true;                                 //activate Invincible abilibty
            Player.invincibleRemaining = lastDuration;                  //refresh ability remaining time
            AudioSource.PlayClipAtPoint(myAudio.clip, transform.position);   
            DestroyObject(gameObject);

        }
        
    }
}
