using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class startTutorial : MonoBehaviour
{
    [SerializeField] public int finishTutorial;
    [SerializeField] public Image shop1, shop2, shop3, levelUp4, levelUp5, group6, story7, energy8,
                                  tears9, money10, friends11, group12, letstart13, preintroduce14, castle15, castle16,
                                  energy17, pressbutton18, character19, bow20, wind21, time22, start23, arrow;

    void Update() {
        finishTutorial = 1;
    }
    void Start() {
        if(finishTutorial == 0) {
            shop1.gameObject.SetActive(true);
            shop2.gameObject.SetActive(false);
            shop3.gameObject.SetActive(false);
            levelUp4.gameObject.SetActive(false);
            levelUp5.gameObject.SetActive(false);
            group6.gameObject.SetActive(false);
            story7.gameObject.SetActive(false);
            energy8.gameObject.SetActive(false);
            tears9.gameObject.SetActive(false);
            money10.gameObject.SetActive(false);
            friends11.gameObject.SetActive(false);
            group12.gameObject.SetActive(false);
            letstart13.gameObject.SetActive(false);
            preintroduce14.gameObject.SetActive(false);
            castle15.gameObject.SetActive(false);
            castle16.gameObject.SetActive(false);
            energy17.gameObject.SetActive(false);
            pressbutton18.gameObject.SetActive(false);
            character19.gameObject.SetActive(false);
            bow20.gameObject.SetActive(false);
            wind21.gameObject.SetActive(false);
            time22.gameObject.SetActive(false);
            start23.gameObject.SetActive(false);
            arrow.gameObject.SetActive(true);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Image Clicked!");
    }
    public void shop1Toshop2() {
        if(finishTutorial == 0) {
            shop1.gameObject.SetActive(false);
            shop2.gameObject.SetActive(true);
            arrow.GetComponent<RectTransform>().position = new Vector3(525f, 225f, 0f);
        }
    }
    public void shop2Toshop3() {
        if(finishTutorial == 0) {
            shop2.gameObject.SetActive(false);
            shop3.gameObject.SetActive(true);
            arrow.GetComponent<RectTransform>().position = new Vector3(1250f, 225f, 0f);
        }
    }
    public void quit1() {
        if(finishTutorial == 0) {
            shop3.gameObject.SetActive(false);
            arrow.GetComponent<RectTransform>().position = new Vector3(1850f, 850f, 0f);
        }
    }
    public void shop3TolevelUp4() {
        if(finishTutorial == 0) {
            levelUp4.gameObject.SetActive(true);
            arrow.GetComponent<RectTransform>().position = new Vector3(1250f, 100f, 0f);
        }
    }
    public void levelUp4TolevelUp5() {
        if(finishTutorial == 0) {
            arrow.gameObject.SetActive(false);
            levelUp4.gameObject.SetActive(false);
            levelUp5.gameObject.SetActive(true);
        }
    }
    public void levelUp5Togroup6() {
        if(finishTutorial == 0) {
            arrow.gameObject.SetActive(false);
            levelUp5.gameObject.SetActive(false);
            group6.gameObject.SetActive(true);
        }
    }
    public void quit2() {
        if(finishTutorial == 0) {
            group6.gameObject.SetActive(false);
            arrow.gameObject.SetActive(true);
            arrow.GetComponent<RectTransform>().position = new Vector3(250f, 60f, 0f);
            arrow.GetComponent<RectTransform>().Rotate(0f, 0f, 180f);
        }
    }
    public void group6Tostory7() {
        if(finishTutorial == 0) {
            story7.gameObject.SetActive(true);
            arrow.GetComponent<RectTransform>().Rotate(0f, 0f, 180f);
            arrow.GetComponent<RectTransform>().position = new Vector3(800f, 100f, 0f);
        }
    }
    public void story7Toquit3() {
        if(finishTutorial == 0) {
            story7.gameObject.SetActive(false);
            arrow.GetComponent<RectTransform>().position = new Vector3(1600f, 850f, 0f);
        }
    }
    public void quit3Toenergy8() {
        if(finishTutorial == 0) {
            energy8.gameObject.SetActive(true);
            arrow.GetComponent<RectTransform>().position = new Vector3(575f, 800f, 0f);
        }
    }
    public void energy8Totears9() {
        if(finishTutorial == 0) {
            energy8.gameObject.SetActive(false);
            tears9.gameObject.SetActive(true);
            arrow.GetComponent<RectTransform>().position = new Vector3(925f, 800f, 0f);
        }
    }
    public void tears9Tomoney10() {
        if(finishTutorial == 0) {
            tears9.gameObject.SetActive(false);
            money10.gameObject.SetActive(true);
            arrow.GetComponent<RectTransform>().position = new Vector3(1300f, 800f, 0f);
        }
    }
    public void money10Tofriends11() {
        if(finishTutorial == 0) {
            money10.gameObject.SetActive(false);
            friends11.gameObject.SetActive(true);
            arrow.GetComponent<RectTransform>().position = new Vector3(1650f, 800f, 0f);
        }
    }
    public void friends11Togroup12() {
        if(finishTutorial == 0) {
            friends11.gameObject.SetActive(false);
            group12.gameObject.SetActive(true);
            arrow.gameObject.SetActive(false);
        }
    }
    public void group12Toletstart13() {
        if(finishTutorial == 0) {
            group12.gameObject.SetActive(false);
            letstart13.gameObject.SetActive(true);
            arrow.gameObject.SetActive(true);
            arrow.GetComponent<RectTransform>().position = new Vector3(1600f, 180f, 0f);
        }
    }
}
