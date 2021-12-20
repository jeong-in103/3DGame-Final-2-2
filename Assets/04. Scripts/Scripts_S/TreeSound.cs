using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSound : MonoBehaviour
{
    [SerializeField]
    private string attackSound_T;
    public void Attack()
    {
        SoundManager.instance.PlaySE(attackSound_T);

        
    }
}
