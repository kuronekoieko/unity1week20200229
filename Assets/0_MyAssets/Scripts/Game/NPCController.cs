using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    [SerializeField] Animator[] animators;
    NavMeshAgent agent;
    public Transform targetTransform { get; set; }
    public int index { get; private set; }
    public void OnStart(int index)
    {
        this.index = index;
        animators[index].SetBool("Run", false);
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 0;
        agent.angularSpeed = 1000;
        agent.acceleration = 100;
        agent.speed = 5;
        SetAnim(index);
    }

    public void OnUpdate()
    {


        targetTransform = GameDirector.i.gameManager.humanManager.GetTargetTransform(transform.position);
        if (targetTransform)
        {
            agent.SetDestination(targetTransform.position);
        }
    }

    void SetAnim(int index)
    {
        for (int i = 0; i < animators.Length; i++)
        {
            animators[i].gameObject.SetActive(i == index);
        }
        animators[index].SetBool("Run", true);
    }
}
