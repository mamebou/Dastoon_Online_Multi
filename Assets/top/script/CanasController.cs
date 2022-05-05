using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System;
using UnityEngine.UI;//buttonを使用するときに使う
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CanasController : MonoBehaviour
{
    //変数定義
    private String[] key = {"data1", "data2", "data3"};
    public GameObject rootObject;//このスクリプトをアタッチしているオブジェクト
    private Interactable interact;
    private TextMeshPro dataText;
    public GameObject dataPrefab;//データテキスト用のオブジェクト
    public GameObject newDataButton;//新規作成用ボタン
    public GameObject selectDataButton;//データ選択ボタン
    public GameObject deleteDataButton;//データ削除ボタン
    private EventSystem eventSystem;
    private GameObject selectedButton;
    public static String playerName;
    
    GameObject main;
    //変数定義ここまで

    // Start is called before the first frame update
    void Start()
    {
        //使用変数設定
        main =  GameObject.Find("Main");

        for(int dataNum = 0; dataNum < 3; dataNum++){
            if (PlayerPrefs.HasKey(key[dataNum]+"Name")){
                //オブジェクト生成
                GameObject data = Instantiate(dataPrefab);
                dataText = data.GetComponent<TextMeshPro>();
                selectDataButton =  Instantiate(selectDataButton);
                deleteDataButton =  Instantiate(deleteDataButton);
                //タグ付け
                data.tag = key[dataNum]+"Text";
                selectDataButton.tag = key[dataNum]+"Select";
                deleteDataButton.tag = key[dataNum]+"Delete";
                //テキスト設定
                dataText.text = PlayerPrefs.GetString(key[dataNum]+"Name");
                //親子関係設定
                data.transform.SetParent(rootObject.transform);
                selectDataButton.transform.SetParent(rootObject.transform);
                deleteDataButton.transform.SetParent(rootObject.transform);
                //位置調整
                RectTransform pos = data.GetComponent<RectTransform>();
                pos.anchoredPosition3D = new Vector3(-0.1f,(float)dataNum*(-0.3f)+0.3f,-1.0f);
                deleteDataButton.transform.localPosition = new Vector3(0.4f, (float)dataNum*(-0.3f)+0.3f, -2.0f);
                selectDataButton.transform.localPosition = new Vector3(0.2f, (float)dataNum*(-0.3f)+0.3f, -2.0f);
                //サイズ調整
                data.transform.localScale = new Vector3(2.0f,2.0f,100.0f);
                newDataButton.transform.localScale = new Vector3(2.0f,2.0f,1.0f);
                selectDataButton.transform.localScale = new Vector3(5.0f,5.0f,100.0f);
                deleteDataButton.transform.localScale = new Vector3(5.0f,5.0f,100.0f);
                //ボタンを押したときのイベントを設定
                Interactable interactable = selectDataButton.GetComponent<Interactable>();
                Interactable interactableDelete = deleteDataButton.GetComponent<Interactable>();
                switch (dataNum)
                {
                    case 0:
                        interactable.OnClick.AddListener(() => DataSelect(1));
                        interactableDelete.OnClick.AddListener(() => DataDelete(1));
                        break;
                    case 1:
                        interactable.OnClick.AddListener(() => DataSelect(2));
                        interactableDelete.OnClick.AddListener(() => DataDelete(2));
                        break;
                    case 2:
                        interactable.OnClick.AddListener(() => DataSelect(3));
                        interactableDelete.OnClick.AddListener(() => DataDelete(3));
                        break;
                    default:
                        break;
                }
            }
        }

        //データ数が2以下の場合は新規データボタン        
        if (!(PlayerPrefs.HasKey(key[2]+"Name"))){
            //オブヘクト生成
            newDataButton =  Instantiate(newDataButton);
            //親子関係設定
            newDataButton.transform.SetParent(rootObject.transform);
            //サイズ設定
            newDataButton.transform.localScale = new Vector3(5.0f,5.0f,100.0f);
            //位置設定
            newDataButton.transform.localPosition = new Vector3(0.0f, -0.40f, -2.0f);
            //リスナー設定
            Interactable interactable = newDataButton.GetComponent<Interactable>();
            interactable.OnClick.AddListener(main.GetComponent<TopController>().pressNewData);
        }
    }

    public void DataSelect(int dataNum){
       playerName = PlayerPrefs.GetString(key[dataNum-1]+"Name");
       SceneManager.LoadScene("Understanding-Simple");
    }

    public void DataDelete(int dataNum){  
        GameObject DeletedSelectButton;
        GameObject DeletedDeleteButton;
        GameObject DeletedData;

        PlayerPrefs.DeleteKey(key[dataNum-1]+"Name");
        DeletedSelectButton = GameObject.FindWithTag(key[dataNum-1]+"Select");
        DeletedDeleteButton = GameObject.FindWithTag(key[dataNum-1]+"Delete");
        DeletedData = GameObject.FindWithTag(key[dataNum-1]+"Text");

        Destroy(DeletedSelectButton);
        Destroy(DeletedDeleteButton);
        Destroy(DeletedData);
    }

    public static String getPlayerName(){
        return playerName;
    }
}
