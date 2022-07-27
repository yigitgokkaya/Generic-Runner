using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    public bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    public bool isDraging = false;
    public Vector2 startTouch, swipeDelta;

   /* public Vector2 swipeDelta {get{return swipeDelta; } }
    public bool swipeLeft{get{return swipeLeft; } }
    public bool swipeRigt{get{return swipeRight; } }
    public bool swipeUp{get{return swipeUp; } }
    public bool swipeDown{get{return swipeDown; } }
*/
     void Update()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        

        if(Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDraging = true;
            startTouch = Input.mousePosition; 
        } else if(Input.GetMouseButtonUp(0))
        {
            isDraging = false;
            Reset();

        }
        

        #region  Mobile Inputs
        if(Input.touches.Length > 0)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                isDraging = true;
                tap = true;
                startTouch = Input.touches[0].position;
            }else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                Reset();
            }

            //Did we cross the dead zone?
            if(swipeDelta.magnitude > 125 )
            {
            //Which direction
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if(Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Left or right
                if(x<0)
                {
                    swipeLeft = true;
                }else
                    swipeRight = true;

            }else

            {
                // Up or down
                 if(y<0)
                {
                    swipeDown = true;
                }else
                    swipeUp = true;

            }



                Reset();
            }
            
        }

        #endregion

        //calculate distance
        swipeDelta = Vector2.zero;
        if(isDraging)
        {
            if(Input.touches.Length > 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            else if(Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }
    }
    
      void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }
}
