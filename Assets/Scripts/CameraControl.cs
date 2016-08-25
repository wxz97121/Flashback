using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public float speed = 0.05f;
	public float MouseXSpeed=1.0f;
	public float MouseYSpeed=1.0f;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		Cursor.lockState=CursorLockMode.Locked;
	}

	// Update is called once per frame
	void Update () 
	{
		//var origin = transform.position;

		//change the view
		var x=Input.GetAxis("Mouse X")*MouseXSpeed;
		transform.rotation =transform.rotation*Quaternion.Euler(0,x,0);
		var y = Input.GetAxis ("Mouse Y")*MouseYSpeed;

		Camera cam = GetComponentInChildren<Camera>();
		cam.transform.localRotation =cam.transform.localRotation*Quaternion.Euler(-y,0,0);

		//var cha = GetComponent<CharacterController>();

		//change the pos
		//if(Input.GetKey(KeyCode.W)){cha.Move(cam.transform.rotation*Vector3.forward*speed);}
		//if(Input.GetKey(KeyCode.S)){cha.Move(cam.transform.rotation*Vector3.back*speed);}
		//if(Input.GetKey(KeyCode.A)){cha.Move(cam.transform.rotation*new Vector3(-1.0f,0.0f,0.0f)*speed);}
		//if(Input.GetKey(KeyCode.D)){cha.Move(cam.transform.rotation*new Vector3(1.0f,0.0f,0.0f)*speed);}
		//transform.position=new Vector3(transform.position.x,origin.y,transform.position.z);

		//debug use
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			Cursor.visible = true;
			Cursor.lockState=CursorLockMode.Confined;
		}

		//test Hit
	}
}
