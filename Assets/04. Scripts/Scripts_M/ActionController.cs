using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range;  // ������ ������ ������ �ִ� �Ÿ�

    private bool pickupActivated = false;  // ������ ���� �����ҽ� True 

    private bool openActivated = false;  // ���� ���� �����ҽ� True 

    private RaycastHit hitInfo;  // �浹ü ���� ����

    [SerializeField]
    private LayerMask layerMask1;  // Ư�� ���̾ ���� ������Ʈ�� ���ؼ��� ������ �� �־�� �Ѵ�.

    [SerializeField]
    private LayerMask layerMask2;  // Ư�� ���̾ ���� ������Ʈ�� ���ؼ��� ������ �� �־�� �Ѵ�.

    [SerializeField]
    private Text actionText;  // �ൿ�� ���� �� �ؽ�Ʈ

    [SerializeField]
    private Inventory theInventory;  // Inventory.cs

    [SerializeField]
    private StatusController theStatusController; // StatusController.cs

    void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            CanPickUp();
            CanOpen();
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask1))
        {
            if (hitInfo.transform.tag == "Item")
            {
                ItemInfoAppear();
            }
        }
        else if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask2))
        {
            if (hitInfo.transform.tag == "Box")
            {
                BoxInfoAppear();
            }
        }
        else
        {
            ItemInfoDisappear();
            BoxInfoDisappear();
        }
    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " ȹ�� " + "<color=yellow>" + "(E)" + "</color>";
    }

    private void BoxInfoAppear()
    {
        openActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = " ���� 10���� �Ҹ��Ͽ� ���� " + "<color=yellow>" + "(E)" + "</color>";
    }

    private void ItemInfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }

    private void BoxInfoDisappear()
    {
        openActivated = false;
        actionText.gameObject.SetActive(false);
    }

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (hitInfo.transform != null)
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " ȹ�� �߽��ϴ�.");
                theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                Destroy(hitInfo.transform.gameObject);
                ItemInfoDisappear();
            }
        }
    }

    private void CanOpen()
    {
        if(openActivated)
        {
            if(hitInfo.transform != null)
            {
                if (theStatusController.coin >= 10)
                {
                    hitInfo.transform.GetComponentInChildren<BonusBox>().OpenBox();
                    theStatusController.coin -= 10;
                    BoxInfoDisappear();
                    hitInfo.transform.gameObject.layer = 0;
                }
            }
        }
    }
}
