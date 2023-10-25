using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recollectable : MonoBehaviour
{
    readonly float maxFloatingDistance = 0.25f, floatingSpeed = 0.25f, rotationSpeed = 50f;
    Vector3 originalPos;
    bool goingDown = false;

    [SerializeField] int itemID;

    Interactor interact;
    Inventory inventory;

    private void Awake()
    {
        originalPos = transform.position;
    }

    void Update()
    {
        Floating();
        Rotating();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject colObject = other.gameObject;
        if (colObject.TryGetComponent(out interact))
        {
            inventory = interact.GetInventory();
            interact.interactReleased += AddToInventory;
        }
    }

    void Floating()
    {
        if (goingDown)
        {
            transform.Translate(Time.deltaTime * floatingSpeed * Vector3.down);
            if (transform.position.y <= originalPos.y - maxFloatingDistance) goingDown = false;
        }
        else
        {
            transform.Translate(Time.deltaTime * floatingSpeed * Vector3.up);
            if (transform.position.y >= originalPos.y + maxFloatingDistance) goingDown = true;
        }
    }

    void Rotating()
    {
        transform.Rotate(Time.deltaTime * rotationSpeed * Vector3.up);
    }

    public void AddToInventory()
    {
        inventory.AddItem(itemID); 
        interact.interactReleased -= AddToInventory;
    }

}
