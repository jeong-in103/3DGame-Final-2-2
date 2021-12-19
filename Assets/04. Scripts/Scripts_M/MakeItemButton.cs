using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MakeItemButton : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;

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

    public void MakeItem()
    {
        Slot[] slot = inventory.ReturnSlot();
        for (int j = 0; j < ingredients.Length; j++)
        {
            for (int i = 0; i < slot.Length; i++)
            {
                if (slot[i].item.itemName == ingredients[j].ingredient.itemName)
                {
                    if(slot[i].itemCount == ingredients[j].count)
                    {
                        inventory.AcquireItem(item);
                        slot[i].SetSlotCount(ingredients[j].count);
                    }
                }
            }
        }
    }
}
