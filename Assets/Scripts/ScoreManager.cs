using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    //public AudioSource hitSFX;
    //public AudioSource missSFX;
    public TMPro.TextMeshPro scoreText;
    static int currentScore;
    static int perfectScore = 20;
    static int greatScore = 10;
    
    void Start()
    {
        Instance = this;
        currentScore = 0;
    }
    public static void Hit()
    {
        currentScore+= perfectScore;
        //Instance.hitSFX.Play();
    }

    public static void Great()
    {
        currentScore += greatScore;
    }
    public static void Miss()
    {
        currentScore += 0;
        //Instance.missSFX.Play();
    }
    private void Update()
    {
        scoreText.text = currentScore.ToString();
    }
}
