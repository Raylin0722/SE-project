using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorial : MonoBehaviour
{
    public Button shop, book, levelUp, startGame, friend, setting, rank, change, tears;
    public Button open1, open2, cancelbutton;
    public Button rankUp, back;
    int tutorial_ing = 0;
    private ServerMethod.Server ServerScript;
    void Start() {
        ServerScript = FindObjectOfType<ServerMethod.Server>();
    }
    void Update()
    {

        if(FruitSelect.start_tutorial == 1) {
            starttutorial();
            Debug.Log("start tutorial");
            tutorial_ing = 1;
            FruitSelect.start_tutorial = 0;
        }
    }
    public GameObject[] watermelon_tutorial;
    public GameObject[] banana_tutorial;
    public GameObject[] highlight;
    public GameObject[] arrow;
    public GameObject[] NoticeMessage;
    int tutorial_image_count = 0;

    public void starttutorial() { //新手教學
        book.interactable = false;
        levelUp.interactable = false;
        startGame.interactable = false;
        friend.interactable = false;
        setting.interactable = false;
        rank.interactable = false;
        change.interactable = false;
        tears.interactable = false;
        open1.interactable = false;
        open2.interactable = false;
        cancelbutton.interactable = false;
        rankUp.interactable = false;
        back.interactable = false;
        
        image1();
    }
    public void image1() {
        if(ServerScript.faction[2] == 1)
            watermelon_tutorial[tutorial_image_count].SetActive(true);
        else if(ServerScript.faction[3] == 1)
            banana_tutorial[tutorial_image_count].SetActive(true);
        
        highlight[0].SetActive(true);
    }
    public void image2() {
        if(tutorial_ing != 1)
            return;

        shop.interactable = false;

        if(ServerScript.faction[2] == 1) {
            watermelon_tutorial[tutorial_image_count++].SetActive(false);
            watermelon_tutorial[tutorial_image_count].SetActive(true);
        }
        else if(ServerScript.faction[3] == 1) {
            banana_tutorial[tutorial_image_count++].SetActive(false);
            banana_tutorial[tutorial_image_count].SetActive(true);
        }

        highlight[0].SetActive(false);
        arrow[0].SetActive(true);
    }
    public void image3() {
        if(ServerScript.faction[2] == 1) {
            watermelon_tutorial[tutorial_image_count++].SetActive(false);
            watermelon_tutorial[tutorial_image_count].SetActive(true);
        }
        else if(ServerScript.faction[3] == 1) {
            banana_tutorial[tutorial_image_count++].SetActive(false);
            banana_tutorial[tutorial_image_count].SetActive(true);
        }
        arrow[0].SetActive(false);
        arrow[1].SetActive(true);
    }
    public void quit1() {
        cancelbutton.interactable = true;

        if(ServerScript.faction[2] == 1) {
            watermelon_tutorial[tutorial_image_count].SetActive(false);
        }
        else if(ServerScript.faction[3] == 1) {
            banana_tutorial[tutorial_image_count].SetActive(false);
        }

        arrow[1].SetActive(false);
        highlight[1].SetActive(true);
        NoticeMessage[0].SetActive(true);
    }

    public void image4() {
        if(tutorial_ing != 1)
            return;

        cancelbutton.interactable = false;
        levelUp.interactable = true;
        
        tutorial_image_count++;
        if(ServerScript.faction[2] == 1) {
            watermelon_tutorial[tutorial_image_count].SetActive(true);
        }
        else if(ServerScript.faction[3] == 1) {
            banana_tutorial[tutorial_image_count].SetActive(true);
        }

        highlight[1].SetActive(false);
        NoticeMessage[0].SetActive(false);
        highlight[2].SetActive(true);
    }

    public void image5() {
        if(tutorial_ing != 1)
            return;

        levelUp.interactable = false;

        if(ServerScript.faction[2] == 1) {
            watermelon_tutorial[tutorial_image_count++].SetActive(false);
            watermelon_tutorial[tutorial_image_count].SetActive(true);
        }
        else if(ServerScript.faction[3] == 1) {
            banana_tutorial[tutorial_image_count++].SetActive(false);
            banana_tutorial[tutorial_image_count].SetActive(true);
        }

        highlight[2].SetActive(false);
        arrow[2].SetActive(true);
    }

    public void image6() {
        if(ServerScript.faction[2] == 1) {
            watermelon_tutorial[tutorial_image_count++].SetActive(false);
            watermelon_tutorial[tutorial_image_count].SetActive(true);
        }
        else if(ServerScript.faction[3] == 1) {
            banana_tutorial[tutorial_image_count++].SetActive(false);
            banana_tutorial[tutorial_image_count].SetActive(true);
        }

        arrow[2].SetActive(false);
    }

    public void quit2() {
        back.interactable = true;

        if(ServerScript.faction[2] == 1) {
            watermelon_tutorial[tutorial_image_count].SetActive(false);
        }
        else if(ServerScript.faction[3] == 1) {
            banana_tutorial[tutorial_image_count].SetActive(false);
        }

        highlight[3].SetActive(true);
        NoticeMessage[1].SetActive(true);
    }

    public void image7() {
        if(tutorial_ing != 1)
            return;

        back.interactable = false;
        book.interactable = true;

        
        
        tutorial_image_count++;
        if(ServerScript.faction[2] == 1) {
            watermelon_tutorial[tutorial_image_count].SetActive(true);
        }
        else if(ServerScript.faction[3] == 1) {
            banana_tutorial[tutorial_image_count].SetActive(true);
        }

        highlight[3].SetActive(false);
        NoticeMessage[1].SetActive(false);
        highlight[4].SetActive(true);
    }

    public void quit3() {
        if(tutorial_ing != 1)
            return;

        book.interactable = false;

        if(ServerScript.faction[2] == 1) {
            watermelon_tutorial[tutorial_image_count].SetActive(false);
        }
        else if(ServerScript.faction[3] == 1) {
            banana_tutorial[tutorial_image_count].SetActive(false);
        }

        highlight[4].SetActive(false);
        highlight[5].SetActive(true);
        NoticeMessage[2].SetActive(true);
    }

    public void image8() {
        if(tutorial_ing != 1)
            return;

        tutorial_image_count++;
        if(ServerScript.faction[2] == 1) {
            watermelon_tutorial[tutorial_image_count].SetActive(true);
        }
        else if(ServerScript.faction[3] == 1) {
            banana_tutorial[tutorial_image_count].SetActive(true);
        }

        highlight[5].SetActive(false);
        NoticeMessage[2].SetActive(false);
        arrow[3].SetActive(true);
    }

    public void image9() {
        if(ServerScript.faction[2] == 1) {
            watermelon_tutorial[tutorial_image_count++].SetActive(false);
            watermelon_tutorial[tutorial_image_count].SetActive(true);
        }
        else if(ServerScript.faction[3] == 1) {
            banana_tutorial[tutorial_image_count++].SetActive(false);
            banana_tutorial[tutorial_image_count].SetActive(true);
        }

        arrow[3].SetActive(false);
        arrow[4].SetActive(true);
    }

    public void image10() {
        if(ServerScript.faction[2] == 1) {
            watermelon_tutorial[tutorial_image_count++].SetActive(false);
            watermelon_tutorial[tutorial_image_count].SetActive(true);
        }
        else if(ServerScript.faction[3] == 1) {
            banana_tutorial[tutorial_image_count++].SetActive(false);
            banana_tutorial[tutorial_image_count].SetActive(true);
        }

        arrow[4].SetActive(false);
        arrow[5].SetActive(true);
    }

    public void image11() {
        if(ServerScript.faction[2] == 1) {
            watermelon_tutorial[tutorial_image_count++].SetActive(false);
            watermelon_tutorial[tutorial_image_count].SetActive(true);
        }
        else if(ServerScript.faction[3] == 1) {
            banana_tutorial[tutorial_image_count++].SetActive(false);
            banana_tutorial[tutorial_image_count].SetActive(true);
        }

        arrow[5].SetActive(false);
    }

    public void image12() {
        startGame.interactable = true;

        if(ServerScript.faction[2] == 1) {
            watermelon_tutorial[tutorial_image_count++].SetActive(false);
            watermelon_tutorial[tutorial_image_count].SetActive(true);
        }
        else if(ServerScript.faction[3] == 1) {
            banana_tutorial[tutorial_image_count++].SetActive(false);
            banana_tutorial[tutorial_image_count].SetActive(true);
        }

        highlight[6].SetActive(true);
    }

    public GameObject background;
    public GameObject gamingScene;
    public GameObject nextPage;
    public GameObject cancel;
    public GameObject[] inGameArrow;
    public GameObject[] inGameWaterMelon;
    public GameObject[] inGameBanana;
    int inGamePhotoCount = 0;

    public void InGameTutorial() {
        if(tutorial_ing != 1)
            return;

        startGame.interactable = false;

        highlight[6].SetActive(false);
        watermelon_tutorial[tutorial_image_count].SetActive(false);
        banana_tutorial[tutorial_image_count].SetActive(false);

        background.SetActive(true);
        gamingScene.SetActive(true);
        nextPage.SetActive(true);
        cancel.SetActive(true);
        if(ServerScript.faction[2] == 1) {
            inGameWaterMelon[inGamePhotoCount].SetActive(true);
        }
        else if(ServerScript.faction[3] == 1) {
            inGameBanana[inGamePhotoCount].SetActive(true);
        }
    }

    public void nextpagebutton() {
        if(ServerScript.faction[2] == 1)
            inGameWaterMelon[inGamePhotoCount].SetActive(false);
        else if(ServerScript.faction[3] == 1)
            inGameBanana[inGamePhotoCount].SetActive(false);
        if(inGamePhotoCount > 0)
            inGameArrow[inGamePhotoCount-1].SetActive(false);

        if(inGamePhotoCount >= 0 && inGamePhotoCount < 9)
            inGamePhotoCount++;
        
        if(ServerScript.faction[2] == 1)
            inGameWaterMelon[inGamePhotoCount].SetActive(true);
        else if(ServerScript.faction[3] == 1)
            inGameBanana[inGamePhotoCount].SetActive(true);

        if(inGamePhotoCount >= 1 && inGamePhotoCount <= 8)
            inGameArrow[inGamePhotoCount-1].SetActive(true);

        if(inGamePhotoCount == 9)
            nextPage.SetActive(false);
    }

    public void canceltutorial() {
        background.SetActive(false);
        gamingScene.SetActive(false);
        nextPage.SetActive(false);
        cancel.SetActive(false);
        for(int i = 0;i < 10;i++) {
            inGameWaterMelon[i].SetActive(false);
            inGameBanana[i].SetActive(false);
        }
        for(int i = 0;i < 8;i++)
            inGameArrow[i].SetActive(false);

        endtutorial();
        tutorial_ing = 0;
    }

    public void endtutorial() {
        shop.interactable = true;
        book.interactable = true;
        levelUp.interactable = true;
        startGame.interactable = true;
        friend.interactable = true;
        setting.interactable = true;
        rank.interactable = true;
        change.interactable = true;
        tears.interactable = true;
        open1.interactable = true;
        open2.interactable = true;
        cancelbutton.interactable = true;
        rankUp.interactable = true;
        back.interactable = true;
    }
}
