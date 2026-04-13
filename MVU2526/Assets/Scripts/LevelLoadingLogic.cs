
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LevelLoadingLogic : MonoBehaviour
{
    private LevelLoader levelLoader;

    [Inject]
    public void SetDependencies(LevelLoader levelLoader)
    {
        this.levelLoader = levelLoader;
    }

    private void Update()
    {
        if (levelLoader.HasLoadingRequest)
        {
            StartCoroutine(StartToLoadScene());
            levelLoader.ConsumeRequest();
        }
    }

    private IEnumerator StartToLoadScene()
    {
        var currentConfig = levelLoader.CurrentRequest;
        yield return LoadSceneAsync(currentConfig.logicScenePath, LoadSceneMode.Single);
        yield return LoadSceneAsync(currentConfig.artScenePath, LoadSceneMode.Additive);
        yield return LoadSceneAsync(currentConfig.designScenePath, LoadSceneMode.Additive);
        yield return LoadSceneAsync(currentConfig.audioScenePath, LoadSceneMode.Additive);
    }

    private static IEnumerator LoadSceneAsync(string scenePath, LoadSceneMode mode)
    {
        var asyncOperation = SceneManager.LoadSceneAsync(scenePath, mode);

        asyncOperation.allowSceneActivation = false;

        //This is another way to implement WaitUntil
        while (asyncOperation.progress < 0.9f)
        {
            yield return null;
        }

        asyncOperation.allowSceneActivation = true;

        yield return new WaitUntil(() => asyncOperation.isDone);
    }
}