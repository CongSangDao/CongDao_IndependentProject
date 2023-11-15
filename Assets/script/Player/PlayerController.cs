using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : MonoBehaviour
{
    public GameObject waitingCustomer = null;
    public GameObject carriedTray = null; // To keep track of the tray the player is carrying
    public float pickupRange = 2f;// The range within which the player can pick up a tray
    public float tableDropRange = 1f;
    public GameObject currentTable = null;

    void Update()
    {
        // Interaction with a customer
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (waitingCustomer != null)
            {
                AssignTableToCustomer(waitingCustomer);
            }
            else if (carriedTray == null) // Check if the player is not already carrying a tray
            {
                PickupFoodTray();
            }
            else if (carriedTray != null && IsPlayerAtTable())
            {
                DropFoodTrayOntoTable();
            }
        }
    }
    void PickupFoodTray()
    {
        // Detect all "FoodTray" tagged objects within the pickup range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickupRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("FoodTray"))
            {
                carriedTray = hitCollider.gameObject; // Assign the detected tray to the carriedTray variable
                Debug.Log("Picked up a food tray.");
                // Here typically disable the tray's collider and make it a child of the player
                // disable its physics and move it to a "holding" position relative to the player
                hitCollider.enabled = false; // Disabling the collider so it doesn't get picked up multiple times
                carriedTray.transform.SetParent(transform); // Make the tray follow the player
                carriedTray.transform.localPosition = new Vector3(0, 1, 1); // Adjust the position as needed
                break; // Exit the loop once a tray has been picked up
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Customer"))
        {
            waitingCustomer = other.gameObject;
        }
        else if (other.CompareTag("FoodTray"))
        {
            carriedTray = other.gameObject;
        }
        else if (other.CompareTag("Table"))
        {
            currentTable = other.gameObject; // Assign the table to currentTable
  
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            waitingCustomer = null;
        }
        else if (other.CompareTag("FoodTray"))
        {
            carriedTray = null;
        }
        else if (other.CompareTag("Table") && currentTable == other.gameObject)
        {
            currentTable = null;
        }
    }

    void AssignTableToCustomer(GameObject customer)
    {
        CustomerMovement customerScript = customer.GetComponent<CustomerMovement>();
        if (customerScript.sitPosition != null)
        {
            customerScript.SetTablePosition(customerScript.sitPosition);
            Table tableScript = customerScript.sitPosition.GetComponent<Table>();
            if (tableScript != null)
            {
                tableScript.assignedCustomer = customerScript;
            }
            else
            {
                Debug.Log("No Table script found on the sit position.");
            }
        }
        else
        {
            Debug.Log("No sit position assigned to the customer.");
        }
    }

    private void PickUpFoodTray(GameObject foodTray)
    {
        // Add logic to handle the pickup, e.g., making the tray a child of the player object
        // so it follows the player around, or just disable it to simulate the pickup.
        foodTray.SetActive(false);
        // Store the picked-up tray in a variable if you need to place it down later or use it.
        // e.g., this.currentlyHeldTray = foodTray;
    }

    void DropFoodTrayOntoTable()
    {
        // Your existing logic to check if the player can drop the food tray
        // Here's where you actually place it on the table
        Vector3 tableSurfacePosition = GetTableSurfacePosition();
        carriedTray.transform.SetParent(null); // Unparent the tray from the player
        carriedTray.transform.position = tableSurfacePosition; // Position it on the table
        carriedTray.SetActive(true); // Make sure the tray is visible if you've disabled it before
        Debug.Log("Food placed on the table.");

        // Serve the order to the customer
        if (currentTable != null)
        {
            // Assuming each table knows which customer it's associated with
            Table tableScript = currentTable.GetComponent<Table>();
            if (tableScript != null && tableScript.assignedCustomer != null)
            {
                // Call ServeOrder on the CustomerMovement script attached to the assigned customer
                tableScript.assignedCustomer.ServeOrder(carriedTray);
            }
            else
            {
                Debug.Log("No assigned customer at table or no Table script attached to the table GameObject.");
            }
        }

        carriedTray = null; // Clear the reference to the tray since you've dropped it
    }
    bool IsPlayerAtTable()
    {
        // Here you check if the player is close enough to a table to drop the food tray
        // The currentTable object should be assigned when the player is near a table
        return currentTable != null && Vector3.Distance(transform.position, currentTable.transform.position) <= tableDropRange;
    }
    Vector3 GetTableSurfacePosition()
    {
        // Here you calculate the position on the table where the food tray should be placed
        if (currentTable != null)
        {
            Collider tableCollider = currentTable.GetComponent<Collider>();
            Vector3 tableTopSurface = currentTable.transform.position + new Vector3(0, tableCollider.bounds.extents.y, 0);
            return tableTopSurface;
        }
        return Vector3.zero;
    }
}
