using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance; // Singleton for easy access.
    public TriggerParticle chefScript; // Reference to the chef's script.

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        // Optionally, you can make this persist across scenes:
        DontDestroyOnLoad(gameObject);
    }

    // Method to call from CustomerMovement when an order is made.
    public void PlaceOrder(int orderIndex)
    {
        chefScript.ReceiveOrder(orderIndex); // Tell the chef which order to prepare.
    }
}
