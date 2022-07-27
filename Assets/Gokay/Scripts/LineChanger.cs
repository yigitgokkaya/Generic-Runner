using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineChanger : MonoBehaviour
{
    CharacterController controller;


    public float[] xpos;
    [SerializeField] float LANE_CHANGE_SPEED=60f;

    public Vector3 velocity;
    private float vSpeed=0f;
    private float gravity=-9.8f;

    private Vector3 direction;

    

   // [SerializeField] Rigidbody playerRb;
   public  enum Dircetion{
        RIGHT,
        LEFT,
        UP,
        DOWN
    };


    private static int desiredLane = 1;//0:left, 1:middle, 2:right
    public static float laneDistance = 2.0f;//The distance between tow lanes
    

    int xposIndex=1;


    void Start(){
        controller = GetComponent<CharacterController>();
    }
    void Update(){
         
         //velocity=Vector3.MoveTowards(transform.position,new Vector3(xpos[xposIndex],0, transform.position.z), Time.deltaTime * LANE_CHANGE_SPEED);
         playerMovement();
         transform.position = Vector3.MoveTowards(transform.position, new Vector3(xpos[xposIndex],0, transform.position.z), Time.deltaTime * LANE_CHANGE_SPEED);
        
    }

    void applyGravity(){
         //REeset the MoveVector
         //velocity = Vector3.zero;
         
             //Add our gravity Vecotr
             velocity += Physics.gravity;
         
 
         //Apply our move Vector , remeber to multiply by Time.delta
         controller.Move(velocity * Time.deltaTime);
 
         
}
    
//     
  public void movePos(Dircetion d){
      Debug.Log("Xpos =>"+xposIndex);
      if (d==Dircetion.RIGHT)
        {
            MoveRight();
        }
        if (d==Dircetion.LEFT)
        {
            MoveLeft();
        }
        
        if(controller.isGrounded){
            Debug.Log("is grounded true");
            if(d==Dircetion.UP){
                vSpeed=10f;
            }
        }

        Vector3 targetPosition = new Vector3(0,transform.position.y,transform.position.z);
        if (xposIndex == 0)
            targetPosition.x += xpos[xposIndex];
        else if (xposIndex == 2)
            targetPosition.x += xpos[xposIndex];

        direction = velocity;
        Debug.Log(direction);
        MoveToPoint(direction);
        
    }


    void MoveLeft () {
        

        xposIndex--;
        if (xposIndex < 0)
            xposIndex = 0;

    }

    void MoveRight () {
       

        xposIndex++;
        if (xposIndex > xpos.Length - 1)
            xposIndex = xpos.Length - 1;

    }

   public float[] getXpos(){
       return xpos;
   }
   void MoveToPoint(Vector3 targetPosition)
     {
        if (targetPosition == transform.position)
            return;
           
        Debug.Log("Transform Position => " +transform.position+"  Target Position => "+targetPosition);
 
        Vector3 movDiff = targetPosition - transform.position;
        Debug.Log("MoveDiff => "+ movDiff);
        Debug.Log("MoveDiff Normalized => "+ movDiff.normalized);
        Vector3 movDir = movDiff.normalized * LANE_CHANGE_SPEED * Time.deltaTime;
        
        if(movDir.sqrMagnitude < movDiff.sqrMagnitude)
        {

            Debug.Log("MoveDir => "+ movDir);
            
            controller.Move((movDir)*Time.deltaTime);
        }
        else
        {
            
           controller.Move((movDiff)*Time.deltaTime);
        }
     }



     //------------------------------------------------- TEST METHODS-----------------------------------------

     void playerMovement(){
         Vector3 dir = new Vector3(xpos[xposIndex],0,transform.position.z);
         velocity = dir;
         gravity = -9.8f*Time.deltaTime;

         if(controller.isGrounded){

         }else{
             velocity.y+=gravity;
         }

         controller.Move(MoveToPointReturn(velocity)*Time.deltaTime);
     }

      Vector3 MoveToPointReturn(Vector3 targetPosition)
     {
        if (targetPosition == transform.position)
            return Vector3.zero;
           
        Debug.Log("Transform Position => " +transform.position+"  Target Position => "+targetPosition);
 
        Vector3 movDiff = targetPosition - transform.position;
        Debug.Log("MoveDiff => "+ movDiff);
        Debug.Log("MoveDiff Normalized => "+ movDiff.normalized);
        Vector3 movDir = movDiff.normalized * LANE_CHANGE_SPEED * Time.deltaTime;
        
        if(movDir.sqrMagnitude < movDiff.sqrMagnitude)
        {

            Debug.Log("MoveDir => "+ movDir);
            
            return movDir;
        }
        else
        {
            
           return movDiff;
        }
     }














}




// OLD MOVE FUNCTİON

//public  void move(Dircetion d){


//         if (d==Dircetion.RIGHT)
//         {
//             desiredLane++;
//             if (desiredLane == 3)
//                 desiredLane = 2;
//         }
//         if (d==Dircetion.LEFT)
//         {
//             desiredLane--;
//             if (desiredLane == -1)
//                 desiredLane = 0;
//         }

        
//         Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
//         if (desiredLane == 0)
//             targetPosition += Vector3.left * laneDistance;
//         else if (desiredLane == 2)
//             targetPosition += Vector3.right * laneDistance;

//         if (transform.position != targetPosition)
//         {
//             Vector3 diff = targetPosition - transform.position;
//             Vector3 moveDir = diff.normalized * 5f * Time.deltaTime;
//             if (moveDir.sqrMagnitude < diff.magnitude){
//                 controller.Move(moveDir);

//             }else{
//                 controller.Move(diff);

//             }


//    }
//   }

