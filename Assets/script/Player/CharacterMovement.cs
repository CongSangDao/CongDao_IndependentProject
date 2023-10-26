using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the movement magnitude
        float moveAmount = new Vector3(horizontal, 0, vertical).magnitude;

        // Set the 'speed' parameter in the Animator
        animator.SetFloat("speed", moveAmount);

        
    }
}
