using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shoppanel : MonoBehaviour
{
    public TMP_Text coinText;
    public void enableSelf(){
        gameObject.SetActive(true);
    }
    public void diableSelf(){
        gameObject.SetActive(false);
    }

    void Start()
    {
        coinText.text=coinText.text+PlayerPrefs.GetInt("NumberofCoins");
        
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
