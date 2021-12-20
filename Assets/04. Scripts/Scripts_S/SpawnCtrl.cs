using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCtrl : MonoBehaviour
{
    public GameObject monster;
    public float spawnTime;

    private float curTime;

    void Update()
    {

        curTime += Time.deltaTime;

        if (curTime > spawnTime)
        {
            float newX = Random.Range(0f, 500f);
            float newY = Random.Range(-50f, 50f);
            float newZ = Random.Range(0f, 500f);

            GameObject instance = Instantiate(monster);

            instance.transform.position = new Vector3(newX, newY, newZ);

            //Destroy(instance, 2.0f);

            curTime = 0;
        }
    }
}
