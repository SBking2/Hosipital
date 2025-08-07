using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T : class, new()
{
    public static T s_Instace;

    public static T Instance
    {
        get
        {
            if (s_Instace == null)
                s_Instace = new T();
            return s_Instace;
        }
    }
}
