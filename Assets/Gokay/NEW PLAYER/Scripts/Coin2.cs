using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin2 : MonoBehaviour
{
   private void OnTriggerEnter(Collider other){
       if(other.tag=="Player"){
           // Collect Coin
           GameManager2.Instance.collect();
           Destroy(gameObject);
       }
   }
}
