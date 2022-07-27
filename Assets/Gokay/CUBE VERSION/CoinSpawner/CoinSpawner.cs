using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{

    public int max_coin=5;
    public float spawn_chance = 0.5f;

    public bool spawnALL=false;

    private GameObject[] coins;

    private void Awake(){
        coins= new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            coins[i]=transform.GetChild(i).gameObject;
        }
        OnDisable();
    }

    private void OnEnable(){
        if(Random.Range(0.0f,1.0f)>spawn_chance){
            return;
        }
        else if(spawnALL){
            for(int i = 0 ;i<max_coin;i++){
                coins[i].SetActive(true);
            }
        }
        else{
            int r = Random.Range(0,max_coin);
            for(int i =0;i<r;i++){
                coins[i].SetActive(true);
            }
        }
    }
     private void OnDisable(){
         foreach(GameObject go in coins){
             go.SetActive(false);
         }
     }
}
