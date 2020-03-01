using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform targetTransform { get; set; }
    public int index { get; private set; }
    public void OnStart(int index, Vector3 pos)
    {
        this.index = index;
        Transform skin = Instantiate(NPCSkinSO.i.skins[index], pos, Quaternion.identity, transform);
        //animators[index].SetBool("Run", false);
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 0;
        agent.angularSpeed = 1000;
        agent.acceleration = 100;
        agent.speed = 5;
    }

    public void OnUpdate()
    {
        targetTransform = GameDirector.i.gameManager.humanManager.GetTargetTransform(transform.position);
        if (targetTransform)
        {
            //agent.SetDestination(Vector3.zero);

            agent.SetDestination(targetTransform.position);


            Debug.Log(agent.hasPath);
        }
    }
}
