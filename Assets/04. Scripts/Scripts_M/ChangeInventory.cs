using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeInventory : MonoBehaviour
{
    [SerializeField]
    private Transform inventory;  // Slot���� �θ��� Grid Setting1
    [SerializeField]
    private Transform toolInventory;  // WeaponSlot���� �θ��� Grid Setting2

    public void ChangeParentToInventory()
    {
        this.transform.SetParent(inventory, false);
    }

    public void ChangeParentToToolInventory()
    {
        this.transform.SetParent(toolInventory, false);
    }
}
