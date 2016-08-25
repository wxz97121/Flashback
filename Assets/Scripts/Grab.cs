using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Grab : MonoBehaviour {
    public bool canHold;
    public bool isHold=false;

    private Rigidbody rigid;
    private GameObject m_col;
    private Transform cam;

    public Vector3 idealPos =new Vector3(0.0f, 0.0f, 2.0f);
    public float grabDis = 2.5f;
	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
        m_col = GameObject.Find("Player");
        cam = GameObject.Find("FakeCamera").transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (cam.childCount == 0) 
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray,out hit,float.MaxValue,LayerMask.GetMask("Default")))
                {
                    if (hit.collider.gameObject.name == gameObject.name)
                    {
                        Vector3 dis = hit.point - cam.position;
                        if (dis.magnitude < grabDis)
                        {
                            transform.SetParent(cam);
                            transform.localPosition = idealPos;
                            transform.localRotation = Quaternion.Euler(Vector3.zero);

                            rigid.drag = float.MaxValue;
                            rigid.freezeRotation = true;
                            rigid.useGravity = false;
                            m_col.GetComponent<FlashBack>().holding = transform.gameObject;
                            isHold = true;
                        }
                    }
                }
            }
            else
            {
                rigid.velocity = m_col.GetComponent<Rigidbody>().velocity;
                if (cam.GetChild(0).name == gameObject.name) 
                {
                    EndGrab();
                }
            }
        }

        //位移过大则扔掉物体
        if (isHold)
        {
            Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue, LayerMask.GetMask("Default")))
            {
                if (hit.collider.gameObject.name != gameObject.name) {
                    EndGrab();
                }
            }
        }
	}

    //闪回状态下，发生碰撞扔掉物体
    void OnCollisionEnter(Collision col)
    {
        if (isHold&&(FlashBack.isflashing||col.gameObject.name=="Player"))
        {
            EndGrab();
        }
    }

    void EndGrab()
    {
        transform.SetParent(null);
        rigid.useGravity = true;
        canHold = false;
        m_col.GetComponent<FlashBack>().holding = null;
        isHold = false;
        rigid.drag = 0.0f;
        rigid.freezeRotation = false;
    }
}
