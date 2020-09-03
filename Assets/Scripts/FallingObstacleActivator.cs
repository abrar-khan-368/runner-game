using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObstacleActivator : MonoBehaviour
{
    [SerializeField] private Transform[] fallingObstacles;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < fallingObstacles.Length; i++)
            {
                fallingObstacles[i].gameObject.SetActive(true);
            }
        }
    }
}
