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
    [SerializeField] NPCManager _nPCManager;

    public HumanManager humanManager { get { return _humanManager; } }
    public UnityChanController unityChanController { get { return _unityChanController; } }

    void Awake()
    {
        Variables.nPCHumanCounts = new int[Values.NPC_COUNT];
    }

    public void OnStart()
    {
        _unityChanController.OnStart();
        _cameraController.OnStart(playerPos: _unityChanController.transform.position);
        _humanManager.OnStart(_unityChanController);
        _nPCManager.OnStart();
        Variables.timer = Values.TIME_LIMIT;

        for (int i = 0; i < Variables.nPCHumanCounts.Length; i++)
        {
            Variables.nPCHumanCounts[i] = 0;
        }
    }

    public void OnInitialize()
    {

    }

    public void OnUpdate()
    {
        _humanManager.OnUpdate();
        _unityChanController.OnUpdate();
        _nPCManager.OnUpdate();
        _cameraController.OnUpdate();
        _cameraController.SetCamPos(playerPos: _unityChanController.transform.position);
        Variables.timer -= Time.deltaTime;
        if (Variables.timer < 0)
        {
            Variables.screenState = ScreenState.RESULT;
        }
    }

}
