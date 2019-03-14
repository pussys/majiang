using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baozha : MonoBehaviour {

    public Rigidbody[] tra;
	// Use this for initialization
	void Start () {
        tra = new Rigidbody[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            tra[i] = transform.GetChild(i).GetComponent<Rigidbody>();
        }
        for (int i = 0; i < tra.Length; i++)
        {
            tra[i].AddExplosionForce(1000,Vector3.zero,50);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
