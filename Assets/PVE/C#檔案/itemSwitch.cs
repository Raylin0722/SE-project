using UnityEngine;
using UnityEngine.UI;
public class ItemImageSwitcher : MonoBehaviour{
    private ServerMethod.Server ServerScript;
    private new Renderer renderer;
    private Image image;
    public Sprite coldWindSprite; 
    public Sprite bombSprite; 
    void Start(){
        if(MainMenu.message==87 && image!=null)
        {
            if(MainMenu.lineup[5]==1)image.sprite=coldWindSprite;else image.sprite=bombSprite;
            return;
        }
        ServerScript = FindObjectOfType<ServerMethod.Server>();
        renderer = GetComponent<Renderer>();
        image = GetComponent<Image>();
        if(ServerScript.lineup[5]==1)image.sprite=coldWindSprite;else image.sprite=bombSprite;
    }
}