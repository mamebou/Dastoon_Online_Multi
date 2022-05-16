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
    public float gameTime = 1000000f;
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
        if(simplePun.isStarted == true){
            scoreText.text = "Let's Cleaning!!\nTime:\n";
            gameTime -= Time.deltaTime;
            countTime += Time.deltaTime;
            gameTimeText.text = gameTime.ToString("F2");

            if(countTime >= 10){
                createEnemy();
                countTime = 0f;
            }
        }
        
        if(gameTime <= 0){
            FinishGame();
            Debug.Log("in");
        }
    }

    public int CalArea()
    {
        //�h��ꂽ�͈͂̌v�Z�B���Ȃ�d����(�v�Z���̓t���[�Y����)
        int allArea = 0;
        int highScore = 0;
        foreach (Transform childTransform in parentObject.transform)
        {
            if(childTransform.gameObject.name=="Floor"){
            GameObject grandchild = childTransform.transform.GetChild(0).gameObject;
            int area = grandchild.transform.GetComponent<InkCanvas>().CompareRenderTexture(new Color(1.0f, 0.5f, 0.0f, 1.0f));
            allArea += area;
            }
        }

        return allArea;
    }

    public void FinishGame(){
        simplePun.isStarted = false;
        score = CalArea();
        scoreText.text = playerName+ "'s Score:" + score + "\nTime:" + countTime.ToString("F1") + "\nDist:" + stateManager.distance.ToString("F2");
    }

    public void TimerStart()
    {
        // �^�C�}�[�X�^�[�g
        countTime = 0;
        timerState = true;
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
