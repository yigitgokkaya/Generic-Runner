using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
  [SerializeField] private float speed = 25f;

  [SerializeField] private float groundobjectDistance=-40f;

  [SerializeField] private float grounddespawnDistance = -210f;


  [SerializeField] private float speedScale=1f;

 

  public bool canSpawnGround = true;
  void Start()
  {
    
  }


  void Update()
  {
    if(!GameManager.isGameStarted)
      {
        return;
      }
       transform.position += -transform.forward * speed * Time.deltaTime*speedScale;

      if(transform.position.z <= groundobjectDistance && transform.tag == "ground" && canSpawnGround)
      {
          ObjectSpawner.instance.SpawnGround();
          canSpawnGround = false;

      }  
      if(transform.position.z <= grounddespawnDistance)
      { 
           canSpawnGround = true;
           gameObject.SetActive(false);
      }
     
   
    

  }
}

 
