using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganizedPlayerMovementScript : MonoBehaviour
{

    // Touch Booleans



    // PLAYER COMPONENTS
    private Animator animator;
    private CharacterController controller;


    // PLAYER VARIABLES
    public bool dad;
    [SerializeField] float LANE_CHANGE_SPEED = 20f;
    public Vector3 velocity;

    // MOVEMENT VARIABLES
    private Vector2 distance;
    public float[] xpos; // LANE x values array that player gonna move
    int xposIndex=1; // the index of the xpos array tracker 
    private Vector2 startPos; // start position of touch
    private Vector2 curPos; // current position of touch
    private Vector2 endPos; // end position of touch
    [SerializeField] float swipeRange = 50f;
    [SerializeField] float tapRange =10f;
    [SerializeField] bool stopTouch = false;



    // End Game Variables
    [SerializeField] MoveComponent moveComponent;
    [SerializeField] ManagerLevel managerLevel;

















    // Start is called before the first frame update
    private void Start(){
        dad=false;

        // initialization of Player components
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        controller.detectCollisions=true;
    }

    // Update is called once per frame
    private void FixedUpdate(){
        controller.detectCollisions=true;
        if(isGrounded()==false){
            verticalVelocity-=gravity*Time.deltaTime;
        }

        // // GRAVITY if player not grounded
        // if(controller.isGrounded==false){
        //     velocity.y -= 9.8f * Time.deltaTime;
        // }
        // // Set the velocity to the current and target position difference
        //     velocity=Vector3.MoveTowards(transform.position,
        // new Vector3(xpos[xposIndex],
        // velocity.y,0),LANE_CHANGE_SPEED*Time.deltaTime);





        //-------------------- TOUCH CONTROLS-------------------------
        // Gets the start position of the touch
        if(Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Began){
            startPos=Input.GetTouch(0).position;            
        }
        // Checks the user contiune to touch
        if (Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Moved){
            curPos= Input.GetTouch(0).position; // get the current position of touch
            distance = curPos-startPos; // Touch length 
            // if the touch is not finished
            if(!stopTouch){
                // LEFT SWIPE
                if(distance.x<-swipeRange){
                    // move left
                    
                    MoveLane(false);



                    // stop the touch
                    stopTouch=true;
                }
                // RIGHT SWIPE
                if (distance.x>swipeRange){
                    
                    // move right
                    MoveLane(true);
                    // stop the touch
                    stopTouch=true;    
                }
                // UP SWIPE
                if(isGrounded()){ // grounded 
                    verticalVelocity=-0.1f;
                    animator.SetBool("Grounded",isGrounded());
                    
                    if (distance.y>swipeRange)
                    {
                        // Jump
                        verticalVelocity=jumpforce;
                        TriggerAnimation("Jump");
                        
                        stopTouch=true;
                        
                    }
                    // DOWN SWIPE
                    else if (distance.y<-swipeRange){
                            
                            // Slide
                            StartCoroutine(slide());
                            stopTouch=true;
                            
                        }
                        
                }
                // fast land
                if (distance.y<-swipeRange)
                {
                     // reverse jump apply negative vertical velocity
                    verticalVelocity=-jumpforce;
                    stopTouch=true;
                            
                            
                }
                
                
            }             
        }
        // Touch Ending check
        if(Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Ended){
            stopTouch = false;
            endPos=Input.GetTouch(0).position;
            Vector2 dis = endPos-startPos;
            // TAP Checking
            if (Mathf.Abs(dis.x)<tapRange && Mathf.Abs(dis.y)<tapRange)
            {
                
                // TAP
                
            }

        }


        //-------------------------------- Testing movement-------------------------------------------------

        // Calculate the target position
        Vector3 target = Vector3.forward;
        if (desiredLane ==0)
        {
            target+= Vector3.left * LANE_DISTANCE;            
        }else if (desiredLane ==2){
            target+= Vector3.right*LANE_DISTANCE;
        }

        Vector3 moveVector = Vector3.zero;
        
        moveVector.x =(target-transform.position).normalized.x*LANE_CHANGE_SPEED;
        moveVector.y=verticalVelocity;
        moveVector.z=0;
        // MOVE PLAYER
        controller.Move(moveVector*Time.deltaTime);

        








    }


    // Plays the animation due to triggered conection due to paramater
    private void TriggerAnimation(string trigger){
        foreach(AnimatorControllerParameter t in animator.parameters){
            // if the trigger is already activated reset it 
            if(t.type == AnimatorControllerParameterType.Trigger){
                animator.ResetTrigger(t.name);
            }
            // active the trigger
            animator.SetTrigger(trigger);
        }
    }




    //-------------------------- NEW MOVEMENT ---------------------------------

    // VARIABLES 
    private float jumpforce=5.0f;
    private float gravity =14.0f;
    private float verticalVelocity;
    private int LANE_DISTANCE =1;
    private int desiredLane =1; //  0 => Left 1=> Midddle 2 => Right



    // METHODS
    private void MoveLane(bool goingRight){
        // changes the desired lane value depend on the parameter goingRight
        desiredLane += (goingRight ? 1:-1);
        desiredLane = Mathf.Clamp(desiredLane,0,2);
    }

    private bool isGrounded(){
        Ray groundRay = new Ray(new Vector3(controller.bounds.center.x,
        (controller.bounds.center.y-controller.bounds.extents.y)+0.2f,
        controller.bounds.center.z),Vector3.down);
        Debug.DrawRay(groundRay.origin,groundRay.direction,Color.green);
        return (Physics.Raycast(groundRay,0.2f+0.1f));
        

    }

    private void StartSliding(){
        
        controller.height/=2;
        controller.center = new Vector3(controller.center.x,
        controller.center.y/2,
        controller.center.z);
        animator.SetBool("Sliding",true);
        
    }

    private void StopSliding(){
        controller.height*=2;
        controller.center = new Vector3(controller.center.x,
        controller.center.y*2,
        controller.center.z);
        animator.SetBool("Sliding",false);
       

    }

     public void OnTriggerEnter(Collider collision){
         if (collision.tag=="Obstacle")
         {
             MoveComponent[] grounds =FindObjectsOfType(typeof(MoveComponent)) as MoveComponent[];
             ManagerLevel[] segments =FindObjectsOfType(typeof(ManagerLevel)) as ManagerLevel[];
            foreach(MoveComponent mc in grounds){
                Destroy(mc);
            }
            foreach(ManagerLevel s in segments){
                Destroy(s);
            }
        }
             StartCoroutine(deathSequence());
              
              
             
    }

     
     private IEnumerator slide(){
         
         StartSliding();
         yield return new WaitForSeconds(1.0f);
         StopSliding();
         

    }

   

    public IEnumerator deathSequence(){
        dad=true;
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name!="Dying Backwards")
        {
            animator.enabled=false;
            
        }
        animator.enabled=true;
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length+animator.GetCurrentAnimatorStateInfo(0).normalizedTime+2f);
        
        
        
    }



















}
