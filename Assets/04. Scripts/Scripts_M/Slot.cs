using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
{
    public Item item; // ȹ���� ������
    public int itemCount; // ȹ���� �������� ����
    public Image itemImage;  // �������� �̹���
    public Image itemButton;  // �������� ��ư

    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    private WeaponManager theWeaponManager;

    void Start()
    {
        theWeaponManager = FindObjectOfType<WeaponManager>();
    }

    // ������ �̹����� ���� ����
    private void SetColor(float _alpha)
    {
        if (item.itemType != Item.ItemType.Equipment)
        {
            Color color = itemImage.color;
            color.a = _alpha;
            itemImage.color = color;
        }
        else
        {
            Color color = itemButton.color;
            color.a = _alpha;
            itemButton.color = color;
        }
    }

    // �κ��丮�� ���ο� ������ ���� �߰�
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

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

    // �ش� ������ ������ ���� ������Ʈ
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    // �ش� ���� �ϳ� ����
    public void ClearSlot()
    {
        if (itemButton.color.a == 0)
        {
            return;
        }
        itemCount = 0;
        itemImage.sprite = null;
        itemButton.sprite = null;
        SetColor(0);
        item = null;

        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }
}