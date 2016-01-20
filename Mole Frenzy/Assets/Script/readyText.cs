using UnityEngine;
using System.Collections;

public class readyText : MonoBehaviour {


    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
    
            
    }

    public void setText(string t)
    {
        GetComponent<TextMesh>().text = t;
    }


}
