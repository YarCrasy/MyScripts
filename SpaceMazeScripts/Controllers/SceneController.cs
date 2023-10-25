using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneName { MainMenu, Loading, Game, }

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    public SceneName actualScene;

    private void Awake()
    {
        instance = this;
        GameSettingData.instance.Load();
        if(actualScene == SceneName.MainMenu) actualScene = (SceneName)SceneManager.GetActiveScene().buildIndex;
    }

    public void GoToScene(SceneName name)
    {
        GoToScene((int)name);
    }

    public void GoToScene(int index)
    {
        StartCoroutine(LoadScene(index));
    }

    IEnumerator LoadScene(int index)    
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
        SceneManager.LoadScene((int)SceneName.Loading, LoadSceneMode.Additive);

        while (!op.isDone)
        {
            yield return null;
        }
        SceneManager.UnloadSceneAsync((int)SceneName.Loading);
        SceneManager.UnloadSceneAsync((int)actualScene);
        instance.actualScene = (SceneName)SceneManager.GetActiveScene().buildIndex;
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
