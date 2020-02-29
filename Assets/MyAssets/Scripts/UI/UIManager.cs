using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 画面UIの一括管理
/// GameDirectorと各画面を中継する役割
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField] GameCanvasManager gameCanvasManager;
    [SerializeField] ResultCanvasManager resultCanvasManager;
    public void OnStart()
    {
        gameCanvasManager.OnStart();
        resultCanvasManager.OnStart();
    }

    public void OnInitialize()
    {
        gameCanvasManager.OnInitialize();
        resultCanvasManager.OnInitialize();
    }
}
