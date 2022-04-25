using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; //text mesh proを扱う際に必要
using UnityEngine.UI;  //Canvasをいじる時に使う

public class TopController : MonoBehaviour
{
    public TouchScreenKeyboard keyboard;
    public GameObject selectDataCanvas;
    public GameObject newDataCanvas;
    private GameObject selectCanvas;
    private GameObject newCanvas;

    // Start is called before the first frame update
    void Start()
    {
        GameObject selectCanvas = Instantiate(selectDataCanvas);
        selectCanvas.transform.position = new Vector3(0.0f, 0.0f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pressCreateData(){
        newCanvas = GameObject.Find("NewDataCanvas(Clone)");
        foreach(Transform child in newCanvas.transform){
            Destroy(child.gameObject);
        }
        Destroy(newCanvas);
        selectCanvas = Instantiate(selectDataCanvas);
        selectCanvas.transform.position = new Vector3(0.0f, 0.0f, 1.5f);
        Debug.Log("pressNewDatqa pressed");
    }

    public void pressNewData(){
        selectCanvas = GameObject.Find("Canvas(Clone)");  
        foreach(Transform child in selectCanvas.transform){
            Destroy(child.gameObject);
        }
        Destroy(selectCanvas);
        newCanvas = Instantiate(newDataCanvas);
        newDataCanvas.transform.position = new Vector3(0.0f, 0.0f, 1.5f);
        Debug.Log("pressCreateData pressed");
    }
}
