using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
//[RequireComponent(typeof(Rigidbody))]
public class Grab : MonoBehaviour {
    public bool canHold;
    public bool isHold=false;

    private Rigidbody rigid;
    private GameObject m_col;
    private Transform cam;

    public Vector3 idealPos =new Vector3(0.0f, -0.7f, 1.56f);
    public Vector3 idealRot = new Vector3(0.0f, 0.0f, 0.0f);
	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
        m_col = GameObject.Find("Player");
        cam = m_col.transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E)&&canHold)
        {
            if (cam.childCount == 0)
            {
                transform.SetParent(cam);
                transform.localPosition = idealPos;
                transform.localRotation = Quaternion.Euler(idealRot);
                rigid.isKinematic = true;
                rigid.useGravity = false;
                m_col.GetComponent<FlashBack>().holding = transform.gameObject;
                isHold = true;
            }
            else
            {
                EndGrab();
            }
        }
	}

    void LateUpdate()
    {
        //if (isHold)
        //{
        //    var ori = transform.localPosition;
        //    transform.localPosition = idealPos;
        //    var tarPos = transform.position;
        //    transform.localPosition = ori;
        //    rigid.MovePosition(tarPos);
        //    rigid.velocity = Vector3.zero;
        //}
    }

    //确认物体可以被拿起
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            canHold = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Player"&&cam.childCount==0)
        {
            canHold = false;
        }
    }

    //仅在闪回状态下触发(物体不被拿起时，无parent/被拿起但不闪回时，iskeblabla为true不计算碰撞)
    void OnCollisionEnter(Collision col)
    {
        rigid.velocity = new Vector3(0.0f, rigid.velocity.y, 0.0f);
        if (isHold && col.gameObject.name != "Player")
        {
            EndGrab();
        }
    }

    void EndGrab()
    {
        transform.SetParent(null);
        rigid.isKinematic = false;
        rigid.useGravity = true;
        canHold = false;
        m_col.GetComponent<FlashBack>().holding = null;
        isHold = false;
    }
}
