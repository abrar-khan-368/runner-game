using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Player movement essential")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float turnSpeed = 20f;
    [Range(1, 3)]
    [SerializeField] private float moveSpeedMultiplier = 1.2f;
    [SerializeField] private float maxMoveSpeed = 25f;

    [SerializeField] private float mileStoneCount = 0;
    [SerializeField] private float incrementMileStoneBy = 100f;

    [SerializeField] private float jumpSpeed;
    [SerializeField] private float doubleJumpSpeed;
    [SerializeField] private float shiftingOfLane = 3.0f;

    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Player Animator")]
    [SerializeField] private Animator animator;

    private int lane = 1;
    private float gravity = 20f;
    private float verticalVelocity = 0f;
    private bool grounded = true;

    Vector3 moveVector = Vector3.zero;

    private const int LEFT = 0;
    private const int RIGHT = 2;

    [HideInInspector] public int coinCollected = 0;
    [HideInInspector] public bool isDead = false;

    void Start()
    {
        controller = this.GetComponent<CharacterController>();
    }
    
    void Update()
    {
        if (moveSpeed <= maxMoveSpeed)
        {
            if (transform.position.z >= incrementMileStoneBy)
            {
                mileStoneCount += incrementMileStoneBy;
                incrementMileStoneBy += incrementMileStoneBy;
                moveSpeed += moveSpeedMultiplier;
            }
        }

        scoreText.text = "" + (int)(transform.position.z / 2);

        Movement();
        Jump();
    }

    private void Movement()
    {

        if (transform.position.magnitude >= 0.1f)
            animator.SetBool("running", true);

        if (Input.GetKeyDown(KeyCode.A) || SwipeInputs.instance.SwipeLeft)
        {
            SwipeInputs.instance.SwipeLeft = false;
            ChangeLane(false);
        }

        if (Input.GetKeyDown(KeyCode.D) || SwipeInputs.instance.SwipeRight)
        {
            SwipeInputs.instance.SwipeRight = false;
            ChangeLane(true);
        }

        if (grounded && (Input.GetKeyDown(KeyCode.S) || SwipeInputs.instance.SwipeDown))
        {
            SwipeInputs.instance.SwipeDown = false;
            SlidingStarted();
            Invoke("SlidingStopped", 1f);
        }

        Vector3 targetPosition = transform.position.z * Vector3.forward;

        switch (lane)
        {
            case LEFT:
                targetPosition += Vector3.left * shiftingOfLane;
                break;
            case RIGHT:
                targetPosition += Vector3.right * shiftingOfLane;
                break;
        }

       
        
        moveVector.x = (targetPosition - transform.position).x * turnSpeed;
        moveVector.y = verticalVelocity;
        moveVector.z = moveSpeed;

        

        controller.Move(moveVector * Time.deltaTime);

        Vector3 turnDirection = controller.velocity;
        turnDirection.y = 0f;
        transform.forward = Vector3.Lerp(transform.forward, turnDirection, 0.05f);

    }

    private void ChangeLane(bool rightHandSide)
    {
        if (!rightHandSide)
        {
            lane--;
            if (lane == -1)
                lane = 0;
        }
        else
        {
            lane++;
            if (lane == 3)
                lane = 2;
        }
    }

    private void Jump()
    {
        if(controller.isGrounded && (Input.GetKeyDown(KeyCode.Space) || SwipeInputs.instance.SwipeUp))
        {
            if (Input.GetKeyDown(KeyCode.Space) || SwipeInputs.instance.SwipeUp)
            {
                SwipeInputs.instance.SwipeUp = false;
                verticalVelocity = jumpSpeed;
            }
        }
        else
        {
            verticalVelocity -= (gravity * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.S) || SwipeInputs.instance.SwipeDown)
            {
                verticalVelocity -= jumpSpeed;
            }

        }

        if (!controller.isGrounded)
        {
            animator.SetBool("hasJump", true);
        }
        else
        {
            animator.SetBool("hasJump", false);
        }
        //jumpDirection.y -= gravity * Time.deltaTime;
        //controller.Move(jumpDirection * Time.deltaTime);
    }

    private void SlidingStarted()
    {
        animator.SetBool("isSliding", true);
        controller.height = controller.height / 2;
        verticalVelocity = 0.3f;
    }

    private void SlidingStopped()
    {
        animator.SetBool("isSliding", false);
        controller.height = controller.height * 2;
        verticalVelocity = 0f;
    }

    private void OnCollisionStay(Collision collision)
    {
        grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Obstacle");
            animator.SetTrigger("dead");
            isDead = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Obstacle"))
        {
            animator.SetTrigger("dead");
            moveSpeed = 0f;
            isDead = true;
        }

        if (other.gameObject.CompareTag("Coin"))
        {
            if (!isDead)
            {
                coinCollected += 1;
                Destroy(other.gameObject);
                FindObjectOfType<SoundManager>().PlayCoinCollectSound();
            }
        }

    }
}
