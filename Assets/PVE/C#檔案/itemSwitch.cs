using UnityEngine;
using UnityEngine.UI;

public class ItemImageSwitcher : MonoBehaviour
{
    private ServerMethod.Server ServerScript;
    private new Renderer renderer;
    private Image image;

    public Sprite coldWindSprite; 
    public Sprite bombSprite; 

    void Start()
    {
        ServerScript = FindObjectOfType<ServerMethod.Server>();
        renderer = GetComponent<Renderer>();
        image = GetComponent<Image>();
        if(ServerScript.lineup[5]==1){
            image.sprite=coldWindSprite;
        }
        else{
            image.sprite=bombSprite;
        }
    }

    
}