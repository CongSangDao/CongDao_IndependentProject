using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class DebugUIManager : MonoBehaviour
{

    // Use a list to keep track of messages
    public TextMeshProUGUI customerOrderText;
    public TextMeshProUGUI customerStatusText;

    // Call this method to update the customer order text
    public void UpdateCustomerOrderUI(string orderInfo)
    {
        customerOrderText.text = orderInfo;
    }

    // Call this method to update the customer status text
    public void UpdateCustomerStatusUI(string statusInfo)
    {
        customerStatusText.text = statusInfo;
    }

}
