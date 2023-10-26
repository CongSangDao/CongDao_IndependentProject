using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public GameObject[] customers; // Store all customers in an array

    private int currentCustomerIndex = 0;

    void Start()
    {
        // Deactivate all customers at the start
        foreach (GameObject customer in customers)
        {
            customer.SetActive(false);
        }

        // Start the customer sequence with the first customer after 5 seconds
        // and then every 55 seconds activate the next customer.
        InvokeRepeating("ActivateNextCustomer", 5f, 55f);
    }

    void ActivateNextCustomer()
    {
        if (currentCustomerIndex < customers.Length)
        {
            customers[currentCustomerIndex].SetActive(true);
            currentCustomerIndex++;
        }
        else
        {
            CancelInvoke("ActivateNextCustomer"); // Stop the repeating invoke when all customers are activated
        }
    }
}
