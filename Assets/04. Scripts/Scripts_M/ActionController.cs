using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range;  // 아이템 습득이 가능한 최대 거리

    private bool pickupActivated = false;  // 아이템 습득 가능할시 True 

    private bool openActivated = false;  // 상자 열기 가능할시 True 

    private RaycastHit hitInfo;  // 충돌체 정보 저장

    [SerializeField]
    private LayerMask layerMask1;  // 특정 레이어를 가진 오브젝트에 대해서만 습득할 수 있어야 한다.

    [SerializeField]
    private LayerMask layerMask2;  // 특정 레이어를 가진 오브젝트에 대해서만 습득할 수 있어야 한다.

    [SerializeField]
    private Text actionText;  // 행동을 보여 줄 텍스트

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
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " 획득 " + "<color=yellow>" + "(E)" + "</color>";
    }

    private void BoxInfoAppear()
    {
        openActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = " 코인 10개를 소모하여 열기 " + "<color=yellow>" + "(E)" + "</color>";
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
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " 획득 했습니다.");
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
