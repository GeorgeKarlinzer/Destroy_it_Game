using System.Linq;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> awakes;

    public static SceneManagerBase SceneManager { get; private set; }


    private void Awake()
    {
        Run();
    }

    public static void Run()
    {
        SceneManager = new SceneManagerMain();
        Coroutines.StartRoutine(InitializeGameRoutine());
    }

    private static IEnumerator InitializeGameRoutine()
    {
        yield return SceneManager.LoadCurrentSceneAsync();
    }

    public static T GetInteractor<T>() where T : Interactor
    {
        return SceneManager.GetInteractor<T>();
    }

    public static T GetRepository<T>() where T : Repository
    {
        return SceneManager.GetRepository<T>();
    }

    private void OnApplicationQuit()
    {
        GetInteractor<SaveSystem>().SaveAll();
    }

    private void OnApplicationPause(bool pause)
    {
        if(pause)
            GetInteractor<SaveSystem>().SaveAll();
    }
}

