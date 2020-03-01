using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HumanManager : MonoBehaviour
{

    [SerializeField] HumanController humanControllerPrefab;
    [SerializeField] Transform fieldCornerUR;
    [SerializeField] Transform fieldCornerLL;
    HumanController[] humanControllers;

    public void OnStart(UnityChanController unityChan)
    {
        HumanCreator();
    }

    void HumanCreator()
    {
        humanControllers = new HumanController[500];
        for (int i = 0; i < humanControllers.Length; i++)
        {
            Vector3 pos = GetRandomPos();
            humanControllers[i] = Instantiate(humanControllerPrefab, pos, Quaternion.identity, transform);
            humanControllers[i].OnStart(HumanType.None, pos);
        }
    }

    public Vector3 GetRandomPos()
    {
        //座標をランダムに取得
        float x = Random.Range(fieldCornerLL.position.x, fieldCornerUR.position.x);
        float z = Random.Range(fieldCornerLL.position.z, fieldCornerUR.position.z);
        return new Vector3(x, 0, z);
    }

    public void OnUpdate()
    {
        for (int i = 0; i < humanControllers.Length; i++)
        {
            humanControllers[i].OnUpdate();
        }
    }

    public Transform GetTargetTransform(Vector3 npcPos)
    {
        var humanController = humanControllers
            .Where(h => h.humanType == HumanType.None)
            .OrderBy(h => (npcPos - h.transform.position).magnitude)
            .FirstOrDefault();

        return humanController.transform;
    }
}
