using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using System;

public class SimplePun : MonoBehaviourPunCallbacks {

    public GameObject onlineStateManager;
    private String playerName;
    public GameObject stateManager;
    int playerNum;

    // Use this for initialization
    void Start () {
        playerName = CanasController.getPlayerName();

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
        GameObject go = GameObject.Find("OnlineStateManager(Clone)");
        if(playerNum == 1){
            onlineStateManager = PhotonNetwork.Instantiate("onlineStateManager", Vector3.zero, Quaternion.identity, 0);
            onlineStateManager.GetComponent<OnlineStateManager>().player1 = "1";
            stateManager.GetComponent<StateManager>().playerNum = 1;
        }
        else if(playerNum == 2){
            onlineStateManager = PhotonNetwork.Instantiate("onlineStateManager", Vector3.zero, Quaternion.identity, 0);
            onlineStateManager.GetComponent<OnlineStateManager>().player2 = "2";
            stateManager.GetComponent<StateManager>().playerNum = 2;
        }
    }
}
