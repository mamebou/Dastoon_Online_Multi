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
    bool isStart = true;
    bool isStarted = false;
    Player enemy = null;
    Player player = null;
    public float CountDown = 5f;
    TextMeshPro CountDownText;
    GameObject SceneUnderstanding;
    public int MaxPlayer = 2;


    // Use this for initialization
    void Start () {
        playerName = CanasController.getPlayerName();
        CountDownText = GameObject.Find("CountDwon").GetComponent<TextMeshPro>();
        SceneUnderstanding = GameObject.Find("SceneUnderstandingManager");
    }

    void Update(){
        if(isStart){
          if((bool)player.CustomProperties["isReady"] && (bool)enemy.CustomProperties["isReady"]){
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
        Player[] players = PhotonNetwork.PlayerListOthers;
        if(players.Length != 0){
            enemy = players[0];
            isStart = true;
        }
        player = PhotonNetwork.LocalPlayer;
        ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();
        properties["playerName"] = "hello";
        properties["isReady"] = false;
        PhotonNetwork.LocalPlayer.SetCustomProperties(properties);
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
    }

    //ほかのプレイヤーが参加時
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayer){
            Player[] players = PhotonNetwork.PlayerListOthers;
            enemy = players[0];
            isStart = true;
        }
    }

    public void Ready(){
        player.CustomProperties["isReady"] = true;
    }
}
