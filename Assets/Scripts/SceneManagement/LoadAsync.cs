using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAsync : MonoBehaviour
{
    private AsyncOperation _asyncOperation;
    private string _sceneToLoad;
    public static LoadAsync LoadInstance;

    private void Awake()
    {
        if (LoadInstance == null)
        {
            LoadInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable ()
    {
        LoadScene("PlayGround");
    }

    private IEnumerator Delay (string Scene)
    {
        yield return new WaitForEndOfFrame();
        //SceneManager.LoadScene(AppScenes.GAME_SCENE);
        _asyncOperation = SceneManager.LoadSceneAsync(Scene, LoadSceneMode.Single);
        _asyncOperation.allowSceneActivation = false;

        while (!(_asyncOperation.progress >= 0.9f))
        {
            Debug.Log(_asyncOperation.progress.ToString("0.0000"));
            yield return null;
        }
        
        FinishLoading();

    }

    public void LoadScene(string SceneName)
    {
        StartCoroutine(Delay(SceneName));
    }
    
    private void FinishLoading()
    {
        _asyncOperation.allowSceneActivation = true;
        enabled = false;
    }
}
