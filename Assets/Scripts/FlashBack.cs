using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class FlashBack : MonoBehaviour {

    public GameObject markPrefab;
    public GameObject markInGame;
    public GameObject holding;
    public static bool isflashing = false;
    public float speed=30.0f;
    private float realSpeed;
    public float tolerance = 0.01f;

    private Rigidbody rigid;
    //private CharacterController controller;
    private Vector3 oriVelocity;
    private Vector3 velocity = Vector3.zero;
    private Vector3 oriRot;

    private float curSpeed;
    private float oriSpeed;

	// Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!isflashing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (markInGame != null)
                {
                    Destroy(markInGame);
                }
                markInGame=Instantiate(markPrefab, transform.position, Camera.main.transform.rotation) as GameObject;
            }
            else if (Input.GetMouseButtonDown(1)&&markInGame!=null)
            {
                isflashing = true;
                oriSpeed = rigid.velocity.magnitude;
                rigid.velocity = Vector3.zero;
                GetComponent<RigidbodyFirstPersonController>().enabled=false;
                GetComponent<CameraControl>().enabled = true;
                rigid.useGravity = false;
                realSpeed = (markInGame.transform.position - transform.position).magnitude / speed;
                Debug.Log(realSpeed);
                gameObject.layer = LayerMask.NameToLayer("GraySpace");
                if (holding != null)
                {
                    holding.GetComponent<Rigidbody>().isKinematic = false;
                }
            }
        }
        else
        {
            rigid.MovePosition(Vector3.MoveTowards(transform.position, markInGame.transform.position,realSpeed));
            if ((transform.position -markInGame.transform.position).magnitude<tolerance)
            {
                EndFlash();
            }
        }
	}

    void OnTriggerEnter()
    {
        if (gameObject.layer == LayerMask.NameToLayer("GraySpace"))
        {
            EndFlash();
        }

    }

    void OnCollisionEnter() {
        rigid.velocity = Vector3.zero;
    }

    void EndFlash()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        isflashing = false;
        GetComponent<RigidbodyFirstPersonController>().enabled = true;
        GetComponent<CameraControl>().enabled = false;
        rigid.velocity =markInGame.transform.forward*oriSpeed;
        Destroy(markInGame);
        rigid.useGravity=true;
        if (holding != null) {
            holding.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
