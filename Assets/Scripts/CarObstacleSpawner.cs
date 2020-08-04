using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarObstacleSpawner : MonoBehaviour
{

    [SerializeField] private Transform[] pointsForSpawning;
    [SerializeField] private float rateOfSpawning;

    [SerializeField] private GameObject[] carObjectList;

    private float lastSpawnTime = 0f;

    private int lastSpawningPoint = 0;
    private int lastCarObjectSpawnIndex = 0;
    private List<GameObject> spawnedCarList = new List<GameObject>();

    void Update()
    {
        if (FindObjectOfType<PlayerController>().enabled)
        {
            if (Time.time > rateOfSpawning + lastSpawnTime)
            {
                SpawnCar();
            }

            if (Time.time > rateOfSpawning + (lastSpawnTime * 2))
                DestroyCar();

        }
    }

    private void SpawnCar()
    {
        int pointIndex = GenerateIndexForSpawnPoint();
        int carIndex = GenerateIndexForCarObject();
        Debug.Log("carIndex ; " + carIndex);
        GameObject @car = Instantiate(carObjectList[carIndex], pointsForSpawning[pointIndex].transform.position, Quaternion.identity);

        car.transform.eulerAngles = new Vector3(car.transform.rotation.x, 180f, car.transform.rotation.z);
        spawnedCarList.Add(car);
        lastSpawnTime = Time.time;
    }

    private int GenerateIndexForSpawnPoint()
    {
        if (pointsForSpawning.Length <= 1)
            return 0;

        int x = lastSpawningPoint;
        while (x == lastSpawningPoint)
        {
            x = Random.Range(0, pointsForSpawning.Length);
        }

        lastSpawningPoint = x;
        return lastSpawningPoint;
    }

    private int GenerateIndexForCarObject()
    {
        if (carObjectList.Length <= 1)
            return 0;

        int x = lastCarObjectSpawnIndex;
        while (x == lastCarObjectSpawnIndex)
        {
            x = Random.Range(0, carObjectList.Length);
        }
        lastCarObjectSpawnIndex = x;
        return lastCarObjectSpawnIndex;
    }

    private void DestroyCar()
    {
        Destroy(spawnedCarList[0], 5f);
        spawnedCarList.RemoveAt(0);
        lastSpawnTime = Time.time;
    }

}
