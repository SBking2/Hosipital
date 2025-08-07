using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonAutoMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T s_Instance;

    public static T GetInstance()
    {
        if (s_Instance == null)
        {
            GameObject obj = new GameObject(typeof(T).Name);
            s_Instance = obj.AddComponent<T>();
            DontDestroyOnLoad(obj);
        }
        return s_Instance;
    }
}
