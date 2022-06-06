using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//scoreゲージ制御用スクリプト
public class ScoreGauge : MonoBehaviour
{

    public int MaxValue = 100;
    public int MinValue = 0;
    private int count;
    public Slider gauge; 
    // Start is called before the first frame update
    void Start()
    {
        gauge.minValue = 0;
        gauge.maxValue = 100;
        gauge.value = 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
