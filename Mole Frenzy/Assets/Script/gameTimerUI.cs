using UnityEngine;
using System.Collections;

public class gameTimerUI : MonoBehaviour {
    handler handler;
	// Use this for initialization
	void Start () {
        handler = GameObject.Find("Ground").GetComponent<handler>();

    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<TextMesh>().text = convertTimerToString(handler.getTimeLimit() - handler.getCurrentTime());
    }

    string convertTimerToString(int scs)
    {
        if(scs > 0)
        {
            int minutes;
            int seconds;
            minutes = scs / 60;
            seconds = scs % 60;
            return "" + minutes + " : " + System.String.Format("{0:00}", seconds);
        }
        return "0 : 00";
        
    }
}
