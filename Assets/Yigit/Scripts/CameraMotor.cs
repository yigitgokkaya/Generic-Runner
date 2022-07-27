using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{

    public Transform lookAt;  // player

    public bool IsMoving {set; get;}

    public Vector3 offset = new Vector3 (0, 5.0f , -10.0f);
    public Vector3 rotation = new Vector3 (35,0,0); 


    // Update is called once per frame
    void LateUpdate()
    {
        if(!IsMoving)
            return;

        Vector3  desiredposition = lookAt.position + offset;
        desiredposition.x = 0;
        transform.position = Vector3.Lerp(transform.position,desiredposition,0.1f);
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(rotation),0.1f);    
        
    }
}
