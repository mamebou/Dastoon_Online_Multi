using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Microsoft.MixedReality.SceneUnderstanding.Samples.Unity;
using Microsoft.MixedReality.SceneUnderstanding;
using TMPro;
using Photon.Pun;

public class OnlineStateManager : MonoBehaviourPunCallbacks
{
    public String player1 = null;
    public String player2 = null;
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
        if(player1 == null){
            photonView.RPC(nameof(SetName), RpcTarget.All, player2);
        }
        else{
            photonView.RPC(nameof(SetName), RpcTarget.All, player1);
        }
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

   [PunRPC]
   private void SetName(String PlayerName){
       if(player1 == null){
           player2 = PlayerName;
       }
       else{
           player1 = PlayerName;
       }
   }
}
