using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour {

    public GameObject coin;
    public float minNum=0;          //max and min number is for random number generation
    public float maxNum=10;
    public float treshold = 4;        //used as below to create a possibility
    float randomNum;
	// Use this for initialization
	void Start () {
        randomNum = Random.Range(minNum, maxNum);
        
        if (randomNum <= treshold)              //use this treshold to adjust the possibility
        {
            Instantiate(coin, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
   /* void SelfDestroy()
    {
        Destroy(gameObject);
    }*/
}
