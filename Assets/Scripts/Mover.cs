using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private float offsetZ;

    void Update()
    {
        float desiredPositionZ = player.position.z + offsetZ;
        transform.position = new Vector3(transform.position.x, transform.position.y, desiredPositionZ);
    }
}
