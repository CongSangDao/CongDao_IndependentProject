using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefBehavior : MonoBehaviour
{
    private Animator chefAnimator;
    private bool playerIsNearChef = false; // This variable checks if the player is near the chef

    private void Start()
    {
        chefAnimator = GetComponent<Animator>();
        // You can still keep the automatic animation after 5 seconds if you want
        Invoke("TriggerPickUpAnimation", 5f);
    }

    private void Update()
    {
        if (playerIsNearChef && Input.GetKeyDown(KeyCode.E))
        {
            TriggerPickUpAnimation();
            TurnChefRight();
        }
    }

    public void TriggerPickUpAnimation()
    {
        chefAnimator.SetTrigger("pickUp");
    }

    private void TurnChefRight()
    {
        // Rotate the chef to the right by 90 degrees
        transform.Rotate(0, 90, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the player enters the trigger zone, set playerIsNearChef to true
        if (other.CompareTag("Player"))
        {
            playerIsNearChef = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If the player exits the trigger zone, set playerIsNearChef to false
        if (other.CompareTag("Player"))
        {
            playerIsNearChef = false;
        }
    }
}
