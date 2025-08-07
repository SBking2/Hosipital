using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 让单例模式可以使用Mono的一些功能,本质是希望编程不依赖Mono
/// </summary>
public class SingletonHandler : SingletonAutoMono<SingletonHandler>
{

    private void Awake()
    {
        InputManager.Instance.Init();
        PlayerController.Instance.Init();
    }

    private void Update()
    {
        float delta = Time.deltaTime;
        PlayerController.Instance.Update(delta);
    }

    private void OnEnable()
    {
        PlayerController.Instance.OnEnable();
    }

    private void OnDisable()
    {
        PlayerController.Instance.OnDisable();
    }
}
