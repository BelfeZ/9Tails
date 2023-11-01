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
    //public static int multiplierTracker; 
    public static  int[] multiplierThresholds = {2,4,6};
    static int perfectScore = 20;
    static int greatScore = 10;

    //Don'Update
    public static float totalNotes;
    public static float perfectHits;
    public static float greatHits;
    public static float missHits;

    void Start()
    {
        Instance = this;
        currentScore = 0;
        currentMultiplier = 0;
    }
    public static void Perfect()
    {
        //if (currentMultiplier - 1 < multiplierThresholds.Length)
        //{
            //multiplierTracker++;

            //if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            //{
                //multiplierTracker = 0;
                currentMultiplier++;
                Debug.Log("Perfect Combo increase");
            //}
        //}
        
        
        currentScore+= perfectScore * currentMultiplier;
        //Instance.hitSFX.Play();

        //Don'Update
        perfectHits++;
    }

    public static void Great()
    {
        //if (currentMultiplier - 1 < multiplierThresholds.Length)
        //{
            //multiplierTracker++;

            //if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            //{
                //multiplierTracker = 0;
                currentMultiplier++;
                Debug.Log("Great combo increase");
            //}
        //}
        
        currentScore += greatScore * currentMultiplier;

        //Don'Update
        greatHits++;
    }
    public static void Miss()
    {
        currentMultiplier = 0;
        //multiplierTracker = 0;
        currentScore += 0;
        Debug.Log("Missed Reset Combo");
        //Instance.missSFX.Play();

        //Don'Update
        missHits++;
    }
    private void Update()
    {
        scoreText.text = currentScore.ToString();
        comboText.text = "Combo x" + currentMultiplier;

        //Don's Update
        if(SongManager.finished == true)
        {
            float totalNotesCheck = perfectHits + greatHits + missHits;
            totalNotes = totalNotesCheck;
        }
    }
}
