using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private static float distance = 0;
    private static float difficultyMultiplier = 1;

    private static float difficultyOffset = 100f;

    private static int obstacleCount; 

    public static float Distance {get => distance; set => distance = value;}

    public static float DifficultyMultiplier {get => difficultyMultiplier; set => difficultyMultiplier = value;}

    public static float DifficultyOffset {get => difficultyOffset; set => difficultyOffset = value;}

    public static int ObstacleCount {get => obstacleCount; set => obstacleCount = value;}

    public static GameController instance;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        
        
    }

    void increaseDiff(){
        difficultyMultiplier = 1 +(distance / difficultyOffset);
    }
}
