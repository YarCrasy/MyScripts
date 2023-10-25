using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPointer : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    [SerializeField] EntityStatsScriptable pStats;
    [SerializeField] Camera playerCam;
    Image pointer;

    Ray ray;
    RaycastHit hit;

    private void Awake()
    {
        pointer = GetComponent<Image>();
    }

    private void Update()
    {
        ray = new(playerCam.transform.position, playerCam.transform.forward);
        if (Physics.Raycast(ray.origin, ray.direction, out hit, pStats.maxRayDistance, ~mask))
        {
            GameObject hitObject = hit.collider.gameObject;
            //Debug.DrawRay(ray.origin, ray.direction * pStats.maxRayDistance, Color.green);
            if (hitObject.layer == 8 || hitObject.layer == 7)
            {
                pointer.enabled = true;
            }
            else
            {
                pointer.enabled = false;
            }
        }
    }

}
