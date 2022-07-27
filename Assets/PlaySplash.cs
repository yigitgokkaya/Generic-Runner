using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySplash : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(AnimationFinish());
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator AnimationFinish(){
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("MENUU");

    }
}
