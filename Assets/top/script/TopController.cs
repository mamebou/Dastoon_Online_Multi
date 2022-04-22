using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; //text mesh proを扱う際に必要
using UnityEngine.UI;  //Canvasをいじる時に使う

public class TopController : MonoBehaviour
{
    public TouchScreenKeyboard keyboard;
    public GameObject select_canvas;
    

    // Start is called before the first frame update
    void Start()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeScene(){
        SceneManager.LoadScene("Understanding-Simple");
        Debug.Log("button is pressed");
    }
}
