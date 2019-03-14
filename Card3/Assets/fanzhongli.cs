using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fanzhongli : MonoBehaviour {

    //Rigidbody rig;
	// Use this for initialization
	void Start () {
        Debug.Log(Vector3.Dot(new Vector3(0, 0, 1), new Vector3(0, 1, 1)));
    }
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    Physics.gravity = new Vector3(0, 9.81f, 0);
        //}

        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    Physics.gravity = new Vector3(0, -9.81f, 0);
        //}
	}
}
