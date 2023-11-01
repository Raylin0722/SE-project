using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))] // check whether we have a camera
public class COPY : MonoBehaviour
{
    public float Moving_Speed = 1.0f; // The speed of moving screen
    public float Zooming_Speed = 1.0f; // The speed of zooming screen
    private Camera Start_Camera; // The Camera we use
    private bool is_press = false; // a boolean value check whether you press

    private Vector3 Moving_Start_Position; // the 
    private float Zooming_End_Distance = -1; //

    void Start()
    {
        Start_Camera = GetComponent<Camera>();
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0)) // 0 is left , 1 is right , 2 is middle
        {
            is_press = true;
            Moving_Start_Position = Get_Position(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0)) // 0 is left , 1 is right , 2 is middle
        {
            is_press = false;
            Zooming_End_Distance = -1;
        }
        if (is_press)
        {
            Move(Input.mousePosition);
        }
        float distance = Input.GetAxis("Mouse ScrollWheel"); // the value of scrolling mouse wheel(-1~1)
        if (0 == distance)
        {
            return;
        }
        Scale(distance * Zooming_Speed);

#else
        if(Input.touchCount==2) // Two fingers
        {
            Zoom();
        }
        else if(Input.touchCount==1) // One finger
        {
            if(TouchPhase.Began==Input.touches[0].phase)
            {
                is_press = true ;
                Moving_Start_Position = Get_Position(Input.mousePosition);
            }
            else if(TouchPhase.Moved==Input.touches[0].phase)
            {
                Move(Input.touches[0].position);
            }
            Zooming_End_Distance = -1;
        }
        else
        {
            is_press = false;
            Zooming_End_Distance = -1;
        }
#endif
    }

    private void Move(Vector2 now_position)
    {
        Vector3 Moving_distance = Moving_Start_Position - Get_Position(now_position);
        Vector3 position = Start_Camera.transform.position;
        position.x = position.x + Moving_distance.x * Moving_Speed;
        /*if(position.x > 17)
        {
            position.x = 17;
        }
        if(position.x < -10)
        {
            position.x = -10;
        }*/
        position.y = position.y + Moving_distance.y * Moving_Speed;
        /*if(position.y > 1.4f)
        {
            position.y = 1.4f;
        }
        if(position.y < 0)
        {
            position.y = 0;
        }*/

        Start_Camera.transform.position = position;
    }

    private void Zoom()
    {
        float distance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position); // the distance between two fingers 
        if (Zooming_End_Distance == -1)
        {
            Zooming_End_Distance = distance;
        }
        Scale(distance - Zooming_End_Distance);
        Zooming_End_Distance = distance;
    }

    private void Scale(float scale)
    {
        Start_Camera.orthographicSize = Start_Camera.orthographicSize - Zooming_Speed * scale;
        if (Start_Camera.orthographicSize < 4)
        {
            Start_Camera.orthographicSize = 4;
        }
        if (Start_Camera.orthographicSize > 6)
        {
            Start_Camera.orthographicSize = 6;
        }
    }

    Vector3 Get_Position(Vector2 position)
    {
        return Start_Camera.ScreenToWorldPoint(new Vector3(position.x, position.y, 0));
    }
}