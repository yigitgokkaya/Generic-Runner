using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnerGokay : MonoBehaviour
{

    private bool spawningObject = false;

    public static ObjectSpawnerGokay instance;
    [SerializeField] public float groundSpawnDistance = 50f;

    private void Awake(){
        instance = this;
    }

    public void SpawnGround(){
            ObjectPoolerGokay.instance.spawnObjects("Ground",new Vector3(0,0,groundSpawnDistance),Quaternion.identity);
    }
   
}
