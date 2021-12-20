using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool invectoryActivated = false;  // 인벤토리 활성화 여부. true가 되면 카메라 움직임과 다른 입력을 막을 것이다.

    [SerializeField]
    private WeaponImageUpdate sub_Inventory; // sub 인벤토리

    [SerializeField]
    private GameObject go_InventoryBase; // Inventory_Base 이미지
    [SerializeField]
    private GameObject go_SlotsParent;  // Slot들의 부모인 Grid Setting1
    [SerializeField]
    private GameObject go_SlotsParent2;  // WeaponSlot들의 부모인 Grid Setting2
    [SerializeField]
    private GameObject AimImage; // 에임 이미지

    public Slot[] slots;  // 슬롯들 배열
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

    public WeaponSlot ReturnWeaponSlot(int i)
    {
        if (WeaponSlots[i].item != null)
        {
            return WeaponSlots[i];
        }
        else
        {
            return null;
        }
    }

    public WeaponSlot[] ReturnWeaponSlot()
    {
        return WeaponSlots;
    }

    public Slot[] ReturnSlot()
    {
        return slots;
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            invectoryActivated = !invectoryActivated;

            if (invectoryActivated)
                OpenInventory();
            else
                CloseInventory();

        }
    }

    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
        AimImage.SetActive(false);
    }

    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
        AimImage.SetActive(true);
    }

    public void AcquireItem(Item _item, int _count = 1)
    {
        if (Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)  // null 이라면 slots[i].item.itemName 할 때 런타임 에러 나서
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }

    public void AcquireWeaponItemToItem(WeaponSlot _item)
    {
        if (_item.itemImage.color.a == 0)
        {
            return;
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item.item);
                _item.ClearSlot();
                sub_Inventory.UpdateImage(WeaponSlots);
                return;
            }
        }
    }

    public void AcquireWeaponItem(Slot _item)
    {
        if (_item.itemButton.color.a == 0)
        {
            return;
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (WeaponSlots[i].item == null)
            {
                WeaponSlots[i].AddItem(_item.item);
                sub_Inventory.UpdateImage(WeaponSlots);
                _item.ClearSlot();
                return;
            }
        }
    }
}