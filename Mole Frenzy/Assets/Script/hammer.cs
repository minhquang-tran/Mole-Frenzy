using UnityEngine;
using System.Collections;

public class hammer : MonoBehaviour {

    Vector3 targetPosition;
    public bool attack;
    handler handler;

    // Use this for initialization
    void Start()
    {
        attack = false;
        //handler = GameObject.Find("Ground").GetComponent<handler>();
    }

    void Update()
    {

        if(!GetComponent<Animation>().isPlaying)
        {
            transform.position = new Vector3(29, 30, -40);
        }
    }   
}
