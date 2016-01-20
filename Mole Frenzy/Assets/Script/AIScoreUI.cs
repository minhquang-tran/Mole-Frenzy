using UnityEngine;
using System.Collections;

public class AIScoreUI : MonoBehaviour {

    handler handler;
    // Use this for initialization
    void Start()
    {
        handler = GameObject.Find("Ground").GetComponent<handler>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMesh>().text = "" + handler.getAIScore();
    }
}
