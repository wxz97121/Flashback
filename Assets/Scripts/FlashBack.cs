using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class FlashBack : MonoBehaviour {

    public GameObject markPrefab;
    public GameObject markInGame;
    public GameObject holding;
    public bool isflashing = false;
    public float speed=0.1f;

    private Rigidbody rigid;
    private CharacterController controller;
    private Vector3 moveVector;
    private Vector3 oriVelocity;
	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
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
                markInGame=Instantiate(markPrefab, transform.position, transform.rotation) as GameObject;
            }
            else if (Input.GetMouseButtonDown(1)&&markInGame!=null)
            {
                isflashing = true;
                GetComponent<FirstPersonController>().enabled=false;
                gameObject.layer = LayerMask.NameToLayer("GraySpace");
                moveVector = markInGame.transform.position-transform.position;
                oriVelocity = rigid.velocity;
                rigid.velocity = Vector3.zero;
                if (holding != null)
                {
                    holding.GetComponent<Rigidbody>().isKinematic = false;
                }
            }
        }
        else
        {
            controller.Move(moveVector * speed);
            if (transform.position == markInGame.transform.position)
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

    void EndFlash()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        isflashing = false;
        GetComponent<FirstPersonController>().enabled = true;
        Destroy(markInGame);
        rigid.velocity = oriVelocity;
        if (holding != null) {
            holding.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
