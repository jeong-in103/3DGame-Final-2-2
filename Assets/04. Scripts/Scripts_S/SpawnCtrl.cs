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
            TerrainData theIsland;
            theIsland = GameObject.Find("Terrain").GetComponent<Terrain>().terrainData;
            // For every tree on the island
            for (int i = 0; i < monster.Length; i++)
            {
                // Find its local position scaled by the terrain size (to find the real world position)
                Vector3 worldTreePos = Vector3.Scale(theIsland.treeInstances[i].position, theIsland.size) + Terrain.activeTerrain.transform.position;
                Instantiate(monster[i], worldTreePos, Quaternion.identity); // Create a prefab tree on its pos
            }

            curTime = 0;
        }
    }
}
