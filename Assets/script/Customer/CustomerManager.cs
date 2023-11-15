using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public GameObject[] customers; // Store all customers in an array
    public static List<GameObject> orderedFoods = new List<GameObject>();

    void Start()
    {
        // Deactivate all customers at the start
        foreach (GameObject customer in customers)
        {
            customer.SetActive(false);
        }

        // Start the customer sequence with a random customer after 5 seconds
        // and then every 20 seconds activate another random customer.
        InvokeRepeating("ActivateRandomCustomer", 5f, 20f);
    }

    void ActivateRandomCustomer()
    {
        // Check if there are any inactive customers left to activate
        if (InactiveCustomerExists())
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, customers.Length);
            }
            while (customers[randomIndex].activeSelf); 

            customers[randomIndex].SetActive(true);
        }
        else
        {
            CancelInvoke("ActivateRandomCustomer"); // Stop the repeating invoke when all customers are activated
        }
    }

    // Utility method to check if there are any inactive customers
    bool InactiveCustomerExists()
    {
        foreach (GameObject customer in customers)
        {
            if (!customer.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
}
