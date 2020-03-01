using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] NPCController nPCControllerPrefab;
    NPCController[] nPCControllers;

    public void OnStart()
    {
        nPCControllers = new NPCController[Values.NPC_COUNT];
        for (int i = 0; i < nPCControllers.Length; i++)
        {
            Vector3 pos = GameDirector.i.gameManager.humanManager.GetRandomPos();
            nPCControllers[i] = Instantiate(nPCControllerPrefab, pos, Quaternion.identity, transform);
            nPCControllers[i].OnStart();
            nPCControllers[i].index = i;
        }
    }

    public void OnUpdate()
    {
        for (int i = 0; i < nPCControllers.Length; i++)
        {
            nPCControllers[i].OnUpdate();
        }
    }
}
