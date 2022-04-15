using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopController : MonoBehaviour
{
    public TouchScreenKeyboard keyboard;

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
        //SceneManager.LoadScene("Understanding-Simple");
    }
}
