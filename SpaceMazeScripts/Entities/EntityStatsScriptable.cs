using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EntityStatsScriptable : ScriptableObject
{
    public float maxRayDistance = 25;
    [Range(1, 10)] public float movementSpeed;
    [Range(1, 500)] public int attackPower, hp;
    [Range(100, 1000)] public float jumpForce;
    [Range(1, 10)] public float movementMultiplier;

}
