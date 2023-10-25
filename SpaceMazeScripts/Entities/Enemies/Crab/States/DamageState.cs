using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageState : EnemyStateBase
{
    [SerializeField] EntityStatsScriptable stats;
    [SerializeField] PlayerDetector pDetector;

    private void OnEnable()
    {
        anim.SetTrigger("Damage");
        SetPlayerDetector(false);
    }

    private void OnDisable()
    {
        SetPlayerDetector(true);
    }

    public void SetPlayerDetector(bool set)
    {
        pDetector.enabled = set;
    }
}