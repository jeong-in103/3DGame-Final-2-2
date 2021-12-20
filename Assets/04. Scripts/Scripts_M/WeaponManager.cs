using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static bool isChangeWeapon = false;  // 무기 교체 중복 실행 방지. (True면 못하게)

    [SerializeField]
    private float changeweaponDelayTime;  // 무기 교체 딜레이 시간. 총을 꺼내기 위해 손 집어 넣는 그 시간. 대략 Weapon_Out 애니메이션 시간.
    [SerializeField]
    private float changeweaponEndDelayTime;  // 무기 교체가 완전히 끝난 시점. 대략 Weapon_In 애니메이션 시간.

    [SerializeField]
    private CloseWeapon[] hands;  //  Hand 형 무기를 가지는 배열
    [SerializeField]
    private CloseWeapon[] axes;  // 도끼 형 무기를 가지는 배열
    [SerializeField]
    private CloseWeapon[] pickaxes;  // 곡괭이 형 무기를 가지는 배열
    [SerializeField]
    private CloseWeapon[] swords; // 칼 형 무기를 가지는 배열

    // 관리 차원에서 이름으로 쉽게 무기 접근이 가능하도록 Dictionary 자료 구조 사용.
    private Dictionary<string, CloseWeapon> handDictionary = new Dictionary<string, CloseWeapon>();
    private Dictionary<string, CloseWeapon> axeDictionary = new Dictionary<string, CloseWeapon>();
    private Dictionary<string, CloseWeapon> pickaxeDictionary = new Dictionary<string, CloseWeapon>();
    private Dictionary<string, CloseWeapon> swordDictionary = new Dictionary<string, CloseWeapon>();

    [SerializeField]
    private string currentWeaponType;  // 현재 무기의 타입 (총, 도끼 등등)
    public static Transform currentWeapon;  // 현재 무기. static으로 선언하여 여러 스크립트에서 클래스 이름으로 바로 접근할 수 있게 함.
    public static Animator currentWeaponAnim; // 현재 무기의 애니메이션. static으로 선언하여 여러 스크립트에서 클래스 이름으로 바로 접근할 수 있게 함.

    [SerializeField]
    private HandController theHandController; // 손 일땐 HandController.cs 활성화, 다른 무기일 땐 HandController.cs 비활성화
    [SerializeField]
    private AxeController theAxeController; // 도끼 일땐 AxeController.cs 활성화, 다른 무기일 땐 AxeController.cs 비활성화
    [SerializeField]
    private PickaxeController thePickaxeController; // 곡괭이 일땐 PickaxeController.cs 활성화, 다른 무기일 땐 PickaxeController.cs 비활성화
    [SerializeField]
    private SwordController theSwordController; // 칼 일땐 SwordController.cs 활성화, 다른 무기일 땐 SwordController.cs 비활성화

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
                // 인벤토리와 대응하여 무기 교체 실행
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