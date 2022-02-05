using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class MonoPool : Interactor
{
    private Dictionary<Type, Stack<MonoBehaviour>> poolsMap;
    private Dictionary<Type, MonoBehaviour> prefabsMap;

    public override void Initialize()
    {
        this.poolsMap = new Dictionary<Type, Stack<MonoBehaviour>>()
        {
            { typeof(StandartCube), new Stack<MonoBehaviour>() },
            { typeof(BonusCube), new Stack<MonoBehaviour>() },
            { typeof(Spike), new Stack<MonoBehaviour>() }
        };

        this.prefabsMap = new Dictionary<Type, MonoBehaviour>()
        {
            { typeof(StandartCube),  Resources.Load<StandartCube>("StandartCube")},
            { typeof(BonusCube), Resources.Load<BonusCube>("BonusCube") },
            { typeof(Spike), Resources.Load<Spike>("Spike") }
        };
    }

    public void Push<T>(T mono, bool isDisactivate = true) where T : MonoBehaviour
    {
        var type = typeof(T);

        if (!poolsMap.ContainsKey(type))
            throw new ArgumentException($"Pool of type {type} doesn't exist!");

        mono.gameObject.SetActive(!isDisactivate);
        poolsMap[type].Push(mono);
    }

    public T PopOrCreate<T>(bool isActiveByDefault = true) where T : MonoBehaviour
    {
        var type = typeof(T);

        if (!poolsMap.ContainsKey(type))
            throw new ArgumentException($"Pool of type {type} doesn't exist!");

        if (poolsMap[type].Count > 0)
        {
            var objFromStack = (T)poolsMap[type].Pop();
            objFromStack.gameObject.SetActive(isActiveByDefault);

            return objFromStack;
        }

        var prefab = prefabsMap[type];
        var newObj = (T)Object.Instantiate(prefab);
        return newObj;
    }
}
