using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private MovingObstacleSpeedConfig speedConfig;

    private float speed;

    private void Start()
    {
        speed = speedConfig.speeds[Random.Range(0, speedConfig.speeds.Length)];
    }

    private void Update()
    {
        this.gameObject.GetComponent<Rigidbody>().position =
            this.gameObject.transform.position - transform.forward * -speed * Time.deltaTime;
    }
}
