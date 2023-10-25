using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyFuse : MonoBehaviour
{
    public static int maxFuseInScene = 1, actualFuseInscene = 0, activatedFuse = 0;

    [SerializeField] MeshRenderer lightMesh;
    [SerializeField] Light powerLight;
    [SerializeField] Material onMat, offMat;
    bool on = false;

    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip lever;

    Interactor interact;

    private void OnTriggerEnter(Collider other)
    {
        GameObject colObject = other.gameObject;
        if (colObject.TryGetComponent(out interact))
        {
            interact.interactReleased += SwitchState;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject colObject = other.gameObject;
        if (colObject.TryGetComponent(out interact))
        {
            interact.interactReleased -= SwitchState;
        }
    }
    public void SwitchState()
    {
        aud.PlayOneShot(lever);
        if (on) SwitchOff();
        else SwitchOn();
    }

    public void SwitchOn()
    {
        on = true;
        lightMesh.material = onMat;
        powerLight.color = Color.green;
        activatedFuse++;
        GoalDisplay.Instance.UpdateDisplay();
        if (activatedFuse >= maxFuseInScene)
        {
            ExitDoorSetter.instance.SetTriggerActive(true);
        }
    }

    public void SwitchOff()
    {
        on = false;
        lightMesh.material = offMat;
        powerLight.color = Color.red;
        activatedFuse--;
        GoalDisplay.Instance.UpdateDisplay();
        if (activatedFuse >= maxFuseInScene)
        {
            ExitDoorSetter.instance.Close();
            ExitDoorSetter.instance.SetTriggerActive(false);
        }
    }
}
