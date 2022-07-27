using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMov : MonoBehaviour
{

    [SerializeField] Animator animator;


     public Text outputText; // This is the text in the canvas will be display swipe direction

    private Vector2 startPos; // start position of touch
    private Vector2 curPos; // current position of touch
    private Vector2 endPos; // ending position of touch


    public float swipeRange =50f;
    public float tapRange =10f;

    public bool stopTouch=false;





    


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Swipe();
    }

    public void Swipe(){
        // Checks the user started touch
        if(Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Began){
            startPos= Input.GetTouch(0).position;
        }
        // Cheks the user contiune to touch
        if(Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Moved){
            curPos=Input.GetTouch(0).position;
            Vector2 distance = curPos-startPos;
            if(!stopTouch){
                if(distance.x<-swipeRange){
                     outputText.text="LEFT";
                    AnimTrigger("SideJump");
                    
                    stopTouch=true;
                }
                if(distance.x>swipeRange){
                    outputText.text="RIGHT";
                    AnimTrigger("SideJump");
                    stopTouch=true;
                    
                }
                if(distance.y>swipeRange){
                    outputText.text="UP";
                    AnimTrigger("Jump");
                    stopTouch=true;
                    
                }
                if(distance.y<-swipeRange){
                    outputText.text="DOWN";
                    stopTouch=true;
                }
            }
        }
        if(Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Ended){
            stopTouch=false;
            endPos=Input.GetTouch(0).position;
            Vector2 dis = endPos-startPos;
            if(Mathf.Abs(dis.x)<tapRange && Mathf.Abs(dis.y)<tapRange){
                 outputText.text="TAP";

            }
        }

    }


  
 void AnimTrigger(string triggerName)
 {
     foreach(AnimatorControllerParameter p in animator.parameters)
         if (p.type == AnimatorControllerParameterType.Trigger)
             animator.ResetTrigger(p.name);
     animator.SetTrigger(triggerName);
 }     



}







