using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    // ü��
    [SerializeField]
    private int hp;  // �ִ� ü��. ����Ƽ ������ ���Կ��� ������ ��.
    private int currentHp;

    // ���¹̳�
    [SerializeField]
    private int sp;  // �ִ� ���¹̳�. ����Ƽ ������ ���Կ��� ������ ��.
    private int currentSp;

    // ���¹̳� ������
    [SerializeField]
    private int spIncreaseSpeed;

    // ���¹̳� ��ȸ�� ������ �ð�
    [SerializeField]
    private int spRechargeTime;
    private int currentSpRechargeTime;

    // ���¹̳� ���� ����
    private bool spUsed;
    public bool cantUseSp;

    // �ʿ��� �̹���
    [SerializeField]
    private Image[] images_Gauge;

    // �� ���¸� ��ǥ�ϴ� �ε���
    private const int HP = 0, SP = 1;

    void Start()
    {
        currentHp = hp;
        currentSp = sp;
    }

    void Update()
    {
        SPRechargeTime();
        SPRecover();
        GaugeUpdate();
    }

    private void GaugeUpdate()
    {
        images_Gauge[HP].fillAmount = (float)currentHp / hp;
        images_Gauge[SP].fillAmount = (float)currentSp / sp;
    }

    public void IncreaseHP(int _count)
    {
        if (currentHp + _count < hp)
            currentHp += _count;
        else
            currentHp = hp;
    }

    public void DecreaseHP(int _count)
    {
        currentHp -= _count;

        if (currentHp <= 0)
            Debug.Log("ĳ������ ü���� 0�� �Ǿ����ϴ�!!");
    }

    public void DecreaseStamina(int _count)
    {
        spUsed = true;
        currentSpRechargeTime = 0;

        if (currentSp - _count > 0)
        {
            currentSp -= _count;
        }
        else
        {
            currentSp = 0;
            cantUseSp = true;
        }     
    }

    private void SPRechargeTime()
    {
        if (spUsed)
        {
            if (currentSpRechargeTime < spRechargeTime)
                currentSpRechargeTime++;
            else
                spUsed = false;
        }
    }

    private void SPRecover()
    {
        if (!spUsed && currentSp < sp)
        {
            currentSp += spIncreaseSpeed;
        }
        else if (currentSp >= sp)
        {
            cantUseSp = false;
        }
    }

    public int GetCurrentSP()
    {
        return currentSp;
    }
}