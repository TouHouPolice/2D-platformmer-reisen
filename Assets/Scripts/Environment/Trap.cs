using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {
    public float Delay = 1.3f; //time before the platform falls
    Rigidbody2D rb2d;
	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))  //if player touch the platform
        {
            
            Invoke("PlatformFall", Delay);
        }
    }

    void PlatformFall()
    {
        rb2d.isKinematic = false;  //make it free fall
    }
}
