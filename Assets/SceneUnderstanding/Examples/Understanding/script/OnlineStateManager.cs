using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Microsoft.MixedReality.SceneUnderstanding.Samples.Unity;
using Microsoft.MixedReality.SceneUnderstanding;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class OnlineStateManager : MonoBehaviourPunCallbacks
{
    public string player1 = null;
    public string player2 = null;
    public bool player1Ready = false;
    public bool player2Ready = false;
    public bool isStart = true;
    public bool isStarted = false;
    public float CountDown = 5.0f;
    GameObject SceneUnderstanding;
    TextMeshPro CountDownText;
    PhotonView photonView;
    
    // Start is called before the first frame update
    void Start()
    {
        SceneUnderstanding = GameObject.Find("SceneUnderstandingManager");
        CountDownText = GameObject.Find("CountDwon").GetComponent<TextMeshPro>();
        photonView = GetComponent<PhotonView>();
        photonView.RPC(nameof(SetName), RpcTarget.All);
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
   private void SetName(){
       if(player1 == null){
           player2 = player2;
       }
       else{
           player1 = player1;
       }
   }
}
