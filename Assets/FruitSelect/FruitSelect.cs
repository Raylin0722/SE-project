using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;
using UnityEngine.EventSystems;

using ServerMethod;
using System;
public class FruitSelect : MonoBehaviour
{
    public GameObject ALL_Button; // ALL Button in Canvas of Main_Scene
    public GameObject page_FruitSelect; // the page for page_FruitSelect
    [SerializeField] GameObject bFrame;
    [SerializeField] GameObject wFrame;
    static public int fruit = -1;// 0->watermelon, 1->banana
    public VideoPlayer video; // the video before game
    private bool bool_play = false;
    public GameObject Music_main_scene; // the Music in main_scene
    public GameObject Skip;
    public static int start_tutorial = 0;

    // Server.cs
    private ServerMethod.Server ServerScript;

    // Start is called before the first frame update
    void Start()
    {
        ServerScript = FindObjectOfType<ServerMethod.Server>();
        Skip.gameObject.SetActive(false);
        //start_tutorial = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Play_Video(); // This is only for you to test,and you can delete it.
    }

    private IEnumerator Surver_Before_Game(Action<bool> callback)
    {
        IEnumerator coroutine = ServerScript.initFaction(2);
        yield return StartCoroutine(coroutine);
        
        Return result = coroutine.Current as Return;
        bool bool_success = false;
        if(result!=null)
        {
            bool_success = result.success;
        }

        callback.Invoke(bool_success);
    }

    // Check Button
    public void Check()
    {
        wFrame.SetActive(false);
        bFrame.SetActive(false);
        if(fruit==-1)   return;
        else if(fruit==0)
        {
            ServerScript.faction[2] = 1;
            ServerScript.faction[3] = 0;
            StartCoroutine(ServerScript.initFaction(2));
        }
        else
        {
            ServerScript.faction[2] = 0;
            ServerScript.faction[3] = 1;
            StartCoroutine(ServerScript.initFaction(3));      
        }
        ServerScript.faction[0] = 0;
        ServerScript.faction[1] = fruit + 2;
        fruit = -1;
        bool_play=false;
        page_FruitSelect.SetActive(false); // Close All button in Fruit Select
        ALL_Button.SetActive(true); // Open All button in Main_Scene
        Music_main_scene.SetActive(true); // Opne music in Main_Scene
    
        start_tutorial = 1;
    }

    // Close All button in Main_Scene
    public void Fruit_Select()
    {
        if(ServerScript.faction[0]==1)
        {
            page_FruitSelect.SetActive(true); // Open All button in Fruit Select
            video.gameObject.SetActive(false); // Close story vedio
        }
    }

    public void bhighlight()
    {
        bFrame.SetActive(true);
        wFrame.SetActive(false);
        fruit=1;
    }
    public void whighlight()
    {
        wFrame.SetActive(true);
        bFrame.SetActive(false);
        fruit=0;
    }

    public void Play_Video()
    {
        if(ServerScript.faction[0]==1 && bool_play==false)
        {
            Music_main_scene.SetActive(false); // Close music in Main_Scene
            ALL_Button.SetActive(false); // Close All button in Main_Scene
            video.gameObject.SetActive(true); // Open story vedio
            video.Play();
            bool_play = true;
            Skip.gameObject.SetActive(false);
        }
        video.loopPointReached += End_Video;
        if(video.time>5f)   
        {
            Skip.gameObject.SetActive(true);
        }
    }

    void End_Video(VideoPlayer video)
    {
        Skip_Video();
    }

    public void Skip_Video()
    {
        if(ServerScript.faction[0]==1 && bool_play==true)
        {
            video.Pause();
            Fruit_Select();
        }
    }
}