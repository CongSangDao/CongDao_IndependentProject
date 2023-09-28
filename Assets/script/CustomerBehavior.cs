using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerBehavior : MonoBehaviour
{
    // This function is called when the customer enters a trigger zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Entrance"))
        {
            PlaceOrder();
        }
    }

    void PlaceOrder()
    {
        // Logic for the customer placing an order
        Debug.Log("Customer has placed an order!");
    }
}
