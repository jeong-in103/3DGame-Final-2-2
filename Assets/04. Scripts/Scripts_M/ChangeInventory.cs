using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeInventory : MonoBehaviour
{
    [SerializeField]
    private Transform inventory;  // Slot들의 부모인 Grid Setting1
    [SerializeField]
    private Transform toolInventory;  // WeaponSlot들의 부모인 Grid Setting2

    private Vector3 pos;

    private void Start()
    {
        pos = this.transform.position;
    }

    public void ChangeParentToInventory()
    {
        this.transform.parent = inventory.transform;
        //this.transform.localPosition = pos + new Vector3(0, 0, 0);
    }

    public void ChangeParentToToolInventory()
    {
        this.transform.parent = toolInventory.transform;
        //this.transform.localPosition = pos + new Vector3(0, -140, 0);
    }
}
