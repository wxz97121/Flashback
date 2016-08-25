using UnityEngine;
using System.Collections;

public class Squat : MonoBehaviour {

   public float scaleY=0.5f;
   public KeyCode squat;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKey(squat))
        {
            if (transform.localScale.y > scaleY)
            {
                float temp = transform.localScale.y - 0.01f < scaleY ? scaleY : transform.localScale.y - 0.1f;
                transform.localScale = new Vector3(1.0f, temp, 1.0f);
            }
        }
        else
        {
            if (transform.localScale.y < 1.0f)
            {
                float temp = transform.localScale.y + 0.01f > 1.0f ? 1.0f : transform.localScale.y + 0.1f;
                transform.localScale = new Vector3(1.0f, temp, 1.0f);
            }
        }
	}
}
