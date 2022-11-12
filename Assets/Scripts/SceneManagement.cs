using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    string titleScreen = "TitleScreen";
    string levelOne = "Level 1";
    // Start is called before the first frame update
    void Start()
    {
        StartUpScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartUpScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "StartupScreen")
        {
            Debug.Log("Load after delay triggered");
            StartCoroutine(LoadTitleAfterDelay());
        }
    }

    IEnumerator LoadTitleAfterDelay()
    {
        Debug.Log("IEnumerator triggered");
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(titleScreen);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(levelOne);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
