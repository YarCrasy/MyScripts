using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyStateBase
{
    [SerializeField] AttackDetector aDetector;

    [SerializeField] EntityStatsScriptable stats;
    PlayerHealth p;

    private void OnEnable()
    {
        anim.SetTrigger("Attack");
    }

    public void CheckAttackValid()
    {
        if (aDetector.player.TryGetComponent(out p))
        {
            if (aDetector.detected)
            {
                p.ReceiveDamage(stats.attackPower);
            }
        }

    }

}
