using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateBase : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public EnemyStateMachine machine;
    [HideInInspector] public Animator anim;

    private void Awake()
    {
        machine = GetComponent<EnemyStateMachine>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
}
