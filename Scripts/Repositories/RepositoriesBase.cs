using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositoriesBase
{
    private Dictionary<Type, Repository> repositoriesMap;
    private SceneConfig sceneConfig;

    public RepositoriesBase(SceneConfig sceneConfig)
    {
        this.sceneConfig = sceneConfig;
    }

    public void CreateAllRepositories()
    {
        repositoriesMap = sceneConfig.CreateAllRepositories();
    }

    public void CreateRepository<T>() where T : Repository, new()
    {
        var repository = new T();
        var type = typeof(T);
        this.repositoriesMap[type] = repository;
    }

    public void SendOnCreateToAllRepositories()
    {
        var allRepositories = this.repositoriesMap.Values;

        foreach (var repository in allRepositories)
        {
            repository.OnCreate();
        }
    }

    public void InitializedAllRepositories()
    {
        var allRepositories = this.repositoriesMap.Values;

        foreach (var repository in allRepositories)
        {
            repository.Initialize();
        }
    }

    public void SendOnStartToAllRepositories()
    {
        var allRepositories = this.repositoriesMap.Values;

        foreach (var repository in allRepositories)
        {
            repository.OnStart();
        }
    }

    public T GetRepository<T>() where T : Repository
    {
        var type = typeof(T);

        return (T)this.repositoriesMap[type];
    }
}
