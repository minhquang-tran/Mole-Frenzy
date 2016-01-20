using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class eventSound : MonoBehaviour {
    AudioSource menuMusicClip;

    void Start () {
        AudioSource[] audios = GetComponents<AudioSource>();
        menuMusicClip = audios[0];
        menuMusicClip.Play();
    }

    void Update()
    {

    }   
}
