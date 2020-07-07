using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] terrains;
    [SerializeField] private Transform player;
    [SerializeField] private Transform parentTerrain;
    [SerializeField] private float safeZone;
    [SerializeField] private float amountOfTerrainOnScreen;
    [SerializeField] private float terrainLength;

    private Vector3 terrainOffset;
    private GameObject lastSpawnTerrain;
    private int lastSpawnedIndex = 0;
    private float spawnZTerrain = 0;

    List<GameObject> terrainsT = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GenerateTerrain();
        }
    }

    private void Update()
    {
        if (player.position.z - safeZone > (spawnZTerrain - amountOfTerrainOnScreen * terrainLength))
        {
            GenerateTerrain();
            DestroySpawnedTerrain();
        }
    }

    private void GenerateTerrain()
    {
        lastSpawnTerrain = Instantiate(terrains[RandomTerrainFetcher()]) as GameObject;
        lastSpawnTerrain.transform.SetParent(parentTerrain);
        spawnZTerrain += terrainOffset.z + terrainLength;
        lastSpawnTerrain.transform.SetPositionAndRotation(new Vector3(
            lastSpawnTerrain.transform.position.x, lastSpawnTerrain.transform.position.y, spawnZTerrain),
            Quaternion.identity);
        terrainsT.Add(lastSpawnTerrain);
    }

    private int RandomTerrainFetcher()
    {
        if (terrains.Length <= 1)
            return 0;

        int x = lastSpawnedIndex;
        while (x == lastSpawnedIndex)
        {
            x = Random.Range(0, terrains.Length);
        }
        lastSpawnedIndex = x;
        return lastSpawnedIndex;

    }

    private void DestroySpawnedTerrain()
    {
        Destroy(terrainsT[0]);
        terrainsT.RemoveAt(0);
    }

}
