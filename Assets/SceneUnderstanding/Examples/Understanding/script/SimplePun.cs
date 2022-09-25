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
    private int enemyNum = 0;
    private bool isDestroyEnemy = false;
    public GameObject NormalEnemy;
    public GameObject RareEnemy;
    public GameObject[] enemys = new GameObject[2];
    private float[] range = new float[2];
    Vector3 position = new Vector3(0,0,0);
    System.Random rand = new System.Random();
    private int additionalPoint;
    private int StageTotalScore;
    private int MyStageTotalScore;
    public GameObject demoPlayer;



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
                  demoPlayer.GetComponent<Rigidbody>().useGravity = true;
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
                    stageNum += 1;
                }
                else{
                    stageNum += 1;
                    dustHander.ChangeStage(stageNum);
                    enemyNum = 0;
                    scoreText.text = "Stage" + stageNum.ToString("F2");
                }
            }

            if(isMyCompare && isEnemyCompare){//スコア比較用
                //ステータス更新
                properties["isVsScore"] = false;
                isMyCompare = false;
                isEnemyCompare = false;
                player.SetCustomProperties(properties);
                //スコア割合計算
                MyStageTotalScore = GetMyScore() + additionalPoint;
                additionalPoint = 0;
                StageTotalScore = GetMyScore() + GetEnemyScore();
                totalScore += StageTotalScore;
                myTotalScore += MyStageTotalScore;
                //全て0の場合は５０：５０となるよう設定
                if(StageTotalScore == 0){
                    StageTotalScore = 2;
                }
                if(MyStageTotalScore == 0){
                    MyStageTotalScore = 1;
                }

                //前のステージから残っているエネミーを削除
                DestroyEnemy(enemys);

                float spaceOccupancy = (float)myTotalScore/(float)totalScore;
                range = GetRange(0.5f - spaceOccupancy);
                enemyNum = GetEnemyNum(0.5f - spaceOccupancy);

                socreGauge.UpdateGuage(spaceOccupancy);
                Array.Resize(ref enemys, enemyNum);
                for(int i = 0; i < enemyNum; i++ ){
                    int num = rand.Next(1,20);
                    position = GetEnemyPosition(range[0]+((float)i/10), i+1);
                    enemys[i] = CreateEnemy(position, isNormalEnemy(num));
                }

                if(stageNum == 6){
                    ResultDisplay();
                }
            }
        }
    }

    // エネミー生成
    public GameObject CreateEnemy(Vector3 position, bool isNomal){
        if (isNomal){
            return Instantiate(NormalEnemy, position, Quaternion.identity);
        }
        else{
            return Instantiate(RareEnemy, position, Quaternion.identity);
        }
        
    }

    public void DestroyEnemy(GameObject[] enemys){
        if(enemys.Length != 0){
            for(int i=0; i<enemys.Length; i++){
                if(enemys[i] != null){
                    Destroy(enemys[i]);
                }
            }
        }
    }

    public bool isNormalEnemy(int num){
        if(num == 1 || num == 10){
            return false;
        }
        else{
            return true;
        }
    }

    //エネミー数決定
    public int GetEnemyNum(float diffScore){
        Debug.Log(diffScore);
        if(-0.5f <= diffScore && diffScore < -0.4f){
            return 9;
        }
        else if(-0.4f <= diffScore && diffScore < -0.3f){
            return 8;
        }
        else if(-0.3f <= diffScore && diffScore < -0.2f){
            return 7;
        }
        else if(-0.2f <= diffScore && diffScore < -0.1f){
            return 6;
        }
        else if(-0.1f <= diffScore && diffScore < 0.0f){
            return 5;
        }
        else if(0.0f <= diffScore && diffScore < 0.1f){
            return 4;
        }
        else if(0.1f <= diffScore && diffScore < 0.2f){
            return 3;
        }
        else if(0.2f <= diffScore && diffScore < 0.3f){
            return 2;
        }
        else if(0.3f <= diffScore && diffScore <= 0.4f){
            return 1;
        }
        else {
            return 0;
        }
        return 0;
    }

    //エネミー生成範囲決定
    public float[] GetRange(float diffScore){
        float[] range = new float[2];
        if(-0.5f <= diffScore && diffScore < -0.3f){
            range[0] = 2.5f;
            range[1] = 1.5f;
            return range;
        }
        else if(-0.3f <= diffScore && diffScore < -0.1f){
            range[0] = 3.5f;
            range[1] = 1.8f;
            return range;
        }
        else if(-0.1f <= diffScore && diffScore < 0.1f){
            range[0] = 4.5f;
            range[1] = 2.0f;
            return range;
        }
        else if(0.1f <= diffScore && diffScore < 0.3f){
            range[0] = 5.5f;
            range[1] = 2.2f;
            return range;
        }
        else if(0.3f <= diffScore && diffScore <= 0.5f){
            range[0] = 6.5f;
            range[1] = 2.4f;
            return range;
        }
        range[0] = 6.5f;
        range[1] = 2.0f;
        return range;
    }

    //エネミーのポジション決定
    public Vector3 GetEnemyPosition(float distance, int direction){
        switch (direction)
        {
            case 1:
                return Camera.main.transform.position + Camera.main.transform.forward * distance;
            case 2:
                return Camera.main.transform.position + Camera.main.transform.right * distance;
            case 3:
                return  Camera.main.transform.position + Camera.main.transform.right * -1 * distance;
            case 4: 
                return Camera.main.transform.position + Camera.main.transform.forward * -1 * distance;
            case 5: 
                return Camera.main.transform.position + Camera.main.transform.forward * distance +  Camera.main.transform.right * distance;
            case 6: 
                return Camera.main.transform.position + Camera.main.transform.forward * distance +  Camera.main.transform.right * -1 * distance;
            case 7: 
                return Camera.main.transform.position + Camera.main.transform.forward * -1 * distance +  Camera.main.transform.right * distance;
            case 8:
                return Camera.main.transform.position + Camera.main.transform.forward * -1 * distance +  Camera.main.transform.right * -1 * distance;
            default:
                break;
        }
        return Camera.main.transform.position + Camera.main.transform.forward * distance;
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
        float myPoint = ((float)myTotalScore / (float)totalScore) * 100;

        if(myPoint >= 50){
            scoreText.text = "You are winner !";
        }
        else{
            scoreText.text = "You are loser";
        }

    }

    public void AddScore(int score){
        this.additionalPoint += score;
    }
}
