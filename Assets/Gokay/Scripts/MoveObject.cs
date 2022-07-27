using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
  [SerializeField] private float speed = 5f;

  [SerializeField] private float objectDistance=-40f;

  [SerializeField] private float despawnDistance = -110f; 

  public bool canSpawnGround = true;

//   public Rigidbody rb;

//   void start()
//   {
//       rb = GetComponent<Rigidbody>();
//   }

  void Update()
  {
      transform.position += -transform.forward * speed * Time.deltaTime;

      if(transform.position.z <= objectDistance && transform.tag == "Ground" && canSpawnGround)
      {
          ObjectSpawnerGokay.instance.SpawnGround();
          canSpawnGround = false;

      }  
      if(transform.position.z <= despawnDistance)
      { 
           canSpawnGround = true;
           gameObject.SetActive(false);
      }


  }

}
