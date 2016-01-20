using UnityEngine;
using System.Collections;

public class soundCamera : MonoBehaviour {
    public AudioClip[] myAudioClip;
    AudioSource readyGoClip;
    AudioSource timeOverClip;
    AudioSource backgroundClip;
    AudioSource ohMyClip;
    AudioSource scoreClip;
    AudioSource perfectClip;
    AudioSource whackClip;
    AudioSource windblowClip;
    AudioSource hurryUpClip;
    // Use this for initialization
    void Start()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        readyGoClip = audios[0];
        timeOverClip = audios[1];
        backgroundClip = audios[2];
        ohMyClip = audios[3];
        scoreClip = audios[4];
        perfectClip = audios[5];
        whackClip = audios[6];
        windblowClip = audios[7];
        hurryUpClip = audios[8];
        PlaySound(2);
        PlaySound(0);      
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void stopSound(int src)
    {
        switch (src)
        {
            case 0:
                readyGoClip.Stop();
                break;
            case 1:
                timeOverClip.Stop();
                break;
            case 2:
                backgroundClip.Stop();
                break;
            case 3:
                ohMyClip.Stop();
                break;
            case 4:
                scoreClip.Stop();
                break;
            case 5:
                perfectClip.Stop();
                break;
            case 6:
                whackClip.Stop();
                break;
            case 7:
                windblowClip.Stop();               
                break;
            case 8:
                hurryUpClip.Stop();
                break;
            default:
                break;
        }
    }

    public void PlaySound(int src)
    {
        switch(src)
        {
            case 0:
                readyGoClip.Play();
                break;
            case 1:
                timeOverClip.Play();
                break;
            case 2:
                backgroundClip.Play();
                break;
            case 3:
                ohMyClip.Play();
                break;
            case 4:
                scoreClip.Play();
                break;
            case 5:
                perfectClip.Play();
                break;
            case 6:
                whackClip.Play();
                break;
            case 7:
                if (!windblowClip.isPlaying)
                {
                    windblowClip.Play();
                }
                break;
            case 8:
                if (!hurryUpClip.isPlaying)
                {
                    hurryUpClip.Play();
                }
                break;
            default:
                break;
        }
        
    }
}
