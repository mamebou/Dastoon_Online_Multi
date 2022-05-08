using Microsoft.MixedReality.SceneUnderstanding.Samples.Unity;
using Microsoft.MixedReality.SceneUnderstanding;
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
    public GameObject sceneRoot;
    private System.Random rand = new System.Random();

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
            //Debug.Log(distance);
            prevBrushPosition = Brush.transform.position;
            currentTime = 0f;
        }

        //敵の生成
        if(isCreate){
            if(createTime > 5f && sceneRoot.transform.childCount != 0){
                createEnemy();
                isCreate = false;
            }  
        }
        //ここまで
            
    }

    public void scanStart()
    {
        SceneUnderstanding.GetComponent<SceneUnderstandingManager>().DisplayScanPlanes = true;

    }

    public void createEnemy(){
        Transform[] allFloor = GetAllChild(sceneRoot);
        int numFloor = allFloor.Length;
        int enemyPlace = rand.Next(0, numFloor - 1);
        Transform pos = allFloor[enemyPlace];
        enemy = Instantiate(enemy, new Vector3(pos.position.x, pos.position.y + 10f, pos.position.z), new Quaternion(0f, 0f, 0f, 0f));
    }

    private Transform[] GetAllChild(GameObject rootObject){
        int pos = 0;
        Transform[] sceneObjects = new Transform[rootObject.transform.childCount];
 
        for (int i = 0; i < rootObject.transform.childCount; i++)
        {
            if(rootObject.transform.GetChild(i).name == "Floor"){
                sceneObjects[pos] = rootObject.transform.GetChild(i);
                pos++;
            }
        }
        Array.Resize(ref sceneObjects, pos);

        return sceneObjects;
    }
}
