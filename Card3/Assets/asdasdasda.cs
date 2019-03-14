using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class asdasdasda : MonoBehaviour {

    public GameObject ga;
    Rigidbody rig;
    Button red;
    Button suiji;
    Button ban;
    // Use this for initialization
    void Start () {
        //Debug.Log(Random.ColorHSV());//随机颜色
        red = GameObject.Find("red").transform.GetComponent<Button>();
        suiji = GameObject.Find("suij").transform.GetComponent<Button>();
        ban = GameObject.Find("ban").transform.GetComponent<Button>();

    }

    // Update is called once per frame
    void Update () {
        ////Debug.Log(Random.onUnitSphere);
        ////Instantiate(ga, Random.onUnitSphere,transform.rotation);
        //float abc = Random.Range(0.1f,1f);
        //GameObject gan = Instantiate(ga, Random.onUnitSphere*10 + transform.position, Quaternion.identity);
        //gan.transform.localScale=new Vector3(abc,abc,abc);
        //transform.localRotation= Random.rotation;
        //transform.GetComponent<Renderer>().material.color = Random.ColorHSV();
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                
            }
            else
            {
                transform.GetComponent<Renderer>().material.color = new Color(1,1,1,1);
            }
        }
    }
    public void Red()
    {
        transform.GetComponent<Renderer>().material.color = Color.red;
    }
    public void Suiji() {
        transform.GetComponent<Renderer>().material.color = Random.ColorHSV();
    }
    public void bantouming()
    {
        Color co = transform.GetComponent<Renderer>().material.color;

        transform.GetComponent<Renderer>().material.color = new Color(co.r,co.g,co.b,0.5f);
    }
}
