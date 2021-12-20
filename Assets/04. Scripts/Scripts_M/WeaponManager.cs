using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static bool isChangeWeapon = false;  // ���� ��ü �ߺ� ���� ����. (True�� ���ϰ�)

    [SerializeField]
    private float changeweaponDelayTime;  // ���� ��ü ������ �ð�. ���� ������ ���� �� ���� �ִ� �� �ð�. �뷫 Weapon_Out �ִϸ��̼� �ð�.
    [SerializeField]
    private float changeweaponEndDelayTime;  // ���� ��ü�� ������ ���� ����. �뷫 Weapon_In �ִϸ��̼� �ð�.

    [SerializeField]
    private CloseWeapon[] hands;  //  Hand �� ���⸦ ������ �迭
    [SerializeField]
    private CloseWeapon[] axes;  // ���� �� ���⸦ ������ �迭
    [SerializeField]
    private CloseWeapon[] pickaxes;  // ��� �� ���⸦ ������ �迭
    [SerializeField]
    private CloseWeapon[] swords; // Į �� ���⸦ ������ �迭

    // ���� �������� �̸����� ���� ���� ������ �����ϵ��� Dictionary �ڷ� ���� ���.
    private Dictionary<string, CloseWeapon> handDictionary = new Dictionary<string, CloseWeapon>();
    private Dictionary<string, CloseWeapon> axeDictionary = new Dictionary<string, CloseWeapon>();
    private Dictionary<string, CloseWeapon> pickaxeDictionary = new Dictionary<string, CloseWeapon>();
    private Dictionary<string, CloseWeapon> swordDictionary = new Dictionary<string, CloseWeapon>();

    [SerializeField]
    private string currentWeaponType;  // ���� ������ Ÿ�� (��, ���� ���)
    public static Transform currentWeapon;  // ���� ����. static���� �����Ͽ� ���� ��ũ��Ʈ���� Ŭ���� �̸����� �ٷ� ������ �� �ְ� ��.
    public static Animator currentWeaponAnim; // ���� ������ �ִϸ��̼�. static���� �����Ͽ� ���� ��ũ��Ʈ���� Ŭ���� �̸����� �ٷ� ������ �� �ְ� ��.

    [SerializeField]
    private HandController theHandController; // �� �϶� HandController.cs Ȱ��ȭ, �ٸ� ������ �� HandController.cs ��Ȱ��ȭ
    [SerializeField]
    private AxeController theAxeController; // ���� �϶� AxeController.cs Ȱ��ȭ, �ٸ� ������ �� AxeController.cs ��Ȱ��ȭ
    [SerializeField]
    private PickaxeController thePickaxeController; // ��� �϶� PickaxeController.cs Ȱ��ȭ, �ٸ� ������ �� PickaxeController.cs ��Ȱ��ȭ
    [SerializeField]
    private SwordController theSwordController; // Į �϶� SwordController.cs Ȱ��ȭ, �ٸ� ������ �� SwordController.cs ��Ȱ��ȭ

    [SerializeField]
    private Inventory theInventory;

    void Start()
    {
        for (int i = 0; i < hands.Length; i++)
        {
            handDictionary.Add(hands[i].closeWeaponName, hands[i]);
        }
        for (int i = 0; i < axes.Length; i++)
        {
            axeDictionary.Add(axes[i].closeWeaponName, axes[i]);
        }
        for (int i = 0; i < pickaxes.Length; i++)
        {
            pickaxeDictionary.Add(pickaxes[i].closeWeaponName, pickaxes[i]);
        }
        for (int i = 0; i < swords.Length; i++)
        {
            swordDictionary.Add(swords[i].closeWeaponName, swords[i]);
        }
    }

    void Update()
    {
        if (!Inventory.invectoryActivated)
        {
            if (!isChangeWeapon)
            {
                // �κ��丮�� �����Ͽ� ���� ��ü ����
                if (Input.GetKeyDown(KeyCode.Alpha1)) 
                    StartCoroutine(ChangeWeaponCoroutine(returnType(theInventory.ReturnWeaponSlot(0)), 
                        theInventory.ReturnWeaponSlot(0) == null ? "Hand" : theInventory.ReturnWeaponSlot(0).item.itemName));
                else if (Input.GetKeyDown(KeyCode.Alpha2)) 
                    StartCoroutine(ChangeWeaponCoroutine(returnType(theInventory.ReturnWeaponSlot(1)), 
                        theInventory.ReturnWeaponSlot(1) == null ? "Hand" : theInventory.ReturnWeaponSlot(1).item.itemName));
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                    StartCoroutine(ChangeWeaponCoroutine(returnType(theInventory.ReturnWeaponSlot(2)), 
                        theInventory.ReturnWeaponSlot(2) == null ? "Hand" : theInventory.ReturnWeaponSlot(2).item.itemName));
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                    StartCoroutine(ChangeWeaponCoroutine(returnType(theInventory.ReturnWeaponSlot(3)), 
                        theInventory.ReturnWeaponSlot(3) == null ? "Hand" : theInventory.ReturnWeaponSlot(3).item.itemName));
                else if (Input.GetKeyDown(KeyCode.Alpha5))
                    StartCoroutine(ChangeWeaponCoroutine(returnType(theInventory.ReturnWeaponSlot(4)), 
                        theInventory.ReturnWeaponSlot(4) == null ? "Hand" : theInventory.ReturnWeaponSlot(4).item.itemName));
                else if (Input.GetKeyDown(KeyCode.Alpha6))
                    StartCoroutine(ChangeWeaponCoroutine(returnType(theInventory.ReturnWeaponSlot(5)), 
                        theInventory.ReturnWeaponSlot(5) == null ? "Hand" : theInventory.ReturnWeaponSlot(5).item.itemName));
                else if (Input.GetKeyDown(KeyCode.Alpha7))
                    StartCoroutine(ChangeWeaponCoroutine(returnType(theInventory.ReturnWeaponSlot(6)), 
                        theInventory.ReturnWeaponSlot(6) == null ? "Hand" : theInventory.ReturnWeaponSlot(6).item.itemName));
            }
        }
    }

    private string returnType(WeaponSlot weapon)
    {
        string type;
        if (weapon != null)
            type = weapon.item.weaponType;
        else
            type = "Hand";

        switch (type)
        {
            case "Hand":
                return "HAND";
            case "Axe":
                return "AXE";
            case "Sword":
                return "SWORD";
            case "PickAxe":
                return "PICKAXE";
        }

        return null;
    }

    public IEnumerator ChangeWeaponCoroutine(string _type, string _name)
    {
        isChangeWeapon = true;
        //currentWeaponAnim.SetTrigger("Weapon_Out");
        yield return new WaitForSeconds(changeweaponDelayTime);

        CancelPreWeaponAction();
        WeaponChange(_type, _name);

        yield return new WaitForSeconds(changeweaponEndDelayTime);

        currentWeaponType = _type;
        isChangeWeapon = false;
    }

    private void CancelPreWeaponAction()
    {
        switch (currentWeaponType)
        {
            case "HAND":
                HandController.isActivate = false;
                break;
            case "AXE":
                AxeController.isActivate = false;
                break;
            case "PICKAXE":
                PickaxeController.isActivate = false;
                break;
            case "SWORD":
                SwordController.isActivate = false;
                break;
        }
    }

    private void WeaponChange(string _type, string _name)
    {
        if (_type == "HAND")
        {
            theHandController.CloseWeaponChange(handDictionary[_name]);
        }
        else if (_type == "AXE")
        {
            theAxeController.CloseWeaponChange(axeDictionary[_name]);
        }
        else if (_type == "PICKAXE")
        {
            theAxeController.CloseWeaponChange(pickaxeDictionary[_name]);
        }
        else if (_type == "SWORD")
        {
            theAxeController.CloseWeaponChange(swordDictionary[_name]);
        }
    }
}