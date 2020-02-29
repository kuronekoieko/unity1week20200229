using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 画面UIの一括管理
/// GameDirectorと各画面を中継する役割
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField] RectTransform safeArea;
    BaseCanvasManager[] baseCanvasManagers;

    public void OnStart()
    {
        baseCanvasManagers = new BaseCanvasManager[safeArea.childCount];
        for (int i = 0; i < baseCanvasManagers.Length; i++)
        {
            var baseCanvasManager = safeArea.GetChild(i).GetComponent<BaseCanvasManager>();
            if (baseCanvasManager == null) { continue; }
            baseCanvasManagers[i] = baseCanvasManager;
            baseCanvasManagers[i].OnStart();
        }
    }

    public void OnInitialize()
    {
        for (int i = 0; i < safeArea.childCount; i++)
        {
            if (baseCanvasManagers[i] == null) { continue; }
            baseCanvasManagers[i].OnInitialize();
        }
    }
}
