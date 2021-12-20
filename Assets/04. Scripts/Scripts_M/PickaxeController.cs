using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeController : CloseWeaponController
{
    // 활성화 여부
    public static bool isActivate = false;

    void Update()
    {
        if (isActivate)
            TryAttack();
    }

    protected override void ChangDamage()
    {
        switch (currentCloseWeapon.closeWeaponName)
        {
            case "WoodPickAxe":
                damage = 10;
                break;
            case "RockPickAxe":
                damage = 20;
                break;
            case "IronPickAxe":
                damage = 25;
                break;
        }
    }

    protected override IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            if (CheckObject())
            {
                if (hitInfo.transform.tag == "Mineral")
                {
                    hitInfo.transform.GetComponent<Rock>().Mining(damage);
                }
                isSwing = false;
                Debug.Log(hitInfo.transform.name);
            }
            yield return null;
        }
    }

    public override void CloseWeaponChange(CloseWeapon _closeWeapon)
    {
        base.CloseWeaponChange(_closeWeapon);
        ChangDamage();
        isActivate = true;
    }
}