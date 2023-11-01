using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreTextManager : MonoBehaviour
{
    public static ScoreTextManager instance;
    public bool Issongended;
    public static bool resultisActive = false;

    public TMP_Text percenthitText, perfecthitsText, greathitsText, misshitsText, totalScoreText;
    

    // Start is called before the first frame update
    void Start()
    {
        resultisActive = true;
        Issongended = SongManager.finished;
        //Debug.Log("Miss : " +ScoreManager.missHits);
        Debug.Log("IsSongEnded : "+Issongended);
    }

    // Update is called once per frame
    void Update()
    {
        if (Issongended == true )
        {
            perfecthitsText.text = ScoreManager.perfectHits.ToString();
            greathitsText.text = ScoreManager.greatHits.ToString();
            misshitsText.text = ScoreManager.missHits.ToString();
            totalScoreText.text = ScoreManager.currentScore.ToString();

            float totalHits = ScoreManager.greatHits + ScoreManager.perfectHits;
            float percentHits = (totalHits / ScoreManager.totalNotes) * 100f;
            percenthitText.text = percentHits.ToString("F1") + "%";
        }
    }
}
