using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

    Ray ra;
    RaycastHit hit;
    Vector3 v3;
    bool ismove=false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ra = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ra,out hit,100,LayerMask.GetMask("plane")))
            {
                v3 = hit.point;
                transform.LookAt(v3);
                ismove = true;
            }
        }

        if (ismove)
        {
            if (Vector3.Distance(v3, transform.position) < 0.1f)
            {
                ismove = false;
            }
            transform.Translate(Vector3.forward*Time.deltaTime*5, Space.Self);

        }
    }
}
