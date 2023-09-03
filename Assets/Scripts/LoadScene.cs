using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private int sceneIndex;
    
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
        
    }
    
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(sceneIndex));
    }

    IEnumerator LoadLevel(int levelindex)
    {
        sceneIndex = levelindex;
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneIndex);
    }
}
