using UnityEngine;
using UnityEngine.UI;
public class ScrollZoom : MonoBehaviour
{
    public GameObject MidPoint;
    public ScrollRect scrollRect;
    public float zoomFactor = 2.0f;
    private RectTransform contentRect;
    private float[] distances;

    void Start() {
        contentRect = scrollRect.content;
        int itemCount = contentRect.childCount;
        distances = new float[itemCount];
    }
    void Update() {
        CalculateDistances();
        int centerIndex = FindNearestCenter();
        for (int i = 0; i < contentRect.childCount; i++) {
            RectTransform item = contentRect.GetChild(i) as RectTransform;
            float scale = (i == centerIndex) ? zoomFactor : 1.5f;
            item.localScale = new Vector3(scale, scale, 1.0f);
            RectTransform rectTransform = item.GetComponent<RectTransform>();
            Vector3 newPosition = rectTransform.localPosition;
            newPosition.y = (i == centerIndex) ? -200f : -250f;
            rectTransform.localPosition = newPosition;
        }
    }
    void CalculateDistances()
    {
        for (int i = 0; i < contentRect.childCount; i++) {
            RectTransform item = contentRect.GetChild(i) as RectTransform;
            distances[i] = Mathf.Abs(MidPoint.transform.position.x - item.position.x);
        }
    }
    int FindNearestCenter() {
        float minDistance = Mathf.Min(distances);
        for (int i = 0; i < distances.Length; i++)      if (distances[i] == minDistance)    return i;
        return -1;
    }
}