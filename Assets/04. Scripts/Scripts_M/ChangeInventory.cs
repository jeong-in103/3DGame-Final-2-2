using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeInventory : MonoBehaviour
{
    [SerializeField]
    private Transform inventory;  // Slot들의 부모인 Grid Setting1
    [SerializeField]
    private Transform toolInventory;  // WeaponSlot들의 부모인 Grid Setting2

    public void ChangeParentToInventory()
    {
        this.transform.SetParent(inventory, false);
    }

    public void ChangeParentToToolInventory()
    {
        this.transform.SetParent(toolInventory, false);
    }
}
