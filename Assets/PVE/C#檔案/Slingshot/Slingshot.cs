using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Slingshot : MonoBehaviour
{
    public Canvas canvas;
    private Vector3 rockHoldPosition;
    public Transform LeftSlingshotOrigin,RightSlingshotOrigin;
    public LineRenderer SlingshotLeftRubber,SlingshotRightRubber;
    public Transform RockRestPositon;
    public GameObject Rock;
    public float ThrowSpeed;
    public SlingshotState slingshotState;
    private Vector3 SlingshotMiddleVector;
    private int energy;
    public LineRenderer Trajectory;
    public GameObject Cancel_Area;
    public GameObject Cancel_Area_UI;
    public int CharacterIdInCd_shoot;
    // Start is called before the first frame update
    void Start()
    {
        
        Rock=null;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerLayer"), LayerMask.NameToLayer("Tower1Layer"),true);
        SetSlingshotRubbersActive(false);
        slingshotState =SlingshotState.do_nothing;
        SlingshotLeftRubber.SetPosition(0,LeftSlingshotOrigin.position);
        SlingshotRightRubber.SetPosition(0,RightSlingshotOrigin.position);
        SlingshotMiddleVector = new Vector3((LeftSlingshotOrigin.position.x + RightSlingshotOrigin.position.x)/2,(LeftSlingshotOrigin.position.y + RightSlingshotOrigin.position.y)/2,0);

    }
    public void SetEnergy(int newEnergy)
    {
        energy = newEnergy;
    }
    void SetTrajectoryActive(bool active)
    {
        Trajectory.enabled = active;
    }
    void SetSlingshotRubbersActive(bool active)
    {
        SlingshotLeftRubber.enabled = active;
        SlingshotRightRubber.enabled = active;
    }
    void DisplaySlingshtRubbers()
    {
        SlingshotLeftRubber.SetPosition(1,Rock.transform.position);
        SlingshotRightRubber.SetPosition(1,Rock.transform.position);
    }
    void InitializeThrow()
    {
        rockHoldPosition = Vector3.zero;
        Rock.transform.position =new Vector3(RockRestPositon.position.x,RockRestPositon.position.y,0);
        slingshotState = SlingshotState.Idle;
        SetSlingshotRubbersActive(true); 
    }

    // Update is called once per frame
    void Update()
    {
        
        switch(slingshotState)
        {
            case SlingshotState.do_nothing:
                //Debug.Log(Rock.tag);
                SetTrajectoryActive(false);
                SetSlingshotRubbersActive(false);
                if(Rock!=null)
                {
                    Destroy(Rock);
                }
                
                break;
            case SlingshotState.Idle:

                Rock.tag = "Untagged";
                //Rock.GetComponent<BoxCollider2D>().enabled = false;
                SetSlingshotRubbersActive(true);
                InitializeThrow();
                DisplaySlingshtRubbers();
                if(Input.GetMouseButtonDown(0))
                {
                    Vector3 location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    location.z=0f;
                    float MouseToRock = Vector3.Distance(location, Rock.transform.position);
                    //Debug.Log(MouseToRock);
                    //Debug.Log("mouse"+location);
                    //Debug.Log("rock"+Rock.transform.position);
                    if ( MouseToRock < 1.0f)
                    {
                        //Debug.Log("要拉了");
                        slingshotState = SlingshotState.Pulling;
                    }
                    /*if(Rock.GetComponent<CircleCollider2D>() == Physics2D.OverlapPoint(location))
                    {
                        slingshotState = SlingshotState.Pulling;
                    }*/
                }
                break;
            
            case SlingshotState.Pulling:
                DisplaySlingshtRubbers();
                Cancel_Area_UI.SetActive(true);
                if (Input.GetMouseButton(0))
                {
                    
                    Vector3 location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    location.z = 0;
                    //float CancelDistance = Vector3.Distance(canvasMousePosition, Cancel_Area.transform.position);
                    //Debug.Log(CancelDistance);
                    float judge_front = SlingshotMiddleVector.x - location.x;
                    //Debug.Log("ditance"+Vector3.Distance(location, SlingshotMiddleVector));
                    if (judge_front < 0)
                    {
                        if (rockHoldPosition == Vector3.zero)
                        {
                            rockHoldPosition = Rock.transform.position;
                        }
                        Rock.transform.position = rockHoldPosition;
                    }
                    else if (Vector3.Distance(location, SlingshotMiddleVector) > 2.0f)
                    {
                        rockHoldPosition = Vector3.zero;
                        var maxPosition = (location - SlingshotMiddleVector).normalized * 2.0f + SlingshotMiddleVector;
                        Rock.transform.position = maxPosition;
                    }
                    else
                    {
                        
                        //Debug.Log("正常使用中");
                        rockHoldPosition = Vector3.zero;
                        Rock.transform.position = location;
                    }

                    float pullDistance = Vector3.Distance(SlingshotMiddleVector, Rock.transform.position);
                    ShowTrajectory(pullDistance);

                }
                else
                {
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out Vector2 canvasMousePosition);
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,  Cancel_Area.transform.position, canvas.worldCamera, out Vector2 canvasCancelPosition);
                    //Debug.Log("Mouse position in Canvas: " + canvasMousePosition);
                    float Canceldistance = Vector2.Distance(canvasMousePosition, canvasCancelPosition);

                    // 在這裡，distance 就是滑鼠與UI元素之間的距離
                    /*Debug.Log("Distance to UI Element: " + distance);
                    Vector3 location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    location.z = 0;
                    float CancelDistance = Vector3.Distance(location, Cancel_Area.transform.position);
                    Debug.Log(CancelDistance);*/
                    if(Canceldistance<42)
                    {
                        slingshotState = SlingshotState.do_nothing;
                        Cancel_Area_UI.SetActive(false);
                        break;
                    }
                    SetTrajectoryActive(false);
                    float distance = Vector3.Distance(SlingshotMiddleVector, Rock.transform.position);
                    if (distance > 0.5)
                    {
                        SetSlingshotRubbersActive(false);
                        slingshotState = SlingshotState.Flying;
                        ThrowRock(distance);
                        Rock=null;
                        ButtonFunction.currentEnergy-=energy;
                        CharacterManage.CharacterIdInCd=CharacterIdInCd_shoot;
                    }
                    Cancel_Area_UI.SetActive(false);
                }
                break;
        }       
    }

    public void cancel_shoot()
    {
        slingshotState = SlingshotState.do_nothing;
        
    }
    void ShowTrajectory(float distance)
    {
        SetTrajectoryActive(true);
        Vector3 diff = SlingshotMiddleVector - Rock.transform.position;
        int segmentCount = 20;
        Vector2[] segments = new Vector2[segmentCount];
        segments[0] = Rock.transform.position;
        Vector2 segVelocity = new Vector2(diff.x , diff.y)*ThrowSpeed*distance;
        for(int i=1; i < segmentCount ; i++)
        {
            float timeCurve = (i* Time.fixedDeltaTime*5);
            segments[i] = segments[0] + segVelocity*timeCurve + 0.5f*Physics2D.gravity * Mathf.Pow(timeCurve,2);
        }
        Trajectory.positionCount = segmentCount;
        for(int j=0 ; j<segmentCount ; j++)
        {
            Trajectory.SetPosition(j, segments[j]);
        }
    }
    void ThrowRock(float distance)
    {
        Vector3 velocity = SlingshotMiddleVector - Rock.transform.position;
        Rock.GetComponent<Rock>().OnThrow();
        Rock.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x,velocity.y)*ThrowSpeed*distance;
    }
}
