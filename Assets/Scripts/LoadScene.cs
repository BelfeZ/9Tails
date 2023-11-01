using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private Animator transition;
    private string sceneName;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /*public void GoToLevelSelection()
    {
        SceneManager.LoadScene("LevelSelection");
        Time.timeScale = 1f;
    }*/

    // Update is called once per frame
    void Update()
    {
       if(SongManager.forloadSceneResult == true && ScoreTextManager.resultisActive == false)
        {
            Result();
        }
    }
    
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel("Scene1"));
    }

    public void BackToMenu()
    {
        StartCoroutine(LoadLevel("MainMenu"));
    }

    public void Result()
    {
        StartCoroutine(LoadLevel("Result"));
    }

    IEnumerator LoadLevel(string scenename)
    {
        sceneName = scenename;
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scenename);
    }
}
