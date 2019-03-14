using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ceshi2 : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler
{
    public GameObject go;
    Ray ra;
    RaycastHit hit;
    GameObject ga;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnDrag(PointerEventData eventData)
    {
        if (go != null&&eventData.button.Equals(PointerEventData.InputButton.Left))
        {
            ra = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ra.origin, ra.direction, Color.red);
            if (Physics.Raycast(ra, out hit, 100, LayerMask.GetMask("plane")))
            {
                Debug.Log("碰到了");
                go.transform.position = hit.point;
            }
            else
            {
                go.transform.position = ra.GetPoint(20);
                Debug.Log("没碰到");
            }
            Debug.Log("找到了go");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button.Equals(PointerEventData.InputButton.Left))
        {
            go = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube));
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (go != null && eventData.button.Equals(PointerEventData.InputButton.Left))
        {
            if (Physics.Raycast(ra, out hit, 100, LayerMask.GetMask("plane")))
            {
                Debug.Log("碰到了");
                go.transform.position = hit.point;
                go = null;
            }
            else
            {
                Destroy(go);
                Debug.Log("没碰到");
            }
            Debug.Log("找到了go");
        }
    }
}
