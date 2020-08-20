using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarObstacleSpawner : MonoBehaviour
{

    [SerializeField] private Transform pointOfSpawn;    

    [SerializeField] private GameObject[] carObjectList;

    private int lastCarObjectSpawnIndex = 0;

    [HideInInspector] public GameObject car;

    private void Start()
    {
        SpawnCar();
    }

    private void SpawnCar()
    {
        int carIndex = GenerateIndexForCarObject();
        Debug.Log("carIndex ; " + carIndex);
        car = Instantiate(carObjectList[carIndex], pointOfSpawn.transform.position, Quaternion.identity);

        car.transform.eulerAngles = new Vector3(car.transform.rotation.x, 180f, car.transform.rotation.z);
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
}
