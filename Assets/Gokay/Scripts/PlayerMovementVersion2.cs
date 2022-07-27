using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public  enum Dircetion{
        RIGHT,
        LEFT,
        UP,
        DOWN
    };





public class PlayerMovementVersion2 : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] float LANE_CHANGE_SPEED=60f;
    public float[] xpos;
    int xposIndex=1;


    public Vector3 velocity;

    private CharacterController characterController;
    

    private bool switchingLeft=false;
    private bool swithingRight = false;


     //public Text outputText; // This is the text in the canvas will be display swipe direction

    private Vector2 startPos; // start position of touch
    private Vector2 curPos; // current position of touch
    private Vector2 endPos; // ending position of touch


    public float swipeRange =50f;
    public float tapRange =10f;

    public bool stopTouch=false;
    

    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    private Vector3 playerVelocity;




    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController= GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
            if(characterController.isGrounded==false){
                velocity.y-=9.8f*Time.deltaTime;
            }
        

        velocity=Vector3.MoveTowards(transform.position,new Vector3(xpos[xposIndex],velocity.y,0),LANE_CHANGE_SPEED*Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(xpos[xposIndex],0, transform.position.z), Time.deltaTime * 50f);

        if(Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Began){
            startPos= Input.GetTouch(0).position;
        }
        // Cheks the user contiune to touch
        if(Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Moved){
            curPos=Input.GetTouch(0).position;
            Vector2 distance = curPos-startPos;
            if(!stopTouch){
                if(distance.x<-swipeRange){
                    MoveLeft();
                     //outputText.text="LEFT";      
                    if(xpos[0]!=transform.position.x){

                        AnimTrigger("SideJump");
                        
                    }
                    
                    stopTouch=true;
                }
                if(distance.x>swipeRange){
                    MoveRight();
                    //outputText.text="RIGHT";
                     if(!(xpos[2]==transform.position.x)){
                        AnimTrigger("SideJump");
                        
                    }
                    
                    stopTouch=true;
                    
                }
                if(characterController.isGrounded==true){
                    if(distance.y>swipeRange){
                        //outputText.text="UP";
                        AnimTrigger("Jump");
                        velocity.y=5000f*Time.deltaTime;
                        stopTouch=true;
                        }
                }
                if(distance.y<-swipeRange){
                    //outputText.text="DOWN";
                    StartCoroutine(Slide());
                    stopTouch=true;
                }
            }
        }
        if(Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Ended){
            stopTouch=false;
            endPos=Input.GetTouch(0).position;
            Vector2 dis = endPos-startPos;
            if(Mathf.Abs(dis.x)<tapRange && Mathf.Abs(dis.y)<tapRange){
                // outputText.text="TAP";

            }
        }
       MoveToPoint(velocity);
    }

    private IEnumerator Slide()
    {
        characterController.height=1;
        characterController.center= new Vector3(0,0.3f,0);
        animator.SetBool("Slide",true);
        yield return new WaitForSeconds(1.3f);
        animator.SetBool("Slide",false);
        characterController.height=2;
        characterController.center= new Vector3(0,1.22f,0);
        
    }

    void AnimTrigger(string triggerName)
 {
     foreach(AnimatorControllerParameter p in animator.parameters)
         if (p.type == AnimatorControllerParameterType.Trigger)
             animator.ResetTrigger(p.name);
     animator.SetTrigger(triggerName);
  }

void MoveToPoint(Vector3 targetPosition)
     {
         targetPosition.z=transform.position.z;
        if (targetPosition == transform.position)
            return;
 
        Vector3 movDiff = targetPosition - transform.position;

        Vector3 movDir = movDiff.normalized * 15f * Time.deltaTime;
        
        if(movDir.sqrMagnitude < movDiff.sqrMagnitude)
        {

            
            
            characterController.Move((movDir)*Time.deltaTime*LANE_CHANGE_SPEED);
            velocity.x=0;
        }
        else
        {
            
           characterController.Move((movDiff)*Time.deltaTime*LANE_CHANGE_SPEED);
           velocity.x=0;
        }
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
        
}

