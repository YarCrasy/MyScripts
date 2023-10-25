using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetector : MonoBehaviour
{
    const float ATTACK_DELAY = 1.5f;

    [SerializeField] EnemyStateMachine machine;

    public GameObject player;
    public bool detected = false;

    float timer = ATTACK_DELAY;

    private void OnTriggerStay(Collider other)
    {
        GameObject colObject = other.gameObject;
        if (colObject.layer == 6)
        {
            timer += Time.deltaTime;
            if (timer >= ATTACK_DELAY)
            {
                player = colObject;
                detected = true;
                machine.SetState(EnemyStates.attack);
                timer = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject colObject = other.gameObject;
        if (colObject.layer == 6)
        {
            timer = 0;
            detected = false;
        }
    }

}
