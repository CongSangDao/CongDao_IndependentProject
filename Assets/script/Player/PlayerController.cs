using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject customerToLead;
    private Transform tableToAssign = null;

    void Update()
    {
        // Interaction with a customer
        if (Input.GetKeyDown(KeyCode.E) && customerToLead != null)
        {
            if (tableToAssign != null) // If near a table
            {
                AssignTableToCustomer();
            }
            

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            customerToLead = other.gameObject;
        }
        else if (other.CompareTag("Table"))
        {
            tableToAssign = other.transform.Find("SitPosition");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            customerToLead = null;
        }
        else if (other.CompareTag("Table"))
        {
            tableToAssign = null;
        }
    }

    void AssignTableToCustomer()
    {
        CustomerFollow customerScript = customerToLead.GetComponent<CustomerFollow>();
        customerScript.SetTablePosition(tableToAssign);
    }
}
