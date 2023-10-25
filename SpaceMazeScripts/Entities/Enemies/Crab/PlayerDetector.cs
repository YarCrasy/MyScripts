using System.Collections;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] EnemyStateMachine machine;

    public Transform playerTsf;
    public bool detected = false;
    //public bool soundDetected = false;

    public const int RAY_DISTANCE = 50;
    Ray ray;
    RaycastHit hit;
    Vector3 dir;

    private void Start()
    {
        playerTsf = FindObjectOfType<PlayerMove>().transform;
    }

    private void FixedUpdate()
    {   
        dir = playerTsf.position - transform.position;
        dir = dir.normalized;
        ray = new(transform.position, dir);
        if (Physics.Raycast(ray.origin, ray.direction, out hit, RAY_DISTANCE))
        {
            GameObject hitObject = hit.collider.gameObject;
            //Debug.DrawRay(ray.origin, ray.direction * RAY_DISTANCE, Color.red);
            if (hitObject.layer == 6)
            {
                if (detected)
                {
                    machine.SetState(EnemyStates.chase);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject colObject = other.gameObject;
        if (colObject.layer == 6)
        {
            StopCoroutine(WaitForUndetect());
            detected = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        GameObject colObject = other.gameObject;
        if (colObject.layer == 6)
        {
            StartCoroutine(WaitForUndetect());
        }
    }

    IEnumerator WaitForUndetect()
    {
        yield return new WaitForSeconds(3);
        detected = false;
        machine.SetState(EnemyStates.patrol);
    }

}
