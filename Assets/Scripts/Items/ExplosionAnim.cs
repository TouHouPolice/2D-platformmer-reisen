using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAnim : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
