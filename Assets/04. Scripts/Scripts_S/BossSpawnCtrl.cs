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
            TerrainData theIsland;
            theIsland = GameObject.Find("Terrain").GetComponent<Terrain>().terrainData;

            Vector3 worldTreePos = Vector3.Scale(theIsland.treeInstances[Random.Range(0, theIsland.treeInstances.Length)].position, theIsland.size) + Terrain.activeTerrain.transform.position;
            Instantiate(boss, worldTreePos, Quaternion.identity); // Create a prefab tree on its pos


            curTime = 0;
        }
    }
}
