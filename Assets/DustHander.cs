using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustHander : MonoBehaviour
{

    private Color color;
    private int stageNum = 1;

    void Start()
    {
        color = color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeStage(){
        switch(stageNum){
            case 1:
                color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
                stageNum += 1;
                break;
            case 2:
                color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
                stageNum += 1;
                break;
            case 3:
                color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
                stageNum += 1;
                break;
            case 4:
                color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
                stageNum += 1;
                break;
            case 5:
                color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
                stageNum += 1;
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
