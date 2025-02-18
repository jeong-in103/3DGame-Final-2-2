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
        public Item ingredient; //필요한 재료
        public int count;       //필요한 재료의 개수
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
        int canMakeCount = 0;
        int[] slotIngredient = new int[ingredients.Length];
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
                        canMakeCount++;
                        slotIngredient[j] = i;
                    }
                }
            }
        }

        if(canMakeCount == ingredients.Length)
        {
            Debug.Log(item + "제작");
            inventory.AcquireItem(item);
            for(int i = 0; i < slotIngredient.Length; i++)
            {
                inventory.slots[slotIngredient[i]].SetSlotCount(-ingredients[i].count);
            }
        }

        theToolInventory.SlotUpdate();
    }
}
