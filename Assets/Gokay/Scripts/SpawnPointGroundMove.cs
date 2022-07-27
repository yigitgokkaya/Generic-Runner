using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointGroundMove : MonoBehaviour
{


  [SerializeField] private SpawnSegment segmentSpawner;
   [SerializeField] private float speed = 25f;

  [SerializeField] private float groundobjectDistance=-40f;

  [SerializeField] private float grounddespawnDistance = -210f;


  [SerializeField] private float speedScale=1f;

 

  public bool canSpawnGround = true;
  void Start()
  {
    //speed=GameManager.Instance.GetSpeed();
    
  }
  public void setSpeed(float s){
    speed=s;

  }

  public void spawnSeg(){
    segmentSpawner.SpawnPoints();
  }
  public void destroySeg(){
    segmentSpawner.DeSpawnPoints();
  }


  void Update()
  {
    
    if(!GameManager.isGameStarted)
      {
        return;
      }
      //speed=GameManager.Instance.GetSpeed();
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
