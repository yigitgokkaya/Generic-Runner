using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public int selected_character_index; // Selected character
    public GameObject[] characters;// All characters
    //public Animator[] animators;
    //public MuteManager[] cameras;

    //public Avatar[] avatar;
    public MuteManager[] muteManagers;

    // Start is called before the first frame update
    void Start()
    {
        // Store the all initial positions of character models
        // Get the selected character index from memory
        selected_character_index=PlayerPrefs.GetInt("SelectedCharacter",0);
        // Set the all elements to deactive
        foreach(GameObject ob in characters ){
            ob.SetActive(false);
        }
        // activate the selected character and show
        characters[selected_character_index].SetActive(true);
        GameManager.Instance.setPlayer(characters[selected_character_index].GetComponent<PlayerMovement>());
        //GameManager.Instance.setCamera(characters[selected_character_index].GetComponent<MuteManager>());
        GameManager.Instance.setAnimator(characters[selected_character_index].GetComponent<Animator>());
        GameManager.Instance.setCamera(muteManagers[selected_character_index]);
        
    }
}
