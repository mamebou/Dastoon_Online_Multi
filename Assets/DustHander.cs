using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustHander : MonoBehaviour
{

    private Color color;

    void Start()
    {
        color = color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeStage(int stageNum){
        switch(stageNum){
            case 1:
                color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
                break;
            case 2:
                color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
                break;
            case 3:
                color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                break;
            case 4:
                color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                break;
            case 5:
                color = new Color(1.0f, 0.0f, 1.0f, 1.0f);
                break;
            default:
                break;
        }
    }

    public Color SetColor()
    {
        return color;
    }
}
