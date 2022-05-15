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
    private string playerName;
    public int playerNum;

    // Start is called before the first frame update
    void Start()
    {
        playerName = CanasController.getPlayerName();
        SceneUnderstanding = GameObject.Find("SceneUnderstandingManager");
        CountDownText = GameObject.Find("CountDwon").GetComponent<TextMeshPro>();
        photonView = GetComponent<PhotonView>();
        photonView.RPC(nameof(SetName), RpcTarget.All);
    }

    // Update is called once per frame
    void Update()
    {
    photonView.RPC(nameof(SetName), RpcTarget.All);
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
       if(playerNum == 1){
           player1 = "playerName1";
       }
       else if(playerNum == 2){
           player2 = "playerName2";
       }
   }
}
