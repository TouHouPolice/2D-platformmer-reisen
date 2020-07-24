using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//The majority of the script was learnt online
//REF： https://www.youtube.com/watch?v=5E5_Fquw7BM&t=4s



//this script is basically to move the background in different speed in accordance to the camera to create parallaxing effect
public class Parallaxing : MonoBehaviour {

    private GameObject[] backgrounds;          //list of background to be parallaxed
    private float[] parallaxScales;         //level of effect for differnet backgrounds    
    public float smoothing=1f;             //How smooth the para, basically change the speed of the background movement

    private Transform cam;         
    private Vector3 previousCamPos;             //The position of the camera in the frame before

    void Awake()
    {
        
        cam = Camera.main.transform;
    }
	// Use this for initialization
	void Start () {
        backgrounds = GameObject.FindGameObjectsWithTag("BackGround");
        
            //first cam position is going to be the previousCamPos in the next frame
        previousCamPos = cam.position;

        parallaxScales = new float[backgrounds.Length]; //make parallaxscales equal length as the backgrounds
        //asigning coresponding parallaxingScales
        for(int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].transform.position.z * -1;  //without -1 will get opposite effect, get all z axis into the float array

        }
       /* StartCoroutine(findBackground());*/
	}

   /* IEnumerator findBackground()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            
        }
    }*/
	
	// Update is called once per frame
	void Update () {
        
        backgrounds = GameObject.FindGameObjectsWithTag("BackGround");
        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].transform.position.z * -1;  //without -1 will get opposite effect, get all z axis into the float array

        }
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // the parallax is the opposite of the camera movement because the prev frame multiplied by the scale
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            //set a target x pos which is the current position plus the parallax
            float backgroundTargetPosX = backgrounds[i].transform.position.x + parallax;

            //crate a target position which is the background's current position with its target x position

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].transform.position.y, backgrounds[i].transform.position.z);
            //fade between current pos and  the target pos using lerp
            backgrounds[i].transform.position = Vector3.Lerp(backgrounds[i].transform.position, backgroundTargetPos, smoothing * Time.deltaTime);
        }


        //set the previous campos to the camera's pos at the end of the freame
        previousCamPos = cam.position;
	}
}
