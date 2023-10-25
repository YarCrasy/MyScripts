using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyStateBase
{
    [SerializeField] PlayerDetector pDetector;
    [SerializeField] EntityStatsScriptable stats;

    private void OnEnable()
    {
        agent.speed = stats.movementSpeed * stats.movementMultiplier;
        anim.SetTrigger("Chase");
        agent.SetDestination(pDetector.playerTsf.position);
    }

    private void Update()
    {
        if (!pDetector.detected)
        {
            machine.SetState(EnemyStates.idle);
        }
        else
        {
            if (agent.remainingDistance > 0)
            {
                agent.SetDestination(pDetector.playerTsf.position);
            }
        }
    }

}
