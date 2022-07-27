using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager2 : MonoBehaviour
{
    [SerializeField]TMP_Text coinText;
   public static GameManager2 Instance {get;set;}
   private int coinCollected;


   private void Awake(){
       Instance=this;
   }
   private void Start(){
       coinText.text="Collected => "+0;
       coinCollected=0;
   }
   public void collect(){
       coinCollected=+1;
       coinText.text="Collected =>"+coinCollected;
   }
}
