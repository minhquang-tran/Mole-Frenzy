using UnityEngine;
using System.Collections;
using System;

public class menuBtn : MonoBehaviour
{
    handler handler;
    // Use this for initialization
    void Start()
    {
        handler = GameObject.Find("Ground").GetComponent<handler>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        if (!handler.isPaused())
        {
            handler.pauseGame();
        }
    }

    public void setTextColor(Color cl)
    {
        GetComponent<TextMesh>().color = cl;
    }
}
   