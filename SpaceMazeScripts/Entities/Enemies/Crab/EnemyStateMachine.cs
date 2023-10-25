using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStates { patrol, idle, chase, attack, damage}

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] EnemyStateBase[] states;

    public EnemyStates lastState, actualState;

    private void Awake()
    {
        actualState = EnemyStates.patrol;
        SetState(EnemyStates.idle);
    }

    public void SetState(EnemyStates state)
    {
        if(actualState != state)
        {
            lastState = actualState;
            actualState = state; 
            for (int i = 0; i < states.Length; i++)
            {
                if (i == (int)state)
                {
                    states[i].enabled = true;
                }
                else
                {
                    states[i].enabled = false;
                }
            }
        }
    }

}
