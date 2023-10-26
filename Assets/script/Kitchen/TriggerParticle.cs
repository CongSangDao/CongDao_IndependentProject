using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerParticle : MonoBehaviour
{
    public Animator chefAnimator;
    public ParticleSystem tableParticle;

    private bool playerIsNearTable = false; // This variable checks if the player is near the table

    private void Update()
    {
        if (playerIsNearTable && Input.GetKeyDown(KeyCode.E))
        {
            TriggerPickUpAnimation();
            tableParticle.Play();
            
        }
    }

    public void TriggerPickUpAnimation()
    {
        chefAnimator.SetTrigger("pickUp");
        TurnChefRight();
    }

    private void TurnChefRight()
    {
        // Rotate the chef 90 degrees to the right
        chefAnimator.transform.Rotate(0, 90, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the player enters the trigger zone, set playerIsNearTable to true
        if (other.CompareTag("Player"))
        {
            playerIsNearTable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If the player exits the trigger zone, set playerIsNearTable to false
        if (other.CompareTag("Player"))
        {
            playerIsNearTable = false;
        }
    }
}
