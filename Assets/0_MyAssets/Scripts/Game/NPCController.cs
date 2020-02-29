using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    [SerializeField] Animator animator;
    NavMeshAgent agent;
    public Transform targetTransform { get; set; }

    public void OnStart()
    {
        animator.SetBool("Run", false);
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 0;
        agent.angularSpeed = 1000;
        agent.acceleration = 100;
        agent.speed = 5;
        // agent.enabled = false;

    }

    public void OnUpdate()
    {
        animator.SetBool("Run", true);

        targetTransform = GameDirector.i.gameManager.humanManager.GetTargetTransform(transform.position);
        if (targetTransform)
        {
            agent.SetDestination(targetTransform.position);
        }
    }
}
