using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 3D空間の処理の管理
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] CameraController cameraController;
    [SerializeField] UnityChanController unityChanController;
    [SerializeField] HumanManager humanManager;

    public void OnStart()
    {

        unityChanController.OnStart();
        cameraController.OnStart(playerPos: unityChanController.transform.position);
        humanManager.OnStart(unityChanController);
    }

    public void OnInitialize()
    {
        Variables.timer = Values.TIME_LIMIT;
    }

    public void OnUpdate()
    {
        humanManager.OnUpdate();
        unityChanController.OnUpdate();
        cameraController.OnUpdate();
        cameraController.SetCamPos(playerPos: unityChanController.transform.position);
        Variables.timer -= Time.deltaTime;
        if (Variables.timer < 0)
        {
            Variables.screenState = ScreenState.RESULT;
        }
    }



}
