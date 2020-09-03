using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform[] terrains;
    [SerializeField] private Transform player;
    [SerializeField] private Transform parentTerrain;
    [SerializeField] private float safeZone;
    [SerializeField] private float amountOfTerrainOnScreen;
    [SerializeField] private float terrainLength;
    [SerializeField] private bool fetchTerrainSequentially = false;

    private Vector3 terrainOffset;
    private Transform lastSpawnTerrain;
    private int lastSpawnedIndex = 0;
    private float spawnZTerrain = 0;

    private int counter = -1;

    List<Transform> terrainsT = new List<Transform>();

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

        if(!fetchTerrainSequentially)
            lastSpawnTerrain = Instantiate(terrains[RandomTerrainFetcher()]) as Transform;
        else
            lastSpawnTerrain = Instantiate(terrains[SequentialTerrainFetcher()]) as Transform;

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

    private int SequentialTerrainFetcher()
    {
        if (terrains.Length <= 1)
            return 0;

        ++counter;

        if (counter < 0)
            counter = -1;
        else if (counter >= terrains.Length)
            counter = 0;

        return counter;

    }

    private void DestroySpawnedTerrain()
    {
        Destroy(terrainsT[0].gameObject);
        terrainsT.RemoveAt(0);
    }

}
