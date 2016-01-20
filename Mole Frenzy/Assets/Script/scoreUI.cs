using UnityEngine;
using System.Collections;

public class scoreUI : MonoBehaviour
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
        GetComponent<TextMesh>().text = "" + handler.getScore();
    }    
}
