using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Microsoft.MixedReality.SceneUnderstanding.Samples.Unity;
using Microsoft.MixedReality.SceneUnderstanding;
using TMPro;

public class OnlineStateManager : MonoBehaviour
{
    public String player1;
    public String player2;
    public bool player1Ready = false;
    public bool player2Ready = false;
    public bool isStart = true;
    public bool isStarted = false;
    public float CountDown = 5.0f;
    GameObject SceneUnderstanding;
    TextMeshPro CountDownText;
    
    // Start is called before the first frame update
    void Start()
    {
        SceneUnderstanding = GameObject.Find("SceneUnderstandingManager");
        CountDownText = GameObject.Find("CountDwon").GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
      if(isStart){
          if(player1Ready && player2Ready){
              if(!isStarted){
                  CountDown -= Time.deltaTime;
                  CountDownText.text = CountDown.ToString("F2");
              }
              
              if(CountDown <= 0f){
                  SceneUnderstanding.GetComponent<SceneUnderstandingManager>().DisplayScanPlanes = true;
                  isStart = false;
                  isStarted = true;
              }
          }
      }
    }
}
