using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCharracter : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float rotationSpeed = 720.0f;
    private CharacterController characterController;

    private bool canMove = true; // New variable

    private void Start()
    {
        
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        if (!canMove) return;  // Stop moving if canMove is false

        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalMovement, 0, verticalMovement).normalized;

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement);
            toRotation *= Quaternion.Euler(0, 90, 0);  // Apply a 90-degree rotation on the Y-axis
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

   
}
