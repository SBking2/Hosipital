using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 此脚本用来加载一些模块
/// </summary>
public class PreLoad : SingletonMono<PreLoad>
{
    private void Start()
    {
        SingletonHandler.GetInstance();
    }
}
