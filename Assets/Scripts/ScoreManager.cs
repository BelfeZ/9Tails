using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    //public AudioSource hitSFX;
    //public AudioSource missSFX;
    public TMPro.TextMeshPro scoreText;
    public TMPro.TextMeshPro comboText;
    public static int currentScore;
    public static int currentMultiplier;
    public static int multiplierTracker; 
    public static  int[] multiplierThresholds = {2,4,6};
    static int perfectScore = 20;
    static int greatScore = 10;
    
    void Start()
    {
        Instance = this;
        currentScore = 0;
        currentMultiplier = 1;
    }
    public static void Perfect()
    {
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
                Debug.Log("combo increase");
            }
        }
        
        
        currentScore+= perfectScore * currentMultiplier;
        //Instance.hitSFX.Play();
    }

    public static void Great()
    {
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
                Debug.Log("combo increase");
            }
        }
        
        currentScore += greatScore * currentMultiplier;
    }
    public static void Miss()
    {
        currentMultiplier = 1;
        multiplierTracker = 0;
        currentScore += 0;
        Debug.Log("Reset Combo");
        //Instance.missSFX.Play();
    }
    private void Update()
    {
        scoreText.text = currentScore.ToString();
        comboText.text = "Combo x" + currentMultiplier;
    }
}
