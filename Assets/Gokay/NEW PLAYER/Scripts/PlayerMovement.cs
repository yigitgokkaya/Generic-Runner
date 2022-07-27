using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool dead;
    private Animator animator;
    private  const int LANE_DISTANCE =1;
    private TouchInputs myinputs;
    private CharacterController controller;
    private float jumpForce=5.0f;
    private float gravity =12.0f;
    private float verticalVelocity;
    private float speed=7.0f; // gerek olmayabilir

    private bool isSliding=false;
    private bool isJumping=false;

    


    private int laneIndex=1; //0 left 1 middle 2 right

    [SerializeField] private AudioClip[] clips;
    private AudioSource source;

    void Awake(){
        
    }


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        controller.detectCollisions=true;
        dead=false;
        source=GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        myinputs = GetComponent<TouchInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.isGameStarted){
            // Inputs
            if(TouchInputs.Instance.SwipeLeft)
            {
                MoveLane(false);
            }
            if(TouchInputs.Instance.SwipeRight)
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
            if(isGrounded()==false){
                moveVector.x =(targetPosition-transform.position).x*speed*4f;
            }else{
                moveVector.x =(targetPosition-transform.position).x*speed*2f;
            }
        
        
        


        // VERTICAL
            bool grounded = isGrounded();
            animator.SetBool("Grounded",grounded);
            if(grounded){
                controller.center = new Vector3(controller.center.x,0.5f,controller.center.z);
                verticalVelocity=-0.1f;
                if(TouchInputs.Instance.SwipeUp){
                    isSliding=false;
                    source.clip=clips[0];
                    animator.SetTrigger("Jump");
                    source.Play();
                    controller.center = new Vector3(controller.center.x,0.9f,controller.center.z);
                    verticalVelocity=jumpForce;
            }
                else if(TouchInputs.Instance.SwipeDown){
                    // SLide
                    if(isSliding==false){
                        StartSliding();
                        Invoke("StopSliding",0.5f);
                    }
                
                }
            }
            else{
                verticalVelocity-=(gravity*Time.deltaTime);        
            // FAST FALL 
                if(TouchInputs.Instance.SwipeDown){
                    animator.SetTrigger("FastLand");
                    verticalVelocity=-jumpForce;
                
                
                }
            }
             moveVector.y =verticalVelocity;
            moveVector.z=0;
            controller.Move(moveVector*Time.deltaTime);
    }
}


    private void StartSliding(){
        isSliding=true;
        source.clip=clips[1];
        source.Play();
        controller.height=controller.height/2;
        controller.center = new Vector3(controller.center.x,
        0.3f,
        controller.center.z);
        animator.SetBool("Sliding",true);

    }
    private void StopSliding(){
        animator.SetBool("Sliding",false);
        controller.height=controller.height*2;
        controller.center = new Vector3(controller.center.x,
        0.5f,
        controller.center.z);
        isSliding=false;
        
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

    // public void OnControllerColliderHit(ControllerColliderHit hit){
    //     Debug.Log(hit.gameObject.tag);
    //     if(hit.gameObject.tag=="Obstacle"){
    //         Crash();
    //     }
    // }
    // private void OnControllerColliderHit(ControllerColliderHit hit){
    //     Debug.Log(hit.gameObject.tag);
    //     switch(hit.gameObject.tag){
    //             case "Obstacle":
    //                 Debug.Log("If in içine girdim");
    //                 MoveComponent[] grounds =FindObjectsOfType(typeof(MoveComponent)) as MoveComponent[];
    //                 ManagerLevel[] segments =FindObjectsOfType(typeof(ManagerLevel)) as ManagerLevel[];
    //                 foreach(MoveComponent mc in grounds){
    //                     Destroy(mc);
    //                 }
    //                 foreach(ManagerLevel s in segments){
    //                     Destroy(s);
    //                 }
    //                 animator.SetTrigger(" Death");
    //                 break;
    //     }
        
        
    // }
    public void OnTriggerEnter(Collider collision){
         if (collision.tag=="Obstacle")
         {
             source.clip=clips[2];
             source.Play();
             controller.enabled=false;
             GameManager.Instance.setGameStarted(false);
             GameManager.Instance.scoreAnalytics();
             MoveComponent[] grounds =FindObjectsOfType(typeof(MoveComponent)) as MoveComponent[];
             ManagerLevel[] segments =FindObjectsOfType(typeof(ManagerLevel)) as ManagerLevel[];
            foreach(MoveComponent mc in grounds){
                Destroy(mc);
            }
            foreach(ManagerLevel s in segments){
                Destroy(s);
            }
            transform.position = new Vector3(transform.position.x,0,transform.position.z);
            animator.SetTrigger(" Death");
            dead=true;
            myinputs.enabled=false;
            

        }
    }
    // public IEnumerator deathSequence(){
    //     //dad=true;
    //     if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name!="Standing React Death Backward")
    //     {
    //         animator.enabled=false;
            
    //     }
    //     animator.enabled=true;
    //     Debug.Log("Animasyonu Oynat");
    //     animator.SetTrigger(" Death");
    //     yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length+animator.GetCurrentAnimatorStateInfo(0).normalizedTime+2f);
        
        
        
    // }
}
