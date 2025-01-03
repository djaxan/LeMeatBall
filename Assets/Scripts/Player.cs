using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed of the player
    public float laneWidth = 100f;  // Distance between lanes (left/right)
    private Vector3 targetPosition;  // Target position to move to

    private void Start()
    {
        // Start by positioning the player in the middle lane
        targetPosition = transform.position;
    }

    private void Update()
    {
        // Handle player input for left and right movement
        if (Input.GetKeyDown(KeyCode.A)) // Move left (A key)
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D)) // Move right (D key)
        {
            MoveRight();
        }

        // Smoothly move the player to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }

    private void MoveLeft()
    {
        // Move the player left by changing the X position
        targetPosition = new Vector3(transform.position.x - laneWidth, transform.position.y, transform.position.z);
    }

    private void MoveRight()
    {
        // Move the player right by changing the X position
        targetPosition = new Vector3(transform.position.x + laneWidth, transform.position.y, transform.position.z);
    }
}