using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove2 : MonoBehaviour
{

    private  const int LANE_DISTANCE =1;
    private CharacterController controller;
    private float jumpForce=14.0f;
    private float gravity =24.0f;
    private float verticalVelocity;
    private float speed=7.0f; // gerek olmayabilir


    private int laneIndex=1; //0 left 1 middle 2 right
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        

        // Inputs
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLane(false);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveLane(true);
        }
        Vector3 targetPosition = Vector3.zero;
        if(laneIndex==0){
            targetPosition+= Vector3.left*LANE_DISTANCE;
        }else if(laneIndex==2){
            targetPosition+= Vector3.right*LANE_DISTANCE;
        }
        Vector3 moveVector = Vector3.zero;
        moveVector.x =(targetPosition-transform.position).normalized.x*speed;
        


        // VERTICAL
        Debug.Log(isGrounded());
        if(isGrounded()){
            verticalVelocity=0f;
            moveVector.y =verticalVelocity;
            if(Input.GetKeyDown(KeyCode.Space)){
                Debug.Log("LAN OGLUM ZIPLA");
                verticalVelocity=jumpForce;
                
                
            }
        }
        else{
            verticalVelocity-=(gravity*Time.deltaTime);        
            // FAST FALL 
            if(Input.GetKeyDown(KeyCode.Space)){
                verticalVelocity=-jumpForce;
                
                
            }
        }
        moveVector.y =verticalVelocity;
        
        moveVector.z=0;
        controller.Move(moveVector*Time.deltaTime);
}

    private void MoveLane(bool right){
        laneIndex+=(right)? 1 : -1;
        laneIndex=Mathf.Clamp(laneIndex,0,2);
    }
    private bool isGrounded(){
        Ray grounded = new Ray(
            new Vector3(
                controller.bounds.center.x,
                (controller.bounds.center.y-controller.bounds.extents.y)+0.2f,
                controller.bounds.center.z
            ),
            Vector3.down
        );
        Debug.DrawRay(grounded.origin,grounded.direction,Color.blue,1.0f);
        return (Physics.Raycast(grounded,0.2f+0.1f));
        

    }
}
