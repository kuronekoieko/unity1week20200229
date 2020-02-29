using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unityで解像度に合わせて画面のサイズを自動調整する
/// http://www.project-unknown.jp/entry/2017/01/05/212837
/// </summary>
public class CameraController : MonoBehaviour
{
    Vector3 distanceToPlayer;
    public void OnStart(Vector3 playerPos)
    {
        distanceToPlayer = transform.position - playerPos;
    }


    public void OnUpdate()
    {

    }

    public void SetCamPos(Vector3 playerPos)
    {
        transform.position = playerPos + distanceToPlayer;
    }
}
