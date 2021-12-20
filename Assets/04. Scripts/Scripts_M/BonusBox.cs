using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBox : MonoBehaviour
{
    [SerializeField]
    private GameObject particle;

    // Start is called before the first frame update
    void Start()
    {
        particle.SetActive(false);
    }

    
    public void OpenBox()
    {
        this.GetComponentInChildren<Animator>().SetTrigger("Open");
        Invoke("ParticleOn", 1f);
        InstantiateItem();
    }

    public void ParticleOn()
    {
        particle.SetActive(true);
    }

    private void InstantiateItem()
    {

    }
}
