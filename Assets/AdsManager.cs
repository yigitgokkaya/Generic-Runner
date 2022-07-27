using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;
using UnityEngine.UI;
public class AdsManager : MonoBehaviour, IUnityAdsListener
{

string gameId = "4478544";


public Button button;



int count = 0;

Action onRewardedAdSuccess;     

//public GameObject manager;
    
    // Start is called before the first frame update
    void Start()
    {   
        
        Advertisement.Initialize(gameId);
        Advertisement.AddListener(this);
    }

    public void playAd() 
    {
        if (Advertisement.IsReady("Interstitial_Android")){
            Advertisement.Show("Interstitial_Android");
        }

    
    }

    public void playRewardedAd(Action onSuccess)
    {   onRewardedAdSuccess = onSuccess;
        if(Advertisement.IsReady("Rewarded_Android")){
            Advertisement.Show("Rewarded_Android");
        }else 
        {
            Debug.Log("Rewarded ad is not ready");
        }
    }

   

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Ads are ready");
        button.enabled=true;
        
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("ERROR" + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Ad started");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
         if(placementId == "Rewarded_Android" && showResult == ShowResult.Finished)
         {  
             
             onRewardedAdSuccess.Invoke();
      
    
    }
    }

     

  
}
