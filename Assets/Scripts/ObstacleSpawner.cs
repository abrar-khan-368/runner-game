using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    [SerializeField] private GameObject[] obstacleSpawnerPoint;
    [SerializeField] private GameObject[] obstaclePrefabs;


    private int lastPrefabSpawnedPos = 0;

    void Start()
    {
        GameObject @object = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], obstacleSpawnerPoint[0].transform.position, Quaternion.identity);
        @object.transform.localPosition = new Vector3(@object.transform.localPosition.x, @object.transform.localPosition.y + 0.35f, @object.transform.localPosition.z);
        @object.transform.localEulerAngles = new Vector3(@object.transform.localEulerAngles.x,
            180f, @object.transform.localEulerAngles.z);

        @object = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], obstacleSpawnerPoint[1].transform.position, Quaternion.identity);
        @object.transform.localPosition = new Vector3(@object.transform.localPosition.x, @object.transform.localPosition.y + 0.35f, @object.transform.localPosition.z);
        @object.transform.localEulerAngles = new Vector3(@object.transform.localEulerAngles.x,
            180f, @object.transform.localEulerAngles.z);

        @object = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], obstacleSpawnerPoint[2].transform.position, Quaternion.identity);
        @object.transform.localPosition = new Vector3(@object.transform.localPosition.x, @object.transform.localPosition.y + 0.35f, @object.transform.localPosition.z);
        @object.transform.localEulerAngles = new Vector3(@object.transform.localEulerAngles.x,
            180f, @object.transform.localEulerAngles.z);

    }

    private int IndexGenerator()
    {
        if (obstacleSpawnerPoint.Length <= 1)
            return 0;

        int x = lastPrefabSpawnedPos;
        while (x == lastPrefabSpawnedPos)
        {
            x = Random.Range(0, obstacleSpawnerPoint.Length);
        }
        lastPrefabSpawnedPos = x;
        return lastPrefabSpawnedPos;

    }

}
