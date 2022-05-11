using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//掃除機のサイズ調整
public class SliderManager : MonoBehaviour
{
    public GameObject brush;
    // Start is called before the first frame update
    void Start()
    {
        brush = GameObject.Find("Brush");
        
    }
    public void OnSliderUpdated(SliderEventData eventData)
    {
        Vector3 scale = new Vector3(0.5f, 0.5f, eventData.NewValue * 1.1f);
        brush.transform.localScale = scale;
    }


}
