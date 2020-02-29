using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using System;
using System.Linq;

/// <summary>
/// 3D空間の処理の管理
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] CameraController _cameraController;
    [SerializeField] UnityChanController _unityChanController;
    [SerializeField] HumanManager _humanManager;
    [SerializeField] NPCController _nPCController;

    public HumanManager humanManager { get { return _humanManager; } }
    public UnityChanController unityChanController { get { return _unityChanController; } }

    void Awake()
    {
        Variables.humanCountDic = new Dictionary<HumanType, int>();
        foreach (HumanType humanType in Enum.GetValues(typeof(HumanType)))
        {
            Variables.humanCountDic.Add(humanType, 0);
        }
    }

    public void OnStart()
    {
        _unityChanController.OnStart();
        _cameraController.OnStart(playerPos: _unityChanController.transform.position);
        _humanManager.OnStart(_unityChanController);
        _nPCController.OnStart();
        Variables.timer = Values.TIME_LIMIT;
    }

    public void OnInitialize()
    {

    }

    public void OnUpdate()
    {
        _humanManager.OnUpdate();
        _unityChanController.OnUpdate();
        _nPCController.OnUpdate();
        _cameraController.OnUpdate();
        _cameraController.SetCamPos(playerPos: _unityChanController.transform.position);
        Variables.timer -= Time.deltaTime;
        if (Variables.timer < 0)
        {
            Variables.screenState = ScreenState.RESULT;
        }
    }

}
