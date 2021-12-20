using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusBox : MonoBehaviour
{
    [SerializeField]
    private GameObject particle;

    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private Item[] items;

    [SerializeField]
    private Sprite[] sprite;

    [SerializeField]
    private Text text;
    [SerializeField]
    private Image image;
    [SerializeField]
    private GameObject itemGetUI;
    [SerializeField]
    private StatusController statusController;

    private bool isOpened = false;

    // Start is called before the first frame update
    void Start()
    {
        particle.SetActive(false);
    }

    private void Update()
    {
        if(isOpened && !itemGetUI.activeSelf)
        {
            Destroy(this.gameObject);
        }
    }

    public void OpenBox()
    {
        this.GetComponentInChildren<Animator>().SetTrigger("Open");
        Invoke("ParticleOn", 1f);
        Invoke("OpenUI", 1f);
        InstantiateItem();
    }

    private void OpenUI()
    {
        itemGetUI.SetActive(true);
        isOpened = true;
    }

    public void ParticleOn()
    {
        particle.SetActive(true);
    }

    private void InstantiateItem()
    {
        int ran = Random.Range(0, 10);

        switch(ran)
        {
            case 0:
            case 1:
                text.text = "스테미나 상승";
                statusController.SetSP(20);
                image.sprite = sprite[0];
                break;
            case 2:
            case 3:
                text.text = "HP 상승";
                statusController.SetHP(15);
                image.sprite = sprite[1];
                break;
            case 4:
            case 5:
            case 6:
                text.text = "얼음의 원소";
                image.sprite = sprite[2];
                inventory.AcquireItem(items[0], Random.Range(2, 5)); // 얼음
                break;
            case 7:
            case 8:
            case 9:
                text.text = "불의 원소";
                image.sprite = sprite[3];
                inventory.AcquireItem(items[1], Random.Range(2, 5)); // 불
                break;
        }
    }
}
