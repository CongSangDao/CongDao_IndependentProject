using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public static TableManager instance;
    public List<Transform> tables = new List<Transform>(); // Assign all tables in the inspector or find them dynamically at runtime
    public List<bool> occupied = new List<bool>(); // A list to track if tables are occupied

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static Transform FindAvailableTable(int customerID)
    {
        if (customerID >= 0 && customerID < instance.tables.Count)
        {
            if (!instance.occupied[customerID])
            {
                instance.occupied[customerID] = true; // Mark the table as occupied
                return instance.tables[customerID - 1];
            }
        }
        return null; // No available tables
    }
}
