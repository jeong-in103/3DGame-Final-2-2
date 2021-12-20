using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCtrl : MonoBehaviour
{
    public GameObject[] monster;

    public float spawnTime_M;

    private float curTime;

    void Update()
    {

        curTime += Time.deltaTime;

        if (curTime > spawnTime_M)
        {
            float newX = Random.Range(0f, 500f);
            float newY = Random.Range(-50f, 50f);
            float newZ = Random.Range(0f, 500f);

            for (int i = 0; i < monster.Length; i++)
            {
                monster[i] = Instantiate(monster[i]);

                monster[i].transform.position = new Vector3(newX, newY, newZ);
            }

            curTime = 0;
        }
    }
}
