using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab; // Assign the customer prefab in the inspector
    public float spawnInterval = 10f; // Time between spawns in seconds
    public Transform spawnPoint; // The position where customers will spawn
    public Transform entranceTarget;

    void Start()
    {
        InvokeRepeating("SpawnCustomer", 0f, spawnInterval);
    }

    void SpawnCustomer()
    {
        if (customerPrefab != null && spawnPoint != null)
        {
            GameObject spawnedCustomer = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);

            CustomerMovement customerMovement = spawnedCustomer.GetComponent<CustomerMovement>();

            if (customerMovement != null && entranceTarget != null)
            {
                customerMovement.entranceTarget = entranceTarget;
            }
        }
    }
}
