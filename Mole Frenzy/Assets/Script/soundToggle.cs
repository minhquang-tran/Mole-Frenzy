using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class soundToggle : MonoBehaviour {

    public static bool isOnSound = true;
    public GameObject toogle;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (isOnSound == true)
        {
            AudioListener.pause = false;

        }
        else
        {
            AudioListener.pause = true;

        }
    }

    public void OnOffSound()
    {
        if (isOnSound)
        {
            isOnSound = false;
        }
        else
        {
            isOnSound = true;
        }
    }
}
