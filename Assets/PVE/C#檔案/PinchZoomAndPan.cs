using UnityEngine;

public class PinchZoomAndPan : MonoBehaviour
{
    [SerializeField] private float panSpeed = 10f;
    [SerializeField] private float panBorderThickness = 5f;

    [SerializeField] private float minZoom = 6f;
    [SerializeField] private float maxZoom = 10f;

    private Camera cam;

    private Vector2 initialMinPanLimit;
    private Vector2 initialMaxPanLimit;

    private void Start()
    {
        cam = Camera.main;

        // Set initial pan limits
        initialMinPanLimit = new Vector2(-5f, -Mathf.Infinity); // Replace with your initial min pan limit
        initialMaxPanLimit = new Vector2(5f, Mathf.Infinity); // Replace with your initial max pan limit
    }

    private void Update()
    {
        HandlePinchZoom();
        HandleCameraPan();
    }

    private void HandlePinchZoom()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            float zoomValue = cam.orthographicSize + deltaMagnitudeDiff * 0.01f;
            cam.orthographicSize = Mathf.Clamp(zoomValue, minZoom, maxZoom);
        }
    }

    private void HandleCameraPan()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
        
            float currentZoom = cam.orthographicSize;
            float zoomFactor = currentZoom / maxZoom;
        
            Vector2 adjustedMinPanLimit = initialMinPanLimit / zoomFactor;
            Vector2 adjustedMaxPanLimit = initialMaxPanLimit / zoomFactor;
        
            if (Mathf.Abs(touchDeltaPosition.x) > panBorderThickness)
            {
                float direction = Mathf.Sign(-touchDeltaPosition.x);
                float step = direction * panSpeed * Time.deltaTime;
        
                Vector3 newPosition = transform.position + new Vector3(step, 0, 0);
                newPosition.x = Mathf.Clamp(newPosition.x, adjustedMinPanLimit.x, adjustedMaxPanLimit.x);
        
                transform.position = newPosition;
            }
        
            if (Mathf.Abs(touchDeltaPosition.y) > panBorderThickness)
            {
                // Add vertical panning (if needed)
            }
        }
    }
}
