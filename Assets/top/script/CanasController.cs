using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System;
using UnityEngine.UI;//buttonを使用するときに使う
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

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
    //変数定義ここまで

    // Start is called before the first frame update
    void Start()
    {
        for(int dataNum = 0; dataNum < 3; dataNum++){
            if (PlayerPrefs.HasKey(key[dataNum]+"Name")){
                //オブジェクト生成
                GameObject data = Instantiate(dataPrefab);
                dataText = data.GetComponent<TextMeshPro>();
                newDataButton =  Instantiate(newDataButton);
                selectDataButton =  Instantiate(selectDataButton);
                deleteDataButton =  Instantiate(deleteDataButton);
                //テキスト設定
                dataText.text = PlayerPrefs.GetString(key[dataNum]+"Name");
                //親子関係設定
                data.transform.SetParent(rootObject.transform);
                newDataButton.transform.SetParent(rootObject.transform);
                selectDataButton.transform.SetParent(rootObject.transform);
                deleteDataButton.transform.SetParent(rootObject.transform);
                //位置調整
                RectTransform pos = data.GetComponent<RectTransform>();
                pos.anchoredPosition3D = new Vector3(-0.1f,(float)dataNum*(-0.3f)+0.3f,-1.0f);
                deleteDataButton.transform.localPosition = new Vector3(0.4f, (float)dataNum*(-0.3f)+0.3f, -1.0f);
                selectDataButton.transform.localPosition = new Vector3(0.2f, (float)dataNum*(-0.3f)+0.3f, -1.0f);
                //サイズ調整
                data.transform.localScale = new Vector3(2.0f,2.0f,100.0f);
                newDataButton.transform.localScale = new Vector3(2.0f,2.0f,1.0f);
                selectDataButton.transform.localScale = new Vector3(5.0f,5.0f,1.0f);
                deleteDataButton.transform.localScale = new Vector3(5.0f,5.0f,1.0f);
                //ボタンを押したときのイベントを設定
                Interactable interactable = selectDataButton.GetComponent<Interactable>();
                switch (dataNum)
                {
                    case 0:
                        interactable.OnClick.AddListener(() => DataSelect(1));
                        break;
                    case 1:
                        interactable.OnClick.AddListener(() => DataSelect(2));
                        break;
                    case 2:
                        interactable.OnClick.AddListener(() => DataSelect(3));
                        break;
                    default:
                        break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DataSelect(int dataNum){
       Debug.Log(dataNum);
    }
}
