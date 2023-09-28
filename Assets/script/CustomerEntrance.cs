using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerEntrance : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            WelcomeCustomer();
        }
    }

    void WelcomeCustomer()
    {
        Debug.Log("Welcome to Pink Piggy Restaurant!");
    }
}
