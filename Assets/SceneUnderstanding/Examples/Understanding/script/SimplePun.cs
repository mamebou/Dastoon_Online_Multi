using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;
using Microsoft.MixedReality.SceneUnderstanding.Samples.Unity;
using Microsoft.MixedReality.SceneUnderstanding;

public class SimplePun : MonoBehaviourPunCallbacks {

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
    public float stageTime = 5f;
    private DustHander dustHander;
    public GameObject DustSensor;
    public GameObject scoreDisplay;
    private CalScore calscore;
    private int totalScore = 0;
    private int myTotalScore = 0;
    private TextMeshPro scoreText;
    public int stageNum = 1;
    public GameObject gauge;
    private ScoreGauge socreGauge;
    private float compareTime = 2.0f;
    private bool isMyCompare = false;
    private bool isEnemyCompare = false;
    
    ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();


    // Use this for initialization
    void Start () {
        playerName = CanasController.getPlayerName();
        CountDownText = GameObject.Find("CountDwon").GetComponent<TextMeshPro>();
        SceneUnderstanding = GameObject.Find("SceneUnderstandingManager");
        scoreText= scoreDisplay.GetComponent<TextMeshPro>();
        calscore = scoreDisplay.GetComponent<CalScore>();
        dustHander = DustSensor.GetComponent<DustHander>();
        socreGauge = gauge.GetComponent<ScoreGauge>();
    }

    void Update(){

        //対戦開始判定
        if(isStart){
          if(player.CustomProperties["isReady"] is true && enemy.CustomProperties["isReady"] is true ){
              if(!isStarted){
                  CountDown -= Time.deltaTime;
                  CountDownText.text = CountDown.ToString();
                }
              
              if(CountDown <= 0f){
                  SceneUnderstanding.GetComponent<SceneUnderstandingManager>().DisplayScanPlanes = true;
                  isStart = false;
                  isStarted = true;
                  scoreText.text = "Stage" + stageNum.ToString("F2");
                }
            }
        }

        //対戦判定用                    
        if(isStarted){
            stageTime -= Time.deltaTime;
            CountDownText.text = stageTime.ToString("F2");
            if(stageTime <= 0){
                Color myColor = dustHander.SetColor();
                properties["isVsScore"] = true;
                properties["StageScore"] = calscore.CalArea(myColor);
                player.SetCustomProperties(properties);
                stageTime = 10f;
                if(stageNum == 5){
                    ResultDisplay();
                }
                else{
                    stageNum += 1;
                    dustHander.ChangeStage(stageNum);
                    scoreText.text = "Stage" + stageNum.ToString("F2");
                }
            }

            if(isMyCompare && isEnemyCompare){
                properties["isVsScore"] = false;
                isMyCompare = false;
                isEnemyCompare = false;
                player.SetCustomProperties(properties);
                totalScore += GetMyScore() + GetEnemyScore();
                myTotalScore += GetMyScore();
                if(totalScore == 0){
                    totalScore = 1;
                }
                float spaceOccupancy = (float)myTotalScore/(float)totalScore;
                socreGauge.UpdateGuage(spaceOccupancy);
                calscore.createEnemy();
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
        if(changedProps.ContainsKey("isVsScore")){
           if(player.CustomProperties["isVsScore"] is true){
               isMyCompare = true;
           }
           if(enemy != null){
                if(enemy.CustomProperties["isVsScore"] is true){
                    isEnemyCompare = true;
                }   
           }
        }
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
        Player[] players = PhotonNetwork.PlayerListOthers;
        enemy = players[0];
        isStart = true;
    }

    public void Ready(){
        properties["isReady"] = true;
        player.SetCustomProperties(properties);
    }

    public int GetMyScore(){
        return (player.CustomProperties["StageScore"] is int score) ? score : 0;
    }

    public int GetEnemyScore(){
        return (enemy.CustomProperties["StageScore"] is int score) ? score : 0;
    }

    public void ResultDisplay(){
        isStarted = false;
        int myPoint = (myTotalScore / totalScore) * 100;
        Debug.Log("result");

        if(myPoint >= 50){
            scoreText.text = "You are winner !";
        }
        else{
            scoreText.text = "You are loser";
        }

    }
}
