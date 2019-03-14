using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    //游戏对象，这里是线段对象
    private GameObject LineRenderGameObject;

    //线段渲染器
    private LineRenderer lineRenderer;

    //设置线段的个数，标示一个曲线由几条线段组成
    private int lineLength = 4;

    //分别记录4个点，通过这4个三维世界中的点去连接一条线段
    private Vector3 v0 = new Vector3(1.0f, 0.0f, 0.0f);
    private Vector3 v1 = new Vector3(0.0f, 1.0f, 0.0f);
    private Vector3 v2 = new Vector3(0.0f, 0.0f, 1.0f);
    private Vector3 v3 = new Vector3(1.0f, 0.0f, 0.0f);

    void Start()
    {

        //通过之前创建的对象的名称，就可以在其它类中得到这个对象，
        //这里在main.cs中拿到line的对象
        LineRenderGameObject = GameObject.Find("line");

        //通过游戏对象，GetComponent方法 传入LineRenderer
        //就是之前给line游戏对象添加的渲染器属性
        //有了这个对象才可以为游戏世界渲染线段
        lineRenderer = (LineRenderer)LineRenderGameObject.GetComponent("LineRenderer");

        //设置线段长度，这个数值须要和绘制线3D点的数量想等
        //否则会抛异常～～
        lineRenderer.SetVertexCount(lineLength);

    }

    void Update()
    {

        //在游戏更新中去设置点
        //根据点将这个曲线链接起来
        //第一个参数为 点的ID 
        //第二个 参数为点的3D坐标
        //ID 一样的话就标明是一条线段
        //所以盆友们须要注意一下！

        lineRenderer.SetPosition(0, v0);
        lineRenderer.SetPosition(1, v1);
        lineRenderer.SetPosition(2, v2);
        lineRenderer.SetPosition(3, v3);

        //通过object对象名 face 得到网格渲染器对象
        MeshFilter meshFilter = (MeshFilter)GameObject.Find("face").GetComponent(typeof(MeshFilter));

        //通过渲染器对象得到网格对象
        Mesh mesh = meshFilter.mesh;

        //API中写的不是提清楚，我详细的在说一遍

        //设置顶点，这个属性非常重要
        //三个点确定一个面，所以Vector3数组的数量一定是3个倍数
        //遵循顺时针三点确定一面
        //这里的数量为6 也就是我创建了2个三角面
        //依次填写3D坐标点
        mesh.vertices = new Vector3[] { new Vector3(5, 0, 0), new Vector3(0, 5, 0), new Vector3(0, 0, 5), new Vector3(-5, 0, 0), new Vector3(0, -5, 0), new Vector3(0, 0, -5) };

        //设置贴图点，因为面确定出来以后就是就是2D 
        //所以贴纸贴图数量为Vector2 
        //第一个三角形设置5个贴图
        //第二个三角形设置一个贴图
        //数值数量依然要和顶点的数量一样
        mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 5), new Vector2(5, 5), new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };

        //设置三角形索引，这个索引是根据上面顶点坐标数组的索引
        //对应着定点数组Vector3中的每一项
        //最后将两个三角形绘制在平面中
        //数值数量依然要和顶点的数量一样
        mesh.triangles = new int[] { 0, 1, 2, 3, 4, 5 };
    }
}
