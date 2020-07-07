using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeInputs : MonoBehaviour
{

    public static SwipeInputs instance = null;

    [SerializeField] private float maxSwipeTime;
    [SerializeField] private float minSwipeDistance;

    public bool SwipeUp;
    public bool SwipeDown;
    public bool SwipeLeft;
    public bool SwipeRight;

    private float startSwipeTime;
    private float endSwipeTime;
    private float swipeTime;

    private Vector2 startSwipePosition;
    private Vector2 endSwipePosition;
    private float swipeLength;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            startSwipeTime = Time.time;
            startSwipePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endSwipeTime = Time.time;
            endSwipePosition = Input.mousePosition;
            swipeTime = endSwipeTime - startSwipeTime;
            swipeLength = (endSwipePosition - startSwipePosition).magnitude;

            if (swipeTime < maxSwipeTime && swipeLength > minSwipeDistance)
            {
                SwipeDirection();
            }
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startSwipeTime = Time.time;
                startSwipePosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endSwipeTime = Time.time;
                endSwipePosition = touch.position;
                swipeTime = endSwipeTime - startSwipeTime;
                swipeLength = (endSwipePosition - startSwipePosition).magnitude;

                if (swipeTime < maxSwipeTime && swipeLength > minSwipeDistance)
                {
                    SwipeDirection();
                }

            }

        }
    }

    private void SwipeDirection()
    {
        Vector2 distance = endSwipePosition - startSwipePosition;
        float x = Mathf.Abs(distance.x);
        float y = Mathf.Abs(distance.y);

        if (x > y)
        {
            if (distance.x < 0)
                SwipeLeft = true;
            else if(distance.x > 0)
                SwipeRight = true;
        }
        else
        {
            if (distance.y > 0)
                SwipeUp = true;
            else
                SwipeDown = true;
        }
        startSwipePosition = endSwipePosition = Vector2.zero;
    }

}
