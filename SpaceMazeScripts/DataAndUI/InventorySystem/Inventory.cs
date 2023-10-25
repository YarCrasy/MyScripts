using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject selection;
    int selectionIndex = 0;

    [SerializeField] GameObject[] slots;
    public GameObject[] itemsOnHandPreset;
    public GameObject[] itemsImgPreset; 

    public delegate void ItemOnHand();
    public event ItemOnHand UseItemReleased;
    public event ItemOnHand ReloadItemReleased;

    GameObject[] collectedItems, ItemImgDisplay;
    int lastOnHandIndex = 0, allCollectedIndex = 0;

    private void Awake()
    {
        collectedItems = new GameObject[slots.Length];
        ItemImgDisplay = new GameObject[slots.Length];
    }

    private void Update()
    {
        ItemSelection();
        UseItem();
    }

    void ItemSelection()
    {
        if (ScrollInput() || NumInput())
        {
            SetItemOnHand();
        }
    }

    void SetItemOnHand()
    {
        selection.transform.position = slots[selectionIndex].transform.position;
        if (collectedItems[lastOnHandIndex] != null)
            collectedItems[lastOnHandIndex].SetActive(false);

        if (collectedItems[selectionIndex] != null)
            collectedItems[selectionIndex].SetActive(true);
        lastOnHandIndex = selectionIndex;
    }

    bool ScrollInput()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selectionIndex != 0) selectionIndex--;
            else selectionIndex = slots.Length - 1;
            return true;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectionIndex != slots.Length - 1) selectionIndex++;
            else selectionIndex = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    bool NumInput()
    {
        bool find = false;
        if (Input.anyKeyDown)
        {
            for (int i = 0; i < slots.Length && !find; i++)
            {
                if (Input.GetKeyDown("" + (i + 1)) || Input.GetKeyDown("[" + (i + 1) + "]"))
                {
                    find = true;
                    selectionIndex = i;
                }
            }
        }
        return find;
    }

    void UseItem()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            UseItemReleased?.Invoke();
        }
        else if (Input.GetButton("Reload"))
        {
            ReloadItemReleased?.Invoke();
        }
    }

    public void AddItem(int itemID)
    {
        selectionIndex = allCollectedIndex;
        collectedItems[allCollectedIndex] = itemsOnHandPreset[itemID];
        ItemImgDisplay[allCollectedIndex] = Instantiate(itemsImgPreset[itemID], slots[allCollectedIndex].transform);
        allCollectedIndex++;
        SetItemOnHand();
    }

}
