using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CloseWeaponController : MonoBehaviour
{
    [SerializeField]
    private GameObject Player; //�÷��̾�
    [SerializeField]
    protected CloseWeapon[] CloseWeapons; // ���� ������ ���� ���� 
    [SerializeField]
    protected CloseWeapon currentCloseWeapon; // ���� ������ ���� ���� 

    [SerializeField]
    protected int damage; // ���� ������ ���� ���� 

    protected bool isAttack = false;  // ���� ���� ������ 
    protected bool isSwing = false;  // ���� �ֵθ��� ������. isSwing = True �� ���� �������� ������ ���̴�.

    protected RaycastHit hitInfo;  // ���� ����(Hand)�� ���� �͵��� ����.

    protected void TryAttack()
    {
        if (!Inventory.invectoryActivated)
        {
            if (Input.GetButton("Fire1"))
            {
                if (!isAttack)
                {
                    StartCoroutine(AttackCoroutine());
                }
            }
        }
    }

    protected IEnumerator AttackCoroutine()
    {
        isAttack = true;
        if(currentCloseWeapon.anim)
        {
            currentCloseWeapon.anim.SetTrigger("Attack");
        }

        yield return new WaitForSeconds(currentCloseWeapon.attackDelayA);
        isSwing = true;

        StartCoroutine(HitCoroutine());

        yield return new WaitForSeconds(currentCloseWeapon.attackDelayB);
        isSwing = false;

        yield return new WaitForSeconds(currentCloseWeapon.attackDelay - currentCloseWeapon.attackDelayA - currentCloseWeapon.attackDelayB);
        isAttack = false;
    }

    protected bool CheckObject()
    {
        Ray ray = new Ray(Player.transform.position, Player.transform.forward);
        int layerMask = 1 << LayerMask.NameToLayer("Ignore Raycast");
        Debug.DrawRay(ray.origin, ray.direction * currentCloseWeapon.range, Color.red, 5f);
        if (Physics.Raycast(ray.origin, ray.direction, out hitInfo, currentCloseWeapon.range, ~layerMask))
        {
            return true;
        }

        return false;
    }

    protected abstract IEnumerator HitCoroutine();

    protected abstract void ChangDamage();

    public virtual void CloseWeaponChange(CloseWeapon _closeWeapon)
    {
        if (WeaponManager.currentWeapon != null)
            WeaponManager.currentWeapon.gameObject.SetActive(false);

        for(int i = 0; i < CloseWeapons.Length; i++)
        {
            if(CloseWeapons[i].closeWeaponName == _closeWeapon.closeWeaponName)
            {
                currentCloseWeapon = CloseWeapons[i];
            }
        }

        currentCloseWeapon = _closeWeapon;
        WeaponManager.currentWeapon = currentCloseWeapon.GetComponent<Transform>();
        if(currentCloseWeapon.anim)
        {
            WeaponManager.currentWeaponAnim = currentCloseWeapon.anim;
        }

        //currentCloseWeapon.transform.localPosition = Vector3.zero;
        currentCloseWeapon.gameObject.SetActive(true);
    }
}