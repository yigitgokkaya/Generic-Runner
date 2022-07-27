using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ObjectPoolerGokay : MonoBehaviour
{
    
    [System.Serializable]
    public class PoolGokay
    {
        public string type;
        public GameObject prefab;
        public int size;
    }

    [SerializeField] Text text;
    public static ObjectPoolerGokay instance;
    
  
      
    private void Awake(){
        instance = this;
    }
  

    public List <PoolGokay> pools;
    public Dictionary<string,Queue<GameObject>> poolDictionary;
    public GameObject objectToSpawn;



    void Start()
    {
        Debug.Log("Awake methodu çalisti");
        


        // Start Method
      poolDictionary = new Dictionary<string,Queue<GameObject>>();

    foreach (PoolGokay pool in pools)
    {
        Queue<GameObject> objectPool = new Queue<GameObject>();
        for (int i = 0; i < pool.size; i++)
        {
            GameObject obj = Instantiate(pool.prefab);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }

        poolDictionary.Add(pool.type,objectPool);
        print();

        
        
    }
}
 void print(){
      foreach( var kvp in poolDictionary ){
          Debug.Log("Key => "+kvp.Key +" Value => "+kvp.Value);
      }
 }
///  patlama

public GameObject spawnObjects(string type,Vector3 direction, Quaternion rotation){
    // if(!poolDictionary.ContainsKey(type)){
    //     Debug.Log("Type does not exsist in pool");
    //     return null;
    // }
    text.text="Type => "+ type +" Direction => "+ direction + " Rotation => "+ rotation;
     objectToSpawn = poolDictionary[type].Dequeue();
     Debug.Log(objectToSpawn.ToString());
     objectToSpawn.SetActive(true);
     objectToSpawn.transform.position= direction;
     objectToSpawn.transform.rotation = rotation;

     poolDictionary[type].Enqueue(objectToSpawn);

    return objectToSpawn;


    }
}



