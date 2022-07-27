using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 public class ObjectSpawner : MonoBehaviour
 {
  // [System.Serializable]
  // public struct Spawnable
  // {
  //   public string type;

  //   public float weight;
      
  // }

  // [System.Serializable]

  // public struct SpawnSettings
  // {
  //     public string type;

  //     public float maxWait;

  //     public float minWait;

  //     public int maxObjects;
  // }
  //   private float totalWeight;

  public bool spawningObject = false; 
  public float spawnofset = 0f;

    // public List<Spawnable> obstacleSpawnables = new List<Spawnable>();

    // public List<SpawnSettings> spawnSettings = new List<SpawnSettings>();

  
  
  [SerializeField] public float groundSpawnDistance = 50f;
  [SerializeField] public float obstacleSpawnDistance= 20f;

  public static ObjectSpawner instance; 

  

  public void Awake()
  {
      instance = this;
        // totalWeight = 0;
        // foreach(Spawnable spawnable in obstacleSpawnables)
        // totalWeight += spawnable.weight; 

  }

  void Start()
  {
    
  }

  public void SpawnGround() 
  {
  
  GameObject go = ObjectPooler.instance.spawnFromPool("ground", new Vector3(0,0,groundSpawnDistance + spawnofset),Quaternion.identity);
  go.GetComponent<SpawnPointGroundMove>().spawnSeg();
  go.GetComponent<SpawnPointGroundMove>().destroySeg();
   
   
   
   }

  // public IEnumerator SpawnObstacle(string type,float time)
  // {
  //   yield return new WaitForSeconds(time);
  //   ObjectPooler.instance.spawnFromPool(type, new Vector3(Random.Range(-4.4f,4.4f),0.3f,-67.1f),Quaternion.identity);
  //   spawningObject = false;
  //   GameController.ObstacleCount++; 
  // }

    public void SpawnObstacle()
      {

      ObjectPooler.instance.spawnFromPool("obstacle", new Vector3(Random.Range(-4.25f,4.25f),0.51f,obstacleSpawnDistance),Quaternion.identity);

      }

  //private IEnumerator SpawnObstacle(string type, float time);

  // void Update()
  // {
  //   if(!spawningObject && GameController.ObstacleCount < spawnSettings[0].maxObjects)
  //   {
  //       spawningObject = true;
  //       float pick = Random.value * totalWeight;
  //       int chosenIndex = 0;
  //       float cumulativeWeight = obstacleSpawnables[0].weight;

  //       while(pick > cumulativeWeight &&  chosenIndex < obstacleSpawnables.Count - 1)
  //       {
  //         chosenIndex++;
  //         cumulativeWeight += obstacleSpawnables[chosenIndex].weight;
  //       }
  //       StartCoroutine(SpawnObstacle(obstacleSpawnables[chosenIndex].type,Random.Range(spawnSettings[0].minWait / GameController.DifficultyMultiplier,spawnSettings[0].maxWait /GameController.DifficultyMultiplier )));
  //   }

  // }


}
