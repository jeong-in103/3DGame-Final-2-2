using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponImageUpdate : MonoBehaviour
{
    [SerializeField]
    private GameObject go_SlotsParent;  // Slot들의 부모인 Grid Setting1
    [SerializeField]
    private GameObject go_InventoryBase;  // Slot들의 부모인 Grid Setting1

    [SerializeField]
    private Image[] slots;

    private void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Image>();
    }

    private void Update()
    {
        if(Inventory.invectoryActivated || ToolInventory.ToolInvectoryActivated)
        {
            go_InventoryBase.SetActive(false);
        }
        else
        {
            go_InventoryBase.SetActive(true);
        }
    }

    public void UpdateImage(WeaponSlot[] WeaponSlots)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(WeaponSlots[i].GetComponentInChildren<Image>().color.a != 0)
            {
                slots[i].sprite = WeaponSlots[i].GetComponentInChildren<Image>().sprite;
                Color color = slots[i].color;
                color.a = 1;
                slots[i].color = color;
            }
            else
            {
                Color color = slots[i].color;
                color.a = 0;
                slots[i].color = color;
            }
        }
    }
}
