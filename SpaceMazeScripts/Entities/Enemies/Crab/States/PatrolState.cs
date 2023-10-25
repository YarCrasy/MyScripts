using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyStateBase
{
    Vector3 patrolPoint = Vector3.zero;
    float auxX, auxZ;
    [SerializeField] EntityStatsScriptable stats;


    private void OnEnable()
    {
        agent.speed = stats.movementSpeed;
        SetDestination();
        anim.SetTrigger("Patrol");
    }

    private void Update()
    {
        if (agent.remainingDistance < 0.5f && !agent.pathPending)
        {
            machine.SetState(EnemyStates.idle);
        }
    }

    void SetDestination()
    {
        auxX = MazeGenerator.instance.MazeRandomWorldPosition();
        auxZ = MazeGenerator.instance.MazeRandomWorldPosition();

        patrolPoint.x = auxX;
        patrolPoint.z = auxZ;
        agent.SetDestination(patrolPoint);

    }

}
