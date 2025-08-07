using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T s_Instance;
    public static T Instance
    {
        get
        {
            return s_Instance;
        }
    }

    protected virtual void Awake()
    {
        s_Instance = this as T;
        DontDestroyOnLoad(this);
    }
}
