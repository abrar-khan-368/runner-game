using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCar : MonoBehaviour
{

    [SerializeField] private CarObstacleSpawner car;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            car.car.GetComponent<CarMovement>().enabled = true;
        }
    }
}
