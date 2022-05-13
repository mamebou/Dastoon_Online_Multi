using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Es.InkPainter;
using System;

public class CalScore : MonoBehaviour
{
    [SerializeField] private GameObject parentObject;

    private bool timerState;
    public static float countTime;
    private TextMeshPro scoreText;

    private GameObject main;
    StateManager stateManager;
    private String playerName;
    // Start is called before the first frame update
    void Start()
    {
        playerName = CanasController.getPlayerName();
        timerState = false;
        countTime = 0;
        scoreText= GetComponent<TextMeshPro>();
        main = GameObject.Find("Main");
        stateManager = main.GetComponent<StateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerState)
        {
            countTime += Time.deltaTime;
            scoreText.text = "Let's Cleaning!!\nTime:\n" + countTime.ToString("F2");
        }
        
    }

    public void CalArea()
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

        // �^�C�}�[�X�g�b�v
        timerState = false;

        if(PlayerPrefs.GetInt(playerName) == null){
            PlayerPrefs.SetInt(playerName, allArea);
            PlayerPrefs.Save();
        }else if(PlayerPrefs.GetInt(playerName) < allArea){
            PlayerPrefs.SetInt(playerName, allArea);
            PlayerPrefs.Save();
        }

        highScore = PlayerPrefs.GetInt(playerName);

        scoreText.text = playerName+ "'s Score:" + allArea + "\nTime:" + countTime.ToString("F1") + "\nDist:" + stateManager.distance.ToString("F2");

    }

    public void TimerStart()
    {
        // �^�C�}�[�X�^�[�g
        countTime = 0;
        timerState = true;
    }
}
