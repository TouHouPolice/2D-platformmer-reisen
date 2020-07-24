using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBox : MonoBehaviour {
    private GameObject itemsFolder;

    public GameObject invincible;    //get all the items that could generate here
    public GameObject powerUp;
    public GameObject doubleJump;
    public GameObject coin;
    public GameObject healthKit;
    public GameObject magicCard;
    public GameObject explosion;
    // Use this for initialization
    void Start () {
        itemsFolder = GameObject.Find("Items");
        gameObject.transform.parent = itemsFolder.transform;  //send into folder
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))                                  //if player's bullet hits it
        {
            float chance = Random.Range(0, 12);                        //same chance to drop any of the item below

            if (chance <= 2)
            {
                Instantiate(invincible, transform.position, Quaternion.identity);  
            }
            else if (chance >2 &&chance<=4)
            {
                Instantiate(powerUp, transform.position, Quaternion.identity);
            }
            else if (chance >4&& chance<=6)
            {
                Instantiate(doubleJump, transform.position, Quaternion.identity);
            }
            else if (chance > 6 && chance  <= 8)
            {
                Instantiate(healthKit, transform.position, Quaternion.identity);

            }
            else if(chance>8 && chance <= 10)
            {
                Instantiate(magicCard, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(coin, transform.position, Quaternion.identity);
            }
            Instantiate(explosion, transform.position, Quaternion.identity);  //play animation
            Destroy(gameObject);
        }
        
    }
}
