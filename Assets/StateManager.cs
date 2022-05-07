using Microsoft.MixedReality.SceneUnderstanding.Samples.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateManager : MonoBehaviour
{ 

    GameObject SceneUnderstanding;

    public float span = 1f;
    private float currentTime = 0f;
    private float createTime = 0f;

    public float distance = 0f;
    private GameObject Brush;
    private Vector3 prevBrushPosition;
    private String playerName;
    private bool isCreate = true;

    [SerializeField]
	private GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        SceneUnderstanding = GameObject.Find("SceneUnderstandingManager");
        Brush = GameObject.Find("Brush");
        prevBrushPosition = Brush.transform.position;
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        createTime += Time.deltaTime;

        if (currentTime > span)//1秒おきに呼ばれる
        {
            distance += Vector3.Distance(Brush.transform.position, prevBrushPosition); ;
            Debug.Log(distance);
            prevBrushPosition = Brush.transform.position;
            currentTime = 0f;
        }

        //敵の生成
        if(isCreate){
            if(createTime > 5f){
                enemy = Instantiate(enemy);
                isCreate = false;
                createTime = 0f;
            }  
        }
        //ここまで
            
    }

    public void scanStart()
    {
        SceneUnderstanding.GetComponent<SceneUnderstandingManager>().DisplayScanPlanes = true;

    }
}
