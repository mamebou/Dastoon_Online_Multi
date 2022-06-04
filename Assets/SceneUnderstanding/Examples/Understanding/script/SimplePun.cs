using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;
using Microsoft.MixedReality.SceneUnderstanding.Samples.Unity;
using Microsoft.MixedReality.SceneUnderstanding;

public class SimplePun : MonoBehaviourPunCallbacks {

    public GameObject onlineStateManager;
    private String playerName;
    public GameObject stateManager;
    int playerNum;
    bool isStart = false;
    public bool isStarted = false;
    public Player enemy;
    public Player player;
    public float CountDown = 5f;
    TextMeshPro CountDownText;
    GameObject SceneUnderstanding;
    public int MaxPlayer = 2;
    public float stageTime = 100000f;
    private DustHander dustHander;
    public GameObject DustSensor;
    public GameObject scoreDisplay;
    private CalScore calscore;
    ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();


    // Use this for initialization
    void Start () {
        playerName = CanasController.getPlayerName();
        CountDownText = GameObject.Find("CountDwon").GetComponent<TextMeshPro>();
        SceneUnderstanding = GameObject.Find("SceneUnderstandingManager");
        calscore = scoreDisplay.GetComponent<CalScore>();
        dustHander = DustSensor.GetComponent<DustHander>();
    }

    void Update(){
        if(isStart){
          if(player.CustomProperties["isReady"] is true && enemy.CustomProperties["isReady"] is true ){
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

        if(isStarted){
            stageTime -= Time.deltaTime;
            if(stageTime <= 0){
                stageTime = 100000f;
                dustHander.ChangeStage();
            }
        }
    }

    void OnGUI()
    {
        //ログインの状態を画面上に出力
        GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
    }

    public void Connect(){
        PhotonNetwork.ConnectUsingSettings();
    }


    //ルームに入室前に呼び出される
    public override void OnConnectedToMaster() {
        bool isJoin = PhotonNetwork.JoinRandomRoom();
        if (isJoin)
            playerNum = 2;
    }

     // ランダムで参加できるルームが存在しないなら、新規でルームを作成する
    public override void OnJoinRandomFailed(short returnCode, string message) {
        // ルームの参加人数を2人に設定する
        playerNum = 1;
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(null, roomOptions);
    }


    //ルームに入室後に呼び出される
    public override void OnJoinedRoom(){
        player = PhotonNetwork.LocalPlayer;
        properties["playerName"] = "hello";
        properties["isReady"] = false;
        properties["isVsScore"] = false;
        player.SetCustomProperties(properties);
        Player[] players = PhotonNetwork.PlayerListOthers;
        if(players.Length != 0){
            enemy = players[0];
            isStart = true;
        }
    }

    //カスタムプロパティがセットされたとき
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        // Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties["playerName"]);
        // Debug.Log(changedProps);
        // Player[] players = PhotonNetwork.PlayerListOthers;
        // Player enemy = null;
        // if(players.Length != 0){
        //     enemy = players[0];
        //     isStart = true;
        // }

        Debug.Log(changedProps);
    }

    //ほかのプレイヤーが参加時
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Player[] players = PhotonNetwork.PlayerListOthers;
        enemy = players[0];
        isStart = true;
    }

    public void Ready(){
        properties["isReady"] = true;
        player.SetCustomProperties(properties);
    }
}
