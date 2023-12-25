using System.Collections;
using UnityEngine;
public class AudioManager : MonoBehaviour{
    public AudioSource AudioSource;
    public AudioClip First_bgm;
    public AudioClip Last_bgm;
    bool SwitchBGM = false;
    private ServerMethod.Server ServerScript; // Server.cs
    private bool bool_slient = false;
    public GameObject slient_BackVolume;
    public GameObject slient_Volume;
    void Start(){
        StartCoroutine(SwitchBGMTime(120f));
    }
    private IEnumerator SwitchBGMTime(float delay){
        yield return new WaitForSeconds(delay);
        if(!SwitchBGM){
            SwitchBGM = AudioSource.enabled = AudioSource.loop = true;
            AudioSource.clip = Last_bgm;
            if(MainMenu.message==87)    AudioSource.volume = Mathf.Clamp01((float)MainMenu.backVolume/100f);
            else    AudioSource.volume = Mathf.Clamp01((float)ServerScript.backVolume/100f);
            AudioSource.Play();
        }
    }
    void Awake(){
        if(MainMenu.message!=87)    ServerScript = FindObjectOfType<ServerMethod.Server>();
        PlayBGM();
    }
    private void PlayBGM(){
        if(!SwitchBGM){
            AudioSource.enabled = AudioSource.loop = true;
            AudioSource.clip = First_bgm;
            if(MainMenu.message==87)    AudioSource.volume = Mathf.Clamp01((float)MainMenu.backVolume/100f);
            else    AudioSource.volume = Mathf.Clamp01((float)ServerScript.backVolume/100f);
            if(AudioSource.volume==0.0f) {
                bool_slient = true;
                slient_BackVolume.SetActive(bool_slient);
            }
            AudioSource.Play();
        }
    }
    public void fake_slient(){
        slient_Volume.SetActive(!slient_Volume.activeSelf);
    }
    public void slient()
    {
        bool_slient = !bool_slient;
        print(bool_slient);
        slient_BackVolume.SetActive(bool_slient);
        if(bool_slient) {
            if(MainMenu.message==87)    AudioSource.volume = MainMenu.backVolume = 0;
            else {
                AudioListener.volume = ServerScript.backVolume = 0;
                StartCoroutine(ServerScript.setting(ServerScript.volume,ServerScript.backVolume,ServerScript.shock));
            }
        }
        else {
            AudioListener.volume = Mathf.Clamp01(0.7f);
            if(MainMenu.message==87)    MainMenu.backVolume = 70;
            else {
                ServerScript.backVolume = 70;
                StartCoroutine(ServerScript.setting(ServerScript.volume,ServerScript.backVolume,ServerScript.shock));
            }
        }
    }
}