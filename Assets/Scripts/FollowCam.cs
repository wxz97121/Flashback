using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = Camera.main.transform.position;
        transform.rotation = Camera.main.transform.rotation;
	}
}
