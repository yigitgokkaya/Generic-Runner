using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MuteManager : MonoBehaviour
{

    public GameObject soundControlButton;
    public Sprite audioOffSprite;
    public Sprite audioOnSprite;
    // Start is called before the first frame update
    void Start()
    {
        if(AudioListener.pause == true){
            soundControlButton.GetComponent<Image>().sprite = audioOffSprite;
        } else {
            soundControlButton.GetComponent<Image>().sprite = audioOnSprite;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SoundControl()
    {
        if(AudioListener.pause == true ){
           AudioListener.pause = false;
           soundControlButton.GetComponent<Image> ().sprite = audioOnSprite;
        }else{
            AudioListener.pause = true;
           soundControlButton.GetComponent<Image> ().sprite = audioOffSprite;
        }

    }



}
