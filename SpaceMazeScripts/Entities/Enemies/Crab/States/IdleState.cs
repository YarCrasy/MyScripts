using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyStateBase
{
    float timer = 0f, maxTime = 5;

    private void OnEnable()
    {
        timer = 0;
        anim.SetTrigger("Idle");
    }

    private void Update()
    {
        if (timer < maxTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            machine.SetState(EnemyStates.patrol);
        }
    }

}
