using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private int spinSpeed=50;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(spinSpeed*Time.deltaTime,0,0);
        
    }
      private void OnTriggerEnter(Collider other){
        if(other.tag=="Player"){
            // Collect Coin
           GameManager.Instance.increaseCoin();
           gameObject.SetActive(false);
        }
   }
}

