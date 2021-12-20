using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : CloseWeaponController
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
            case "WoodSword":
                damage = 20;
                break;
            case "RockSword":
                damage = 30;
                break;
            case "IronSword":
                damage = 50;
                break;
            case "IceSword":
                damage = 45;
                break;
            case "FireSword":
                damage = 40;
                break;
        }
    }
    protected override IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            if (CheckObject())
            {
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