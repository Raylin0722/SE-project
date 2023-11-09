using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip First_bgm;
    public AudioClip Last_bgm;

    //float passtime = 0f;
    bool SwitchBGM = false;

    void Start()
    {
        //passtime = 0f;
        StartCoroutine(SwitchBGMTime(120f));
    }

    private IEnumerator SwitchBGMTime(float delay){

        yield return new WaitForSeconds(delay);

        if(!SwitchBGM){
            SwitchBGM = true;
            AudioSource.enabled = true;
            AudioSource.clip = Last_bgm;
            AudioSource.loop = true;
            AudioSource.volume = 0.7f;
            AudioSource.Play();
        }

    }

    void Awake()
    {
        PlayBGM();
    }

    private void PlayBGM(){

        if(!SwitchBGM){
            AudioSource.enabled = true;
            AudioSource.clip = First_bgm;
            AudioSource.loop = true;
            AudioSource.volume = 0.7f;
            AudioSource.Play();
        }

    }
}
