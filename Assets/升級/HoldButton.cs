using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class HoldButton : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] GameObject[] Up_Images; // The all pictures in up level_up
    private bool bool_dragging = false;
    private GameObject Copy_image; // the upper image when you drag
    public Canvas canvas;
    public PartyManage partyManager;
    private int index;
    private Dictionary<string, int> Image_Dictionary = new Dictionary<string, int>() {
        { "西瓜1", 0 }, { "西瓜2", 1 }, { "西瓜3", 2 }, { "西瓜4", 3 }, { "西瓜5", 4 }, { "西瓜6", 5 }, { "西瓜7", 6 },
        { "香蕉1", 0 }, { "香蕉2", 1 }, { "香蕉3", 2 }, { "香蕉4", 3 }, { "香蕉5", 4 }, { "香蕉6", 5 }, { "香蕉7", 6 },
        { "辣椒1", 0 }, { "辣椒2", 1 }, { "辣椒3", 2 }, { "辣椒4", 3 }, { "辣椒5", 4 }, { "辣椒6", 5 }, { "辣椒7", 6 },
        { "香菇1", 0 }, { "香菇2", 1 }, { "香菇3", 2 }, { "香菇4", 3 }, { "香菇5", 4 }, { "香菇6", 5 }, { "香菇7", 6 },
    };
    private ServerMethod.Server ServerScript; // Server.cs

    void Start() {
        if(MainMenu.message!=87)    ServerScript = FindObjectOfType<ServerMethod.Server>();
    }
    public void OnPointerDown(PointerEventData eventData) {
        bool_dragging = true;
        if(eventData.pointerCurrentRaycast.gameObject != null) {
            GameObject Clicked_Object = eventData.pointerCurrentRaycast.gameObject;
            if(Image_Dictionary.TryGetValue(Clicked_Object.name, out int value)) {
                index = value;
                Copy(index);
            }
            else    index = 0;
        }
    }
    bool IsMouseOverSpecificUI(string target){
        PointerEventData eventData = new PointerEventData(EventSystem.current); // 检查是否有鼠标悬停在UI元素上
        eventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();// 获取射线碰撞的所有UI元素
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)    if (result.gameObject.tag == target)    return true; // 检查射线碰撞的物体是否是特定UI物体

        return false; // 如果没有碰撞的UI元素或者碰撞的不是特定UI物体，返回 false
    }
    public void OnDrag(PointerEventData eventData) {
        if (bool_dragging && Copy_image != null)    Copy_image.transform.position = Input.mousePosition; // 將生成的物件移動到滑鼠位置
    }
    public void OnPointerUp(PointerEventData eventData) {
        bool_dragging = false;
        if (Copy_image != null)
        {
            int tmp_position = -1;
            int tmp_value = -1;
            int party_num = 0;
            if (IsMouseOverSpecificUI("party_1"))   party_num = 0;
            else if (IsMouseOverSpecificUI("party_2"))   party_num = 1;
            else if (IsMouseOverSpecificUI("party_3"))   party_num = 2;
            else if (IsMouseOverSpecificUI("party_4"))   party_num = 3;
            else if (IsMouseOverSpecificUI("party_5"))   party_num = 4;
            {   
                for(int i = 0; i<5 ; i++) {
                    if(MainMenu.message==87) {
                        if(MainMenu.lineup[i]==index+1) {
                            tmp_position = i;
                            tmp_value = MainMenu.lineup[party_num];
                        }
                    }
                    else {
                        if(ServerScript.lineup[i]==index+1) {
                            tmp_position = i;
                            tmp_value = ServerScript.lineup[party_num];
                        }
                    }
                }
                if(tmp_position!=-1) {
                    if(MainMenu.message==87) {
                        MainMenu.lineup[tmp_position] = tmp_value;
                        MainMenu.lineup[party_num] = index + 1;
                    }
                    else {
                        ServerScript.lineup[tmp_position] = tmp_value;
                        ServerScript.lineup[party_num] = index + 1;
                    }
                }
                else {
                    if(MainMenu.message==87)    MainMenu.lineup[party_num] = index + 1;
                    else        ServerScript.lineup[party_num] = index + 1;
                }
                Destroy(Copy_image);
            }
        }
    }
    void Copy(int index) {
        Vector3 mousePosition = Input.mousePosition;
        Copy_image = Instantiate(Up_Images[index], Input.mousePosition, Quaternion.identity, canvas.transform);// 生成物件
    }
}