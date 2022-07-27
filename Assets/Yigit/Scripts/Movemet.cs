using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movemet : MonoBehaviour
{
  public bool left;
  public bool right;  
  public Rigidbody rb;

  public float speed = 5.0f;
  public float jump = 5.0f;
  


  void Start()
  {
    rb = GetComponent<Rigidbody>();
  }

    // Update is called once per frame
    void FixedUpdate()
    {
      Vector3 move_right = new Vector3(7.96f,transform.position.y,transform.position.z);
      Vector3 move_left = new Vector3(-0.7f,transform.position.y,transform.position.z);
      transform.Translate (0,0,speed * Time.deltaTime);
        //rb.AddForce(0,0,3000 * Time.deltaTime);

        if(Input.touchCount > 0)
        {
          Touch finger = Input.GetTouch(0);
          if(finger.deltaPosition.x > 50.0f)
          {
            right = true;
            left = false;

          } 
            if(finger.deltaPosition.x < -50.0f)
          {
            right = false;
            left = true;
            
      
          } 
          if(right == true)
          {
            transform.position = Vector3.Lerp(transform.position,move_right,5*Time.deltaTime);
          }
          if(left == true)
          {
            transform.position = Vector3.Lerp(transform.position,move_left,5*Time.deltaTime);
          }
        }
    }
}
