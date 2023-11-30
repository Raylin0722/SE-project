using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] GameObject[] objectToSpawnPrefab;  // 要生成的物件的預置體
    private RectTransform rectTransform;
    private ScrollRect scrollRect;
    private bool isDragging = false;
    private GameObject spawnedObject;
    public Canvas canvas;
    public PartyManage partyManager;
    private int index;


    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        scrollRect = GetComponentInParent<ScrollRect>();
    }
    void Update()
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        // 禁用 ScrollView 滑動
        //scrollRect.enabled = false;

        // 確保 eventData.pointerCurrentRaycast 不為空
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;

            // 在这里，你可以获取有关该物体的信息，或执行其他操作
            //Debug.Log("Clicked on: " + clickedObject.name);
            switch (clickedObject.name)
            {
                case "西瓜-1":
                    index = 1;
                    break;
                case "西瓜-2":
                    index = 2;
                    break;
                case "西瓜-3":
                    index = 3;
                    break;
                case "西瓜-4":
                    index = 4;
                    break;
                case "西瓜-5":
                    index = 5;
                    break;
                case "西瓜-6":
                    index = 6;
                    break;
                default:
                    index = 0;
                    break;
            }
            if (index == 0)
            {
                return;
            }
            // 生成物件
            SpawnObject(index);
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
        if (isDragging && spawnedObject != null)
        {
            // 將生成的物件移動到滑鼠位置
            spawnedObject.transform.position = Input.mousePosition;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        if (spawnedObject != null)
        {
            if (IsMouseOverSpecificUI("party_1"))
            {
                partyManager.SetPartyMemberValue(0, index);
                Destroy(spawnedObject);
            }
            else if (IsMouseOverSpecificUI("party_2"))
            {
                partyManager.SetPartyMemberValue(1, index);
                Destroy(spawnedObject);
            }
            else if (IsMouseOverSpecificUI("party_3"))
            {
                partyManager.SetPartyMemberValue(2, index);
                Destroy(spawnedObject);
            }
            else if (IsMouseOverSpecificUI("party_4"))
            {
                partyManager.SetPartyMemberValue(3, index);
                Destroy(spawnedObject);
            }
            else if (IsMouseOverSpecificUI("party_5"))
            {
                partyManager.SetPartyMemberValue(4, index);
                Destroy(spawnedObject);
            }
            else
            {
                Destroy(spawnedObject);
            }
        }
        // 啟用 ScrollView 滑動
        //scrollRect.enabled = true;
    }

    void SpawnObject(int index)
    {
        Vector3 mousePosition = Input.mousePosition;
        // 生成物件
        spawnedObject = Instantiate(objectToSpawnPrefab[index - 1], mousePosition, Quaternion.identity, canvas.transform);
    }
}