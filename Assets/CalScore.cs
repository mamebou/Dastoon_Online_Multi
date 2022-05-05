using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Es.InkPainter;

public class CalScore : MonoBehaviour
{
    [SerializeField] private GameObject parentObject;

    private bool timerState;
    public static float countTime;
    private TextMeshPro scoreText;

    private GameObject main;
    StateManager stateManager;
    // Start is called before the first frame update
    void Start()
    {
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
        foreach (Transform childTransform in parentObject.transform)
        {
            if(childTransform.gameObject.name=="Floor"){
            GameObject grandchild = childTransform.transform.GetChild(0).gameObject;
            int area = grandchild.transform.GetComponent<InkCanvas>().CompareRenderTexture();
            allArea += area;
            }
        }

        // �^�C�}�[�X�g�b�v
        timerState = false;

        scoreText.text = "Your Score:\n" + allArea + "\nTime:" + countTime.ToString("F1") + "\nDist:" + stateManager.distance.ToString("F2");

    }

    public void TimerStart()
    {
        // �^�C�}�[�X�^�[�g
        countTime = 0;
        timerState = true;
    }
}
