using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancePlayer : MonoBehaviour
{

    public Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
