using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
[RequireComponent (typeof (Button))]
public class Reklamci : MonoBehaviour,IUnityAdsListener
{
    public static int watchedcount =0;

    public static Reklamci Instance{get;set;}
    
    void Awake(){
        button=GetComponent<Button>();
        Instance=this;
        button.interactable = false;
    }

    string gameId = "4478544"; // app id
    bool testMode = true; // test mode activate for adds
    string placementId ="Rewarded_Android"; // add type
    private bool collected=false; // reward checker
    public static bool watched =false; // watch reward tracker
    Button button;

void Start()
    {
        Advertisement.AddListener(this); 
        // initiate the add listener
        Advertisement.Initialize(gameId,testMode);
        // reset reward
        collected=false;
        //Debug.Log(button.name);
        // make the reward button active
        button.interactable =true;
        // Load and prepare the adds
        LoadAd();

        
        
        
    }
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + placementId);
        Advertisement.Load(placementId);
    }
    public void DisplayAd()
    {
        Debug.Log(Advertisement.IsReady("Interstitial_Android"));
           if (Advertisement.IsReady("Interstitial_Android")){
                Advertisement.Show("Interstitial_Android");
        }
        
    }
    public void DisplayVideoAd(){
        Advertisement.Show(placementId);
        watched=true;
    }

    public void OnUnityAdsDidError(string message)
    {
        
    }
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(showResult == ShowResult.Finished && Advertisement.IsReady(placementId)){
            // Reward User
            Advertisement.RemoveListener(this);
            button.interactable = false;
            Debug.Log("You got a reward");
            // Give The Reward
            GameManager.Instance.increaseScore(300);
        }else{
            // No Reward
             Debug.Log("You dont get a reward");
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        
    }

    public void OnUnityAdsReady(string placementId2)
    {
        if (placementId == placementId2 && collected==false) {        
            
        }  
    }


    public void setWatchedFalse(){
        watched=false;
    }

    void Update(){
        if(watchedcount>1){
            button.interactable=false;
            watchedcount=0;
            return;
        }
    }
    void OnDestroy()
    {
        // Clean up the button listeners:
        Advertisement.RemoveListener(this);
        
    }

    
}
