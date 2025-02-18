using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    // 체력
    [SerializeField]
    private int hp;  // 최대 체력. 유니티 에디터 슬롯에서 지정할 것.
    private int currentHp;

    // 스태미나
    [SerializeField]
    private int sp;  // 최대 스태미나. 유니티 에디터 슬롯에서 지정할 것.
    private int currentSp;

    // 스태미나 증가량
    [SerializeField]
    private int spIncreaseSpeed;

    // 스태미나 재회복 딜레이 시간
    [SerializeField]
    private int spRechargeTime;
    private int currentSpRechargeTime;

    // 스태미나 감소 여부
    private bool spUsed;
    public bool cantUseSp;

    public int coin = 0;

    // 필요한 이미지
    [SerializeField]
    private Image[] images_Gauge;

    [SerializeField]
    private Text coinText; // 코인 개수

    [SerializeField]
    private Gameover gameOver;

    // 각 상태를 대표하는 인덱스
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

    public void SetHP(int _hp)
    {
        hp += _hp;
        currentHp += _hp;
    }

    public void SetSP(int _sp)
    {
        sp += _sp;
        currentSp += _sp;
    }

    private void GaugeUpdate()
    {
        images_Gauge[HP].fillAmount = (float)currentHp / hp;
        images_Gauge[SP].fillAmount = (float)currentSp / sp;
        coinText.text = coin.ToString();
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
        {
            Debug.Log("게임오버");
            gameOver.GameOver();
        }
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