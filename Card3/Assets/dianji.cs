using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class dianji : MonoBehaviour
{

    public GameObject one;
    public GameObject two;
    bool iskaishi = false;
    bool iszanting = false;
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        Debug.Log("Time.deltaTime:" + Time.deltaTime);
        Debug.Log("Time.unscaledDeltaTime:" + Time.unscaledDeltaTime);
            one.transform.position = Vector3.MoveTowards(one.transform.position, one.transform.position+one.transform.forward, Time.deltaTime);
            two.transform.position =Vector3.MoveTowards(two.transform.position, two.transform.position+two.transform.forward, Time.unscaledDeltaTime);
        
        if (iszanting)
        {
            Time.timeScale = 0;
            Debug.Log("Time.time:" + Time.time);
            Debug.Log("Time.unscaledTime:" + Time.unscaledTime);
        }
	}
    public void kaishi()
    {
        iszanting = false;
        iskaishi = true;
    }
    public void zanting()
    {
        iskaishi = false;
        iszanting = true;
    }
    //public void anxia()
    //{
    //    Debug.Log("执行了按下");
    //    go = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube));
        
        
    //}
    //public void tuozhuai()
    //{
    //    if (go!=null)
    //    {
    //        ra = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        Debug.DrawRay(ra.origin, ra.direction, Color.red);
    //        if (Physics.Raycast(ra, out hit, 100, LayerMask.GetMask("plane")))
    //        {
    //            Debug.Log("碰到了");
    //            go.transform.position = hit.point;
    //        }
    //        else
    //        {
    //            go.transform.position = ra.GetPoint(20);
    //            Debug.Log("没碰到");
    //        }
    //        Debug.Log("找到了go");
    //    }
    //}
    //public void taiqi()
    //{
    //    if (go != null)
    //    {
    //        if (Physics.Raycast(ra, out hit,100, LayerMask.GetMask("plane")))
    //        {
    //            Debug.Log("碰到了");
    //            go.transform.position = hit.point;
    //            go = null;
    //        }
    //        else
    //        {
    //            Destroy(go);
    //            Debug.Log("没碰到");
    //        }
    //        Debug.Log("找到了go");
    //    }
    //}
}
