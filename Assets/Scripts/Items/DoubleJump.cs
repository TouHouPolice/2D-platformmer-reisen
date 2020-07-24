using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour {
    private GameObject itemsFolder;
    public float lastDuration = 10f;  //amont of time the ability will last
    AudioSource myAudio;
    // Use this for initialization
    void Start () {
        itemsFolder = GameObject.Find("Items");
        gameObject.transform.parent = itemsFolder.transform;        //put the item into item folder
        myAudio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))   //if the collider is player
        {
            PlayerController Player = collision.gameObject.GetComponent<PlayerController>();
            Player.doubleJumpRemaining = lastDuration;                           //refresh the ability remaining time
            Player.doubleJumpAbility = true;                                  //activate the ability
            AudioSource.PlayClipAtPoint(myAudio.clip, transform.position);
            DestroyObject(gameObject);

        }
       
    }
}
