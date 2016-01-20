using UnityEngine;
using System.Collections;

public class attackEffect : MonoBehaviour {
    public int particleNum;
    // Use this for initialization
    void Start ()
    {
        GetComponent<ParticleSystem>().Emit(particleNum);
    }

    // Update is called once per frame
    void Update () {
        if(GetComponent<ParticleSystem>().particleCount == 0 && gameObject.name.Contains("Clone"))
        {
            Destroy(this.gameObject);
        }
    }

    

}
