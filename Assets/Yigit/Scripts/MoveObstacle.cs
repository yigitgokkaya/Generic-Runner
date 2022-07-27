using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{

    [SerializeField] private float speed = 25f;
    [SerializeField] private float obstacleobjectDistance = -20f;

    [SerializeField] private float obstacledespawnDistance = -50f;

    public bool canSpawnObstacle = true;
    

    void Start()
    {
       if(!GameManager.isGameStarted)
      {
        return;
      }
        
    }

    
    void Update()
    {
      if(!GameManager.isGameStarted)
      {
        return;
      }
         transform.position += -transform.forward * speed * Time.deltaTime;
       if(transform.position.z <= obstacleobjectDistance && transform.tag == "obstacle" && canSpawnObstacle)
      {
          ObjectSpawner.instance.SpawnObstacle();
          canSpawnObstacle = false;

      }  
      if(transform.position.z <= obstacledespawnDistance)
      { 
           canSpawnObstacle = true;
           gameObject.SetActive(false);
      }
  }
        
    
}
