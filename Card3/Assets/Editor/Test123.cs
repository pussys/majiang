using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Test123 : MonoBehaviour {

    public static Transform parnt;
    [MenuItem("MyMenu/Unity从入门到放弃")]
    static void DoSomeThint()
    {
        parnt = GameObject.Find("gameobject").transform;
        ShengChengTupian1();
    }
    [MenuItem("MyMenu/修改名字")]
    static void DonSth1()
    {
       Transform tra =  Selection.activeTransform;
        for (int i = 0; i < tra.childCount; i++)
        {
            tra.GetChild(i).name = i.ToString();
        }
    }
    public static Texture2D myTexture;
	// Use this for initialization
	void Start () {
        StartCoroutine("ShengChengTupian");
	}
	
	// Update is called once per frame
	IEnumerator ShengChengTupian () {
        for (int i = 0; i < myTexture.height; i++)
        {
            for (int j = 0; j < myTexture.width; j++)
            {
                Color c = myTexture.GetPixel(i, j);
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Quad);
                go.transform.position = new Vector3(i, j, 0);
                go.GetComponent<MeshRenderer>().material.color = c;
                go.transform.parent = parnt;
            }
            yield return null;
        }
    }

    static void ShengChengTupian1()
    {
        myTexture = Resources.Load<Texture2D>("tupian");
        for (int i = 0; i < myTexture.height; i++)
        {
            for (int j = 0; j < myTexture.width; j++)
            {
                Color c = myTexture.GetPixel(i, j);
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Quad);
                go.transform.position = new Vector3(i, j, 0);
                go.GetComponent<MeshRenderer>().material.color = c;
                go.transform.parent = parnt;
            }
        }
    }
}
