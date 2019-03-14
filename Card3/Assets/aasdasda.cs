using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aasdasda : MonoBehaviour {

    float v, h;
    Rigidbody rig;
    public Vector3 v3;
    public Vector3 v5;
	// Use this for initialization
	void Start () {
        rig = transform.GetComponent<Rigidbody>();
        rig.AddForceAtPosition(v5,transform.position+ v3, ForceMode.Force);
        //Animation a = GetComponent<Animation>();
        //a.Stop();
       
    }
	
	// Update is called once per frame
	void Update () {
        //v = Input.GetAxis("Vertical");
        //h = Input.GetAxis("Horizontal");
        //rig.MovePosition(transform.forward*v*Time.deltaTime*5+transform.position);
        //rig.MoveRotation(Quaternion.Euler(0, h*Time.deltaTime*5, 0)*transform.rotation);
	}
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = new Color(0, 0, 1, 0.5f);
    //    Gizmos.DrawSphere(Vector3.zero, 5);
    //}
}
