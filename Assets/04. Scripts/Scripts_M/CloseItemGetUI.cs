using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseItemGetUI : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            gameObject.SetActive(false);
        }
    }
}
