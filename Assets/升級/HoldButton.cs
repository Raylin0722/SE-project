using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] GameObject[] Up_Images; // The all pictures in up level_up
    private bool bool_dragging = false;
    private GameObject Copy_image; // the upper image when you drag
    public Canvas canvas;
    public PartyManage partyManager;
    private int index;

    private Dictionary<string, int> Image_Dictionary = new Dictionary<string, int>()
    {
        { "西瓜1", 0 }, { "西瓜2", 1 }, { "西瓜3", 2 }, { "西瓜4", 3 }, { "西瓜5", 4 }, { "西瓜6", 5 }, { "西瓜7", 6 },
        { "香蕉1", 0 }, { "香蕉2", 1 }, { "香蕉3", 2 }, { "香蕉4", 3 }, { "香蕉5", 4 }, { "香蕉6", 5 }, { "香蕉7", 6 },
        { "辣椒1", 0 }, { "辣椒2", 1 }, { "辣椒3", 2 }, { "辣椒4", 3 }, { "辣椒5", 4 }, { "辣椒6", 5 }, { "辣椒7", 6 },
        { "香菇1", 0 }, { "香菇2", 1 }, { "香菇3", 2 }, { "香菇4", 3 }, { "香菇5", 4 }, { "香菇6", 5 }, { "香菇7", 6 },
    };
    private ServerMethod.Server ServerScript; // Server.cs

    void Start()
    {
        ServerScript = FindObjectOfType<ServerMethod.Server>();
    }
    void Update()
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        bool_dragging = true;
        if(eventData.pointerCurrentRaycast.gameObject != null)
        {
            GameObject Clicked_Object = eventData.pointerCurrentRaycast.gameObject;
            Debug.Log(Clicked_Object.name);
            if(Image_Dictionary.TryGetValue(Clicked_Object.name, out int value))
            {
                index = value;
                Copy(index);
            }
            else
            {
                index = 0;
                return;
            }
        }
    }

    bool IsMouseOverSpecificUI(string target)
    {
        // 检查是否有鼠标悬停在UI元素上
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        // 获取射线碰撞的所有UI元素
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        // 检查射线碰撞的物体是否是特定UI物体
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.tag == target)
            {
                return true;
            }
        }

        // 如果没有碰撞的UI元素或者碰撞的不是特定UI物体，返回 false
        return false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (bool_dragging && Copy_image != null)
        {
            // 將生成的物件移動到滑鼠位置
            Copy_image.transform.position = Input.mousePosition;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        bool_dragging = false;
        if (Copy_image != null)
        {
            if (IsMouseOverSpecificUI("party_1"))
            {
                //partyManager.SetPartyMemberValue(0, index);
                int tmp_position = -1;
                int tmp_value = -1;
                for(int i = 0; i<5 ; i++)
                {
                    if(ServerScript.lineup[i]==index+1)
                    {
                        tmp_position = i;
                        tmp_value = ServerScript.lineup[0];
                    }
                }
                if(tmp_position!=-1)    ServerScript.lineup[tmp_position] = tmp_value;
                ServerScript.lineup[0] = index + 1;
                Destroy(Copy_image);
            }
            else if (IsMouseOverSpecificUI("party_2"))
            {
                //partyManager.SetPartyMemberValue(1, index);
                int tmp_position = -1;
                int tmp_value = -1;
                for(int i = 0; i<5 ; i++)
                {
                    if(ServerScript.lineup[i]==index+1)
                    {
                        tmp_position = i;
                        tmp_value = ServerScript.lineup[1];
                    }
                }
                if(tmp_position!=-1)    ServerScript.lineup[tmp_position] = tmp_value;
                ServerScript.lineup[1] = index + 1;
                Destroy(Copy_image);
            }
            else if (IsMouseOverSpecificUI("party_3"))
            {
                //partyManager.SetPartyMemberValue(2, index);
                int tmp_position = -1;
                int tmp_value = -1;
                for(int i = 0; i<5 ; i++)
                {
                    if(ServerScript.lineup[i]==index+1)
                    {
                        tmp_position = i;
                        tmp_value = ServerScript.lineup[2];
                    }
                }
                if(tmp_position!=-1)    ServerScript.lineup[tmp_position] = tmp_value;
                ServerScript.lineup[2] = index + 1;
                Destroy(Copy_image);
            }
            else if (IsMouseOverSpecificUI("party_4"))
            {
                //partyManager.SetPartyMemberValue(3, index);
                int tmp_position = -1;
                int tmp_value = -1;
                for(int i = 0; i<5 ; i++)
                {
                    if(ServerScript.lineup[i]==index+1)
                    {
                        tmp_position = i;
                        tmp_value = ServerScript.lineup[3];
                    }
                }
                if(tmp_position!=-1)    ServerScript.lineup[tmp_position] = tmp_value;
                ServerScript.lineup[3] = index + 1;
                Destroy(Copy_image);
            }
            else if (IsMouseOverSpecificUI("party_5"))
            {
                //partyManager.SetPartyMemberValue(4, index);
                int tmp_position = -1;
                int tmp_value = -1;
                for(int i = 0; i<5 ; i++)
                {
                    if(ServerScript.lineup[i]==index+1)
                    {
                        tmp_position = i;
                        tmp_value = ServerScript.lineup[4];
                    }
                }
                if(tmp_position!=-1)    ServerScript.lineup[tmp_position] = tmp_value;
                ServerScript.lineup[4] = index + 1;
                Destroy(Copy_image);
            }
            else
            {
                Destroy(Copy_image);
            }
        }
        // 啟用 ScrollView 滑動
        //scrollRect.enabled = true;
    }

    void Copy(int index)
    {
        Vector3 mousePosition = Input.mousePosition;
        Debug.Log(Up_Images.Length);
        // 生成物件
        Copy_image = Instantiate(Up_Images[index], Input.mousePosition, Quaternion.identity, canvas.transform);
    }
}