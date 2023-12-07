using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerParticle : MonoBehaviour
{
    public Animator chefAnimator;
    public ParticleSystem tableParticle;
    public GameObject[] foodPrefabs; // Assign your food prefabs in the inspector
    // This should be an array if you have multiple spawn locations, one for each table.
    public Transform[] spawnLocations; // Make sure this is an array if you have multiple tables.

    private bool playerIsNearTable = false; // This variable checks if the player is near the table
    private bool isAwaitingOrder = false;
    private int currentOrderIndex = -1;
    private void Update()
    {
        if (playerIsNearTable && Input.GetKeyDown(KeyCode.E))
        {
            // Player has pressed 'E' to interact with the chef
            isAwaitingOrder = true;
            Debug.Log("Press a number key to select an order.");
        }

        if (isAwaitingOrder)
        {
            CheckForOrderInput();
        }
    }
    private void CheckForOrderInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCookingOrder(0); // For food prefab at index 0
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCookingOrder(1); // For food prefab at index 1
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCookingOrder(2); // For food prefab at index 2
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartCookingOrder(3); // For food prefab at index 3
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            StartCookingOrder(4); // For food prefab at index 4
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            StartCookingOrder(5); // For food prefab at index 5
        }
        
    }
    private void StartCookingOrder(int orderIndex)
    {
        if (orderIndex < 0 || orderIndex >= foodPrefabs.Length)
        {
            Debug.LogError("Order index is out of bounds. Index: " + orderIndex);
            return; // Exit the function to prevent the out of range error
        }

        Debug.Log("Cooking order: " + (orderIndex + 1));

        if (chefAnimator != null)
        {
            chefAnimator.SetTrigger("pickUp"); // Confirm this trigger exists in the Animator
        }

        if (tableParticle != null)
        {
            tableParticle.Play(); // Start the particle effect
        }

        StartCoroutine(CookingRoutine(orderIndex));
        isAwaitingOrder = false; // Reset the flag after starting the coroutine
    }

    public void ReceiveOrder(int orderIndex)
    {
        currentOrderIndex = orderIndex; // Set the current order index to the received order.
        Debug.Log("Order received: " + currentOrderIndex);
    }

    private IEnumerator CookingRoutine(int orderIndex)
    {
        chefAnimator.SetTrigger("pickUp");
        yield return new WaitForSeconds(30); // Wait for cooking time
        Instantiate(foodPrefabs[orderIndex], spawnLocations[0].position, Quaternion.identity);
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
