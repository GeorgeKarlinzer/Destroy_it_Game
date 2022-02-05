using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class SceneManagerBase
{
    public Scene Scene { get; private set; }
    public bool IsLoading { get; private set; }

    protected Dictionary<string, SceneConfig> sceneConfigMap;

    public SceneManagerBase()
    {
        this.sceneConfigMap = new Dictionary<string, SceneConfig>();
        InitScenesMap();
    }

    protected abstract void InitScenesMap();

    public Coroutine LoadCurrentSceneAsync()
    {
        if (this.IsLoading)
            throw new Exception("Scene is loading now");

        var sceneName = SceneManager.GetActiveScene().name;
        var config = sceneConfigMap[sceneName];
        return Coroutines.StartRoutine(LoadCurrentSceneRoutine(config));
    }

    private IEnumerator LoadCurrentSceneRoutine(SceneConfig sceneConfig)
    {
        this.IsLoading = true;

        yield return Coroutines.StartRoutine(this.InitializeSceneAsync(sceneConfig));

        this.IsLoading = false;
    }

    public Coroutine LoadNewSceneAsync(string sceneName)
    {
        if (this.IsLoading)
            throw new Exception("Scene is loading now");

        var config = sceneConfigMap[sceneName];

        return Coroutines.StartRoutine(LoadNewSceneRoutine(config));
    }

    private IEnumerator LoadNewSceneRoutine(SceneConfig sceneConfig)
    {
        this.IsLoading = true;

        yield return Coroutines.StartRoutine(this.LoadSceneRoutine(sceneConfig));
        yield return Coroutines.StartRoutine(this.InitializeSceneAsync(sceneConfig));

        this.IsLoading = false;
    }

    private IEnumerator LoadSceneRoutine(SceneConfig sceneConfig)
    {
        var async = SceneManager.LoadSceneAsync(sceneConfig.SceneName);
        async.allowSceneActivation = false;

        while(async.progress < 0.9f)
        {
            yield return null;
        }

        async.allowSceneActivation = true;
    }

    private IEnumerator InitializeSceneAsync(SceneConfig sceneConfig)
    {
        this.Scene = new Scene(sceneConfig);
        yield return this.Scene.InitializeAsync();
    }

    public T GetRepository<T>() where T : Repository
    {
        return this.Scene.GetRepository<T>();
    }

    public T GetInteractor<T>() where T : Interactor
    {
        return this.Scene.GetInteractor<T>();
    }
}
