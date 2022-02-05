using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene
{
    private RepositoriesBase repositoriesBase;
    private InteractorsBase interactorsBase;
    private SceneConfig sceneConfig;

    public Scene(SceneConfig sceneConfig)
    {
        this.sceneConfig = sceneConfig;
        this.interactorsBase = new InteractorsBase(sceneConfig);
        this.repositoriesBase = new RepositoriesBase(sceneConfig);
    }

    public Coroutine InitializeAsync()
    {
        return Coroutines.StartRoutine(this.InitializeRoutine());
    }

    private IEnumerator InitializeRoutine()
    {
        interactorsBase.CreateAllInteractors();
        repositoriesBase.CreateAllRepositories();

        interactorsBase.SendOnCreateToAllInteractors();
        repositoriesBase.SendOnCreateToAllRepositories();

        interactorsBase.InitializedAllInteractors();
        repositoriesBase.InitializedAllRepositories();

        interactorsBase.SendOnStartToAllInteractors();
        repositoriesBase.SendOnStartToAllRepositories();


        yield return null;
    }
    
    public T GetRepository<T>() where T : Repository
    {
        return this.repositoriesBase.GetRepository<T>();
    }

    public T GetInteractor<T>() where T : Interactor
    {
        return this.interactorsBase.GetInteractor<T>();
    }
}
