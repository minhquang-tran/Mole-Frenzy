using UnityEngine;
using System.Collections;

public class gainScoreEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if (gameObject.name.Contains("(Clone)"))
        {
            transform.Translate(new Vector3(0, 2.0f, 0) * Time.deltaTime);
            if (transform.position.y > 32.5f)
            {
                Destroy(gameObject);
            }
        }         
    }
}
