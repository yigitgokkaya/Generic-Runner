using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopManager : MonoBehaviour
{

    public int selected_character_index; // Selected character
    public GameObject[] playerModels;// All character Models
    public Vector3[] positions;  // All character Model initial positions

    public CharacterBlueprint[] characters;


    public Button buyButton;

    public TMP_Text coinText;

    // Start is called before the first frame update
    void Start()
    {

        //LockCharacter();
       // PlayerPrefs.SetInt("NumberofCoins",1000);
        // Check the avaiable characters in shop
        foreach(CharacterBlueprint cb in characters){
            if(cb.price==0){
                cb.isUnlocked=true;
            }
            else{
                cb.isUnlocked = PlayerPrefs.GetInt(cb.name,0) == 0 ? false : true;
            }

        }
        // Store the all initial positions of character models
        // Get the selected character index from memory
        //selected_character_index=PlayerPrefs.GetInt("SelectedCharacter",0);
        selected_character_index=PlayerPrefs.GetInt("SelectedCharacter",0);
        // Set the all elements to deactive
        foreach(GameObject ob in playerModels ){
            
            ob.SetActive(false);
        }
        // activate the selected character and show
        playerModels[selected_character_index].SetActive(true);
    }

    public void nextCharacter(){
        // Disable the current character
        playerModels[selected_character_index].transform.position=positions[selected_character_index];
        playerModels[selected_character_index].SetActive(false);
        selected_character_index++;
        // if the index out of bounds set back to 0 
        if(selected_character_index==playerModels.Length){
            selected_character_index=0;
        }
        // Activate the new Character.
        playerModels[selected_character_index].SetActive(true);
        CharacterBlueprint c = characters[selected_character_index];
        if(!c.isUnlocked){
            return;
        }
        // Update the new index in memory
        PlayerPrefs.SetInt("SelectedCharacter",selected_character_index);
    }
    public void prevCharacter(){
        // Disable the current character
        playerModels[selected_character_index].transform.position=positions[selected_character_index];
        playerModels[selected_character_index].SetActive(false);
        selected_character_index--;
        // if the index is negative set back to lenght-1
        if(selected_character_index<0){
            selected_character_index=playerModels.Length-1;
        }
        // Activate the new Character.
        playerModels[selected_character_index].SetActive(true);
        
        CharacterBlueprint c = characters[selected_character_index];
        if(!c.isUnlocked){
            return;
        }
        // Update the new index in memory
        PlayerPrefs.SetInt("SelectedCharacter",selected_character_index);
        
    }



    private void UpdateUI(){
        CharacterBlueprint c = characters[selected_character_index];
        if(c.isUnlocked){
            buyButton.gameObject.SetActive(false);
        }
        else{
             buyButton.gameObject.SetActive(true);
             buyButton.GetComponentInChildren<Text>().text="Buy-> " +c.price;
             if(c.price<PlayerPrefs.GetInt("NumberofCoins",0)){
                 buyButton.interactable=true;
             }else{
                 buyButton.interactable=false;
             }

        }
    }



    public void UnlockCharacter(){
         CharacterBlueprint c = characters[selected_character_index];
         PlayerPrefs.SetInt(c.name,1);
         PlayerPrefs.SetInt("SelectedCharacter",selected_character_index);
         c.isUnlocked=true;
         PlayerPrefs.SetInt("NumberofCoins",PlayerPrefs.GetInt("NumberofCoins",0)-c.price);
         coinText.text="Coin : "+PlayerPrefs.GetInt("NumberofCoins");

    }

    public void LockCharacter(){
        PlayerPrefs.SetInt(characters[1].name,0);
        PlayerPrefs.SetInt(characters[2].name,0);
        PlayerPrefs.SetInt(characters[3].name,0);
        PlayerPrefs.SetInt("SelectedCharacter",0);
    }



    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        
    }
    public void selectLastUnlocked(){
        int tracker=0;
        Debug.Log("Girdi");
        playerModels[selected_character_index].SetActive(false);
        int index = PlayerPrefs.GetInt("SelectedCharacter");
        playerModels[index].SetActive(false);
        if(characters[index].isUnlocked){
            playerModels[index].SetActive(true);
        }else{
            
            for(int i =0;i<characters.Length;i++){
                if(characters[i].isUnlocked && characters[i+1].isUnlocked){
                    tracker=(i+1);
                }
                else if(characters[i].isUnlocked && characters[i+1].isUnlocked){
                    index=i;
                }
            }
            if(index < tracker){
                index=tracker;
            }
            playerModels[index].SetActive(true);
            PlayerPrefs.SetInt("SelectedCharacter",index);
        }
    }
        
    
}
