using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnCtrl : MonoBehaviour
{
    public GameObject boss;

    public float spawnTime_B;

    private float curTime;

    void Update()
    {
        curTime += Time.deltaTime;

        if (curTime > spawnTime_B)
        {
            float newX = Random.Range(0f, 500f);
            float newY = Random.Range(-50f, 50f);
            float newZ = Random.Range(0f, 500f);

            boss = Instantiate(boss);

            boss.transform.position = new Vector3(newX, newY, newZ);

            curTime = 0;
        }
    }
}
