using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
  [SerializeField]private int coinCount =0;

  public void increaseCount(){
      coinCount++;
  }

}
