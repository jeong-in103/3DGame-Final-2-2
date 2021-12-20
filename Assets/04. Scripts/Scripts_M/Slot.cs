using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
{
    public Item item; // 획득한 아이템
    public int itemCount; // 획득한 아이템의 개수
    public Image itemImage;  // 아이템의 이미지
    public Image itemButton;  // 아이템의 버튼

    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    private WeaponManager theWeaponManager;

    void Start()
    {
        theWeaponManager = FindObjectOfType<WeaponManager>();
    }

    // 아이템 이미지의 투명도 조절
    private void SetColor(float _alpha)
    {
        if(item == null)
        {
            Color color1 = itemImage.color;
            color1.a = _alpha;
            itemImage.color = color1;

            Color color2 = itemButton.color;
            color2.a = _alpha;
            itemButton.color = color2;
            return;
        }

        if(item.itemType != Item.ItemType.Equipment)
        {
            Color color1 = itemImage.color;
            color1.a = _alpha;
            itemImage.color = color1;
        }
        else
        {
            Color color2 = itemButton.color;
            color2.a = _alpha;
            itemButton.color = color2;
        }
    }

    // 인벤토리에 새로운 아이템 슬롯 추가
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;
        itemButton.sprite = item.itemImage;

        if (item.itemType != Item.ItemType.Equipment)
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
        }

        SetColor(1);
    }

    // 해당 슬롯의 아이템 갯수 업데이트
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    // 해당 슬롯 하나 삭제
    public void ClearSlot()
    {
        if(itemImage.color.a == 0)
        {
            itemButton.sprite = null;
            SetColor(0);
            item = null;
            return;
        }

        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);
        item = null;

        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }
}