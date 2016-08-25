using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().velocity=new Vector3(1,0,0);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
