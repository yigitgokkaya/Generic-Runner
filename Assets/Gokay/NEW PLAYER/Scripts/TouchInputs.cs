using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputs : MonoBehaviour
{

    public static TouchInputs Instance {set; get;}

    private bool tap, swipeLeft,swipeRight,swipeUp,swipeDown;
    private Vector2 swipeDelta,startTouch;
    private const float DEAD_ZONE=50;

    public bool Tap {get{return tap; } }
    public Vector2 SwipeDelta{ get{return swipeDelta; } }
    public bool SwipeLeft { get {return swipeLeft; } }
    public bool SwipeRight { get {return swipeRight; } }
    public bool SwipeUp { get {return swipeUp; } }
    public bool SwipeDown { get {return swipeDown; } }

    private void Awake(){
        Instance = this;
    }

    private void Update(){
        // Reset the inputs
        tap=swipeDown=swipeLeft=swipeRight=swipeUp=false;
        
        // #region Standalone Inputs 
        // if(Input.GetMouseButtonDown(0)){
        //     tap=true;
        //     startTouch=Input.mousePosition;
        // }else if(Input.GetMouseButtonUp(0)){
        //     startTouch=swipeDelta=Vector2.zero;
        // }
        // #endregion

        #region Mobile Inputs 
        if(Input.touchCount>0){
            if(Input.GetTouch(0).phase == TouchPhase.Began){
                tap=true;
                startTouch=Input.GetTouch(0).position;
            }
            else if(Input.touches[0].phase== TouchPhase.Ended || Input.touches[0].phase== TouchPhase.Canceled){
                startTouch=swipeDelta=Vector2.zero;
            }

        }
        #endregion

        // Calculate Distance
        swipeDelta= Vector2.zero;
        if(startTouch!=Vector2.zero){
            if(Input.touches.Length!=0){
                swipeDelta=Input.touches[0].position-startTouch;
            } else if(Input.GetMouseButton(0)){
                 swipeDelta=(Vector2)Input.mousePosition-startTouch;
             }
        }
        

        if(swipeDelta.magnitude>DEAD_ZONE){
            //Confirmed swpie
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if(Mathf.Abs(y)<Mathf.Abs(x)){
                // Left or Right
                if(x<0){
                    swipeLeft=true;
                }else{
                    swipeRight=true;
                }
            }else{
                // Up or Down
                if(y<0){
                    swipeDown=true;
                }else{
                    swipeUp=true;
                }
                
            }

            startTouch=swipeDelta=Vector2.zero;

        }
    }



}
