using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTest : MonoBehaviour
{
    public Swipe swipeControls;
    public Transform player;
    private Vector3 desiredPosition;

 
    // Update is called once per frame
    void Update()
    {
        if(swipeControls.swipeLeft)
        {
            desiredPosition += Vector3.left;
        }
         if(swipeControls.swipeRight)
        {
            desiredPosition += Vector3.right;
        }
         if(swipeControls.swipeUp)
        {
            desiredPosition += Vector3.forward;
        }
         if(swipeControls.swipeDown)
         {
             desiredPosition += Vector3.back;
         }
         
         player.transform.position = Vector3.MoveTowards(player.transform.position , desiredPosition , 3f * Time.deltaTime);
        
    }
}
