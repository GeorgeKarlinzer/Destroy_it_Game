using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElements : MonoBehaviour
{
    [SerializeField] private GameObject[] elements;


    public void Hide()
    {
        foreach (var c in elements)
        {
            if (c.TryGetComponent(out Animator a))
                a.SetTrigger("Hide");
            else
                c.SetActive(false);
        }
    }

    public void Show()
    {
        foreach (var c in elements)
        {
            if (c.TryGetComponent(out Animator a))
                a.SetTrigger("Show");
            else
                c.SetActive(true);
        }
    }
}
