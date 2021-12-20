using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolInventory : MonoBehaviour
{
    public static bool ToolInvectoryActivated = false;

    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private GameObject go_InventoryBase; // Inventory_Base 이미지
    [SerializeField]
    private GameObject go_SlotsParent;  // Slot들의 부모인 Grid Setting1
    [SerializeField]
    private GameObject go_SlotsParent2;  // Slot들의 부모인 Grid Setting1

    private Slot[] slots;  // 슬롯들 배열
    private WeaponSlot[] WeaponSlots;  // 슬롯들 배열

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        WeaponSlots = go_SlotsParent2.GetComponentsInChildren<WeaponSlot>();
    }

    void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToolInvectoryActivated = !ToolInvectoryActivated;

            if (ToolInvectoryActivated)
                OpenInventory();
            else
                CloseInventory();

        }
    }

    private void OpenInventory()
    {
        SlotUpdate();
        go_InventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
    }

    public void SlotUpdate()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].item = inventory.ReturnSlot()[i].item;
            slots[i].itemCount = inventory.ReturnSlot()[i].itemCount;
        }

        for (int i = 0; i < WeaponSlots.Length; i++)
        {
            WeaponSlots[i].item = inventory.ReturnWeaponSlot()[i].item;
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].item != null)
            {
                slots[i].AddItem(slots[i].item, slots[i].itemCount);
                slots[i].SetSlotCount(0);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }

        for (int i = 0; i < WeaponSlots.Length; i++)
        {
            if (WeaponSlots[i].item != null)
            {
                WeaponSlots[i].AddItem(WeaponSlots[i].item);
            }
            else
            {
                WeaponSlots[i].ClearSlot();
            }
        }
    }
}
