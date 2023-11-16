using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public GameObject[] customers; // Store all customers in an array
    public static List<GameObject> orderedFoods = new List<GameObject>();
    public int currentLevel = 1; // Start at level 1

    void Start()
    {
        CustomerManager.orderedFoods.Clear();
        // Deactivate all customers at the start
        foreach (GameObject customer in customers)
        {
            customer.SetActive(false);
        }

        // Invoke customer spawning at different levels with delays
        Invoke("ActivateCustomersLevel1", 10f); // Level 1: Invoke after 10 seconds
        Invoke("ActivateCustomersLevel2", 70f); // Level 2: Invoke after 70 seconds (60 seconds after Level 1)
        Invoke("ActivateCustomersLevel3", 160f); // Level 3: Invoke after 160 seconds (90 seconds after Level 2)
    }

    // Level 1 activation
    void ActivateCustomersLevel1()
    {
        currentLevel = 1;
        ActivateRandomCustomer();
    }

    // Level 2 activation
    void ActivateCustomersLevel2()
    {
        currentLevel = 2;
        ActivateRandomCustomer(); // This will activate 2 customers because currentLevel is now 2
    }

    // Level 3 activation
    void ActivateCustomersLevel3()
    {
        currentLevel = 3;
        ActivateRandomCustomer(); // This will activate 3 customers because currentLevel is now 3
    }

    void ActivateRandomCustomer()
    {
        for (int i = 0; i < currentLevel; i++) // Spawn customers according to the current level
        {
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
        }
    }

    // Checks if there is at least one inactive customer to activate
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
