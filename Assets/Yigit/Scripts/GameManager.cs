using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using UnityEngine.Advertisements;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance{get;set;}


    public static float speed;


    // SAVEABLE VALUES
    public static int score;
    public static int   savedScore;
    public static int highscore;
    public static int coinCount;

    public static int savedCoinCount;
    private static float scoreMultiplyer =0.2f;

    

    // Game Managment Variables Start&End
    public static bool isGameStarted;
    public static bool isGameEnded;

    public static int deadcount=0;

    // Player variables
    // [SerializeField] public Animator[] animators;
    // [SerializeField] public PlayerMovement[] players;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Animator playerAnimator;
    // [SerializeField] private AnimatorController playerController;
    // [SerializeField] private Avatar playerAvatar;
    [SerializeField] private MuteManager maincamera;



    // UI variables
    public GameObject startingText;
    public GameObject pauseButton;
    public GameObject muteButton;

    public GameObject PauseMenu;
    public GameObject DeathMenu;

   // public AdsManager ads;
    //public Reklamci adder; 
    [SerializeField] TMP_Text DeathScore;
    [SerializeField] TMP_Text DeathHighScore;

    [SerializeField] TMP_Text DeathCoinCount;
    
    // Text Score u belirten
    public TMP_Text scoreText;
    public TMP_Text coinText;
    // Text Coin sayısını belirten



    void Awake(){
        Instance=this;
        
    }

    // Start is called before the first frame update
    void Start()
    {

       //PlayerPrefs.SetInt("HighScore",0);

        // playerAnimator.avatar=playerAvatar;
        // playerAnimator.runtimeAnimatorController = Resources.Load("Assets/Gokay/NEW PLAYER/Animations/Player.controller") as RuntimeAnimatorController;
        //Button btn = playButton.GetComponent<Button>(); // Component
        savedScore = 0;
        savedCoinCount=0;
        coinCount=0;
        highscore=PlayerPrefs.GetInt("HighScore");
        isGameStarted = false;
        pauseButton.SetActive(false);
        muteButton.SetActive(false);
        scoreText.text= "Score: 0";
        coinText.text="Coins: "+0;
        // foreach(GameObject ob in GameObject.FindGameObjectsWithTag("Player")){
        //     if(ob.activeSelf==true){
        //         player=ob.GetComponent<PlayerMovement>();
        //         playerAnimator=ob.GetComponent<Animator>();
        //         break;
        //     }
        // } 
    }

    // Update is called once per frame
    
    void Update()
    {
        speedConditions();
        if(TouchInputs.Instance.Tap && ((TouchInputs.Instance.SwipeDown==false)&&
        (TouchInputs.Instance.SwipeUp==false) && 
        (TouchInputs.Instance.SwipeLeft==false) && 
        (TouchInputs.Instance.SwipeRight==false)))
        {
        // { if ((EventSystem.current.IsPointerOverGameObject.Input.GetTouch (0).fingerId)) 
        //  {
        //      Debug.Log("Hit UI button, Ignore Touch");
        //  } 
           playerAnimator.SetTrigger("GameStarted");
           player.GetComponent<CharacterController>().detectCollisions=true;
           Destroy(startingText);
           pauseButton.SetActive(true);
           muteButton.SetActive(true);
          // FindObjectOfType<CameraMotor>().IsMoving = true;
           isGameStarted = true;
           playerAnimator.SetTrigger("GameStarted");
        }   
        // Score Calculator
        if(player.dead!=true && Time.timeScale!=0 && isGameStarted){
            savedScore += (int)(1+(1.0f*scoreMultiplyer));
            //Debug.Log(score);
            scoreText.text="Score:"+savedScore.ToString();
            
        }
        if (player.dead)
        {
            deadcount+=1;
            isGameEnded=true;
            player.dead = false;
            DeathMenu.SetActive(true);
            // Update Highscore
            Updatehighscore();
            //resetHighscore();
            // Update Coin Count
            UpdateCoinCount();

            pauseButton.SetActive(false);
            savedScore+=0;
            Destroy(scoreText);
            Debug.Log(deadcount); 
            
            
            // Show the GameEndMenu
            
            
            //ads.playAd();
            
            //resetHighscore();
            
         }
        
        //Debug.Log("Player State => "+player.dead);
        //Debug.Log("Time scale => "+Time.timeScale);
        //Debug.Log("Game Started"+isGameStarted);
    }
    public void speedConditions(){
        switch(savedScore){
                case  1000:
                    speedIncrease(2);
                    break;
                case 3000:
                    speedIncrease(2);
                    break;
                case  8000:
                    speedIncrease(2);
                    break;
                case 15000:
                    speedIncrease(2);
                    break;
                default:
                    speed=10;
                    break;
            }
                
    }
    public void speedIncrease(int i){
        speed=speed+i;
    }
    public float GetSpeed(){
        return speed;
    }

    public void setPlayer(PlayerMovement p){
        player=p;
    }
    public void setAnimator(Animator animator){
        playerAnimator=animator;
    }
    // public void setAnimatorController(AnimatorController controller){
    //     playerController=controller;
    // }
    // public void setAvatar(Avatar avatar){
    //     playerAvatar=avatar;
    // }
    public void setCamera( MuteManager c){
        maincamera=c;
    }



    public void scoreAnalytics(){
      AnalyticsResult result= Analytics.CustomEvent("Scoree", new Dictionary<string, object>
        {
            { "Score", score},
            { "Scene", SceneManager.GetActiveScene().name}
        });

        Debug.Log(result);
        
    }
    public int getScore(){
        return score;
    }
    public void SetScore(int s){
        score= s;
    }
    public void setGameStarted(Boolean b){
        isGameStarted=b;
    }
    public void Updatehighscore(){
            score=savedScore;
            savedScore=0;
            DeathScore.text="Score:"+score;
             if(score>highscore){
                 PlayerPrefs.SetInt("HighScore",score);
                 highscore=PlayerPrefs.GetInt("HighScore");
                 DeathHighScore.text="Highscore:"+highscore;   
             }
             else{
                 DeathHighScore.text="Highscore:"+highscore;
             }
    }

    public void UpdateCoinCount(){
        savedCoinCount=PlayerPrefs.GetInt("NumberofCoins");
        DeathCoinCount.text="Coins: "+coinCount;
        PlayerPrefs.SetInt("NumberofCoins",savedCoinCount+coinCount);
        //PlayerPrefs.SetInt("NumberofCoins",0);
    }
    public void increaseCoin(){
        coinCount++;
        coinText.text="Coins  "+coinCount;
    }

     public void SendHighScore(float highScore)
    {
        // Create dictionary to store you event data
        Dictionary<string, object> data = new Dictionary<string, object>();
 
        //Add the high score to your event data
        data.Add("high_score", highScore);
 
        // The name of the event that you will send
        string eventName = "High Score";
 
        //Send the event. Also get the result, so we can make sure it sent correctly
        AnalyticsResult result = Analytics.CustomEvent(eventName, data);
        Debug.Log(result);
}

    public void resetHighscore(){
        DeathScore.text="Score:"+score;
        PlayerPrefs.SetInt("HighScore",0);
        highscore=PlayerPrefs.GetInt("HighScore");
        DeathHighScore.text="Highscore:"+highscore;
    }   
    

    public void increaseScore(int amount){
        Debug.Log(amount);
        savedScore=score+amount;
        score=savedScore;
        DeathScore.text="Score:"+score;
             if(score>highscore){
                 PlayerPrefs.SetInt("HighScore",score);
                 highscore=PlayerPrefs.GetInt("HighScore");
                 DeathHighScore.text="Highscore:"+highscore;   
             }
             else{
                 DeathHighScore.text="Highscore:"+highscore;
             }
    }

    public void manageSound(){
        maincamera.SoundControl();
    }
}
