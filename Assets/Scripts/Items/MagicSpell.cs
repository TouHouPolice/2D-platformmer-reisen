using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSpell : MonoBehaviour {
    private GameObject death;
    AudioSource myAudio;
    public float pushBackDistance = 20f; //The amount of distance Death will be pushed back
    private GameObject itemsFolder;
    // Use this for initialization
    void Start () {
        myAudio = GetComponent<AudioSource>();
        death = GameObject.Find("Death");
        itemsFolder = GameObject.Find("Items");     //same as other items' scripts
        gameObject.transform.parent = itemsFolder.transform;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))  //when player pick up this item
        {
            Vector2 temp = death.transform.position;
            temp.x -= pushBackDistance;               
            death.transform.position = temp;                      //push back Death on x axis
            AudioSource.PlayClipAtPoint(myAudio.clip, transform.position);
            Destroy(gameObject);

        }

    }
}
