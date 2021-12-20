using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeItemButton : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private ToolInventory theToolInventory;

    [SerializeField]
    private Item item;

    [Serializable]
    public struct ItemIngerdient
    {
        public Item ingredient; //�ʿ��� ���
        public int count;       //�ʿ��� ����� ����
    }

    [SerializeField]
    private ItemIngerdient[] ingredients;

    private void Start()
    {
        if(item != null)
            this.gameObject.GetComponent<Image>().sprite = item.itemImage;
    }
    public void MakeItem()
    {
        for (int j = 0; j < ingredients.Length; j++)
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if(inventory.slots[i].item == null)
                {
                    continue;
                }

                if (inventory.slots[i].item.itemName == ingredients[j].ingredient.itemName)
                {
                    if(inventory.slots[i].itemCount >= ingredients[j].count)
                    {
                        Debug.Log(item + "����");
                        inventory.AcquireItem(item);
                        inventory.slots[i].SetSlotCount(-ingredients[j].count);
                    }
                }
            }
        }

        theToolInventory.SlotUpdate();
    }
}
