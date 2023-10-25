using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorSetter : MonoBehaviour
{
    public static ExitDoorSetter instance;
    [SerializeField] Animator anim;
    [SerializeField] MeshRenderer exitLightMesh;
    [SerializeField] Light exitLight;
    [SerializeField] Material openMat, closeMat;
    [SerializeField] BoxCollider interationTrigger;
    [SerializeField] AudioSource aud;

    bool opened = false;

    Interactor interact;

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject colObject = other.gameObject;
        if(colObject.TryGetComponent(out interact))
        {
            interact.interactReleased += SetDoorState;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject colObject = other.gameObject;
        if (colObject.layer == 6)
        {
            interact.interactReleased -= SetDoorState;
        }
    }

    public void SetDoorState()
    {
        if(opened)Close();
        else Open();
    }

    public void Open()
    {
        aud.Play();
        opened = true;
        anim.SetBool("OpenDoor", opened);
        exitLightMesh.material = openMat;
        exitLight.color = Color.green;
    }

    public void Close() 
    {
        aud.Play();
        opened = false;
        anim.SetBool("OpenDoor", opened);
        exitLightMesh.material = closeMat;
        exitLight.color = Color.red;
    }

    public void SetTriggerActive(bool set)
    {
        interationTrigger.enabled = set;
    }
}
