using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UI;
using System.Threading.Tasks;
using TMPro;

//scoreゲージ制御用スクリプト
public class ScoreGauge : MonoBehaviour
{

    public GameObject indicatorObject;
    private IProgressIndicator indicator;
    float Count = 5f;
    float value = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        indicator = indicatorObject.GetComponent<IProgressIndicator>();
        OpenIndigater();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private async void OpenIndigater(){
        await indicator.OpenAsync();
        indicator.Message = "player1 vs player2";
        indicator.Progress = 0.5f;
    }

   public async void UpdateGuage(float value){
        indicator.Progress = value;
    }
}
