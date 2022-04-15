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
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            // ê¬
            color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // êÖêF
            color = new Color(0.0f, 1.0f, 1.0f, 1.0f);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // óŒ
            color = new Color(0.0f, 1.0f, 0.0f, 1.0f);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // â©
            color = new Color(1.0f, 1.0f, 0.0f, 1.0f);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // ûÚ
            color = new Color(1.0f, 0.5f, 0.0f, 1.0f);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            // ê‘
            color = new Color(1.0f, 0.0f, 0.0f, 1.0f);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            // îí
            color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            // çï
            color = new Color(0.0f, 0.0f, 0.0f, 1.0f);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            // ó\îı
            color = new Color(0.0f, 0.0f, 0.0f, 1.0f);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            // ó\îı
            color = new Color(0.0f, 0.0f, 0.0f, 1.0f);

        }
        else
        {

        }


    }

    public Color SetColor()
    {
        return color;
    }
}
