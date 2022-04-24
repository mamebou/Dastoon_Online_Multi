using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine.UI;
using System;

public class InputCanvasController : MonoBehaviour
{
    public GameObject inputCanvas;
    public GameObject addDataButton;
    private Interactable interact;
    private Text userName;
    private String[] key = {"data1", "data2", "data3"};
    GameObject main;

    // Start is called before the first frame update
    void Start()
    {
        interact = addDataButton.GetComponent<Interactable>();
        interact.OnClick.AddListener(OnAddDataClick);
        main =  GameObject.Find("Main");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnAddDataClick(){
        userName = transform.Find("Input/VerticalGroup/MRKeyboardInputField/Text").GetComponent<Text>();

        //PlayerPrefsでユーザ情報保存、ほんとはあんまよくない
        for(int dataNum = 0; dataNum < key.Length; dataNum++){
            if (!PlayerPrefs.HasKey(key[dataNum]+"Name")){
                PlayerPrefs.SetString(key[dataNum]+"Name",userName.text);
                PlayerPrefs.Save();
                break;
            }
        }

        //保存したらcanvas切り替えの絶え目にTopControllerのメソッドを呼ぶ
        main.GetComponent<TopController>().pressCreateData();
        Debug.Log(PlayerPrefs.GetString("data1Name"));
    }
}
