
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LevelLoadingLogic : MonoBehaviour
{
    private const string LOADING_SCENE = "Scenes/LoadingScene";

    private LevelLoader levelLoader;

    private List<AsyncOperation> loadingOperations 
        = new List<AsyncOperation>();

    [Inject]
    public void SetDependencies(LevelLoader levelLoader)
    {
        this.levelLoader = levelLoader;
    }

    private void Update()
    {
        if (levelLoader.HasLoadingRequest)
        {
            loadingOperations.Clear();
            StartCoroutine(StartToLoadScene());
            levelLoader.ConsumeRequest();
        }
    }

    private IEnumerator StartToLoadScene()
    {
        levelLoader.SetProgress(0);
        var currentConfig = levelLoader.CurrentRequest;
        var asyncOperation = SceneManager.LoadSceneAsync(LOADING_SCENE, LoadSceneMode.Single);

        asyncOperation.allowSceneActivation = false;

        //This is another way to implement WaitUntil
        while (asyncOperation.progress < 0.9f)
        {
            yield return null;
        }

        asyncOperation.allowSceneActivation = true;

        while (asyncOperation.isDone == false)
        {
            yield return null;
        }

        loadingOperations.Clear();


        yield return LoadSceneAsync(currentConfig.logicScenePath);
        levelLoader.SetProgress(0.25f);
        yield return LoadSceneAsync(currentConfig.artScenePath);
        levelLoader.SetProgress(0.5f);
        yield return LoadSceneAsync(currentConfig.designScenePath);
        levelLoader.SetProgress(0.75f);
        yield return LoadSceneAsync(currentConfig.audioScenePath);
        levelLoader.SetProgress(1f);

        foreach (var loadingOperation in loadingOperations)
        {
            loadingOperation.allowSceneActivation = true;
        }

        yield return new WaitUntil(() => loadingOperations.All(ao => ao.isDone));

        var unloadingScene = SceneManager.UnloadSceneAsync(LOADING_SCENE);

        yield return new WaitUntil(() => unloadingScene.isDone);
    }

    private IEnumerator LoadSceneAsync(string scenePath)
    {
        var asyncOperation = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);

        loadingOperations.Add(asyncOperation);

        asyncOperation.allowSceneActivation = false;

        //This is another way to implement WaitUntil
        while (asyncOperation.progress < 0.9f)
        {
            yield return null;
        }

        asyncOperation.allowSceneActivation = true;

        while (asyncOperation.isDone == false)
        {
            yield return null;
        }
    }
}