using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] GameObject interactMsg;

    GameObject itemOnFloor;
    EnergyFuse interactingFuse;
    ExitDoorSetter exitDoor;
    bool interactionEnabled = false;

    public delegate void Interact();
    public event Interact interactReleased;

    private void Update()
    {
        if (interactionEnabled && Input.GetButtonDown("Interact"))
        {
            if (itemOnFloor != null)
            {
                interactReleased?.Invoke();
                interactMsg.SetActive(false);
                Destroy(itemOnFloor);
            }
            else if (interactingFuse != null || exitDoor != null)
            {
                interactReleased?.Invoke();
                interactMsg.SetActive(false);
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject colObject = other.gameObject;
        if (colObject.TryGetComponent(out Recollectable item))
        {
            itemOnFloor = colObject;

            interactionEnabled = true;
            interactMsg.SetActive(true);
        }
        else if (colObject.TryGetComponent(out interactingFuse))
        {
            interactionEnabled = true;
            interactMsg.SetActive(true);
        }
        else if (colObject.TryGetComponent(out exitDoor))
        {
            interactionEnabled = true;
            interactMsg.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject colObject = other.gameObject;
        if (colObject.layer == 8)
        {
            interactionEnabled = false;
            interactMsg.SetActive(false);
        }
    }


    public Inventory GetInventory()
    {
        return inventory;
    }

}
