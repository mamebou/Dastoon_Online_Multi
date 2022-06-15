using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Es.InkPainter;
using System;
using Microsoft.MixedReality.SceneUnderstanding.Samples.Unity;
using Microsoft.MixedReality.SceneUnderstanding;

public class CalScore : MonoBehaviour
{
    [SerializeField] private GameObject parentObject;

    private bool timerState;
    public static float countTime;
    private TextMeshPro scoreText;

    private GameObject main;
    StateManager stateManager;
    private String playerName;
    TextMeshPro gameTimeText;
    public float gameTime = 10f;
    GameObject SceneUnderstanding;
    public GameObject connectManager;
    private SimplePun simplePun = new SimplePun();
    private int score = 0;
    public GameObject sceneRoot;
    private System.Random rand = new System.Random();
    [SerializeField]
	private GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        playerName = CanasController.getPlayerName();
        simplePun = connectManager.GetComponent<SimplePun>();
        gameTimeText = GameObject.Find("CountDwon").GetComponent<TextMeshPro>();
        timerState = false;
        countTime = 0;
        scoreText= this.GetComponent<TextMeshPro>();
        main = GameObject.Find("Main");
        stateManager = main.GetComponent<StateManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public int CalArea(Color myColor)
    {
        //�h��ꂽ�͈͂̌v�Z�B���Ȃ�d����(�v�Z���̓t���[�Y����)
        int allArea = 0;
        int highScore = 0;
        foreach (Transform childTransform in parentObject.transform)
        {
            if(childTransform.gameObject.name=="Floor"){
            GameObject grandchild = childTransform.transform.GetChild(0).gameObject;
            int area = grandchild.transform.GetComponent<InkCanvas>().CompareRenderTexture(myColor);
            allArea += area;
            }
        }

        return allArea;
    }

    public void TimerStart()
    {
        // �^�C�}�[�X�^�[�g
        countTime = 0;
        timerState = true;
    }

    //結果表示用

    public void createEnemy(){
        Transform[] allFloor = GetAllChild(sceneRoot);
        int numFloor = allFloor.Length;
        int enemyPlace = rand.Next(0, numFloor - 1);
        Transform pos = allFloor[enemyPlace];
        enemy = Instantiate(enemy, Camera.main.transform.position + Camera.main.transform.forward * 1.5f, new Quaternion(0f, 0f, 0f, 0f));
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
