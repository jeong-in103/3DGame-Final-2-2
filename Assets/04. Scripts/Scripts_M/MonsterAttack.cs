using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField]
    private StatusController statusController;

    [SerializeField]
    int damage;

    private void Start()
    {
        statusController = GameObject.Find("FPSController").GetComponent<StatusController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("�ĸ���");
            statusController.DecreaseHP(damage);
        }
    }
}
