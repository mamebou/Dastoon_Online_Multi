using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using System;

public class SimplePun : MonoBehaviourPunCallbacks {

    public GameObject onlineStateManager;
    private String playerName;
    public GameObject stateManager;

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
        // "room"という名前のルームに参加する（ルームが無ければ作成してから参加する）
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
    }

    //ルームに入室後に呼び出される
    public override void OnJoinedRoom(){
        GameObject go = GameObject.Find("OnlineStateManager(Clone)");
        if(GameObject.Find("OnlineStateManager(Clone)") == null){
            onlineStateManager = PhotonNetwork.Instantiate("onlineStateManager", Vector3.zero, Quaternion.identity, 0);
            onlineStateManager.GetComponent<OnlineStateManager>().player1 = playerName;
            stateManager.GetComponent<StateManager>().playerNum = 1;
        }
        else{
            onlineStateManager = GameObject.Find("OnlineStateManager");
            onlineStateManager.GetComponent<OnlineStateManager>().player2 = playerName;
            stateManager.GetComponent<StateManager>().playerNum = 2;
        }
    }
}
