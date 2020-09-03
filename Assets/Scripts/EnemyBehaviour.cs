using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField] private Transform castRayTransform;
    [SerializeField] private float playerDetectionRange = 18f;
    [SerializeField] private Animator animator;

    [Header("Select Enemy State")]
    [SerializeField] private EnemyState enemyState;

    [Header("Fields related to enemy state : throwing")]
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private Transform stones;
    [SerializeField] private Transform stoneObstacleSpawner;
    [SerializeField] private float throwForce = 100f;
    [SerializeField] private float throwRange = 7f;
    [SerializeField] private float throwSpeed = 10f;

    private float lastFiredAt = 0f;

    private enum EnemyState { StandBy, Melee, Attacker};

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        switch (enemyState) {
            case EnemyState.Melee:
                MeleeAttack();
                break;
            case EnemyState.StandBy:
                StandBy();
                break;
            case EnemyState.Attacker:
                ThrowingObstacleAttack();
                break;
            default:
                Debug.Log("Enemy State Selection is invalid");
                break;
        }
    }

    private void MeleeAttack()
    {
        RaycastHit raycast;
        if (Physics.Raycast(castRayTransform.position, castRayTransform.forward, out raycast, playerDetectionRange))
        {
            if (raycast.collider.tag == "Player")
            {
                animator.SetTrigger("shield");
            }
        }
    }

    private void StandBy()
    {
        RaycastHit raycast;
        if (Physics.Raycast(castRayTransform.position, castRayTransform.forward, out raycast, playerDetectionRange))
        {
            if (raycast.collider.tag == "Player")
            {
                animator.SetTrigger("equipThrowable");
            }
        }
    }

    private void ThrowingObstacleAttack()
    {
        RaycastHit raycast;
        if (Physics.Raycast(castRayTransform.position, castRayTransform.forward, out raycast, playerDetectionRange))
        {
            if (raycast.collider.tag == "Player")
            {
                animator.SetBool("equipThrowable", true);

                Debug.DrawLine(this.transform.position, Vector3.forward, Color.red);
                Debug.Log("Distance : " + Vector3.Distance(this.transform.position, player.position));
                if (Vector3.Distance(this.transform.position, player.position) < throwRange)
                {
                    Debug.DrawLine(castRayTransform.position, raycast.point, Color.red);
                    if (Time.time > fireRate + lastFiredAt)
                    {
                        animator.SetBool("throw", true);
                        Transform obstacle = Instantiate(stones, stoneObstacleSpawner.position, Quaternion.identity);
                        obstacle.gameObject.GetComponent<Rigidbody>().AddForce(-obstacle.transform.forward * throwForce * throwSpeed, ForceMode.Force);

                        lastFiredAt = Time.time;
                    }
                }
            }
        }
    }

}
