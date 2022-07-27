using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
 {  public Transform target;

    public float smoothSpeed = 0.125f;

    public Vector3 offset;

    //public GameObject player;

     void Start()
     {
          //player.transform.position = new Vector3(0,0,-105.7f);
         // offset = transform.position - target.position;
      }
    

       void FixedUpdate()       
    {
        //  Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z+ target.position.z );
        //  transform.position = Vector3.Lerp(transform.position, newPosition, 10*Time.deltaTime); 



         Vector3 desiredPosition =  target.position + offset;
         Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
         transform.position = smoothedPosition;

         transform.LookAt(target);
        
    }
}
