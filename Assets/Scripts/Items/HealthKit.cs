using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : MonoBehaviour {
    private GameObject itemsFolder;
    AudioSource myAudio;
    // Use this for initialization
    void Start()
    {
        itemsFolder = GameObject.Find("Items");
        gameObject.transform.parent = itemsFolder.transform; //same as other items' scripts
        myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))               //if collider is player
        {
            PlayerController Player = collision.gameObject.GetComponent<PlayerController>();
            Player.HP += 1;                                    //recover health

            AudioSource.PlayClipAtPoint(myAudio.clip, transform.position);
            DestroyObject(gameObject);

        }

    }
}
