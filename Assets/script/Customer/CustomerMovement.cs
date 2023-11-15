using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class CustomerMovement : MonoBehaviour
{
    // Customer related fields
    public float waitTime = 0;
    public float maxWaitTime = 10;
    private bool alreadyUnsatisfied = false;
    

    // Movement related fields
    private NavMeshAgent agent;
    public Transform entranceTarget;
    public Transform sitPosition;
    private bool hasArrivedAtEntrance = false;
    private bool isWaiting = false;
    private bool isServed = false;

    // Highlighting the customer
    public Material normalMaterial;
    public Material highlightMaterial;
    private Renderer customerRenderer;

    //Food Order
    public GameObject[] foodPrefabs; 
    public GameObject orderedFood;
    private GameObject customerOrder;
    private bool isWaitingForOrder = false;
    private List<GameObject> availableFoodPrefabs = new List<GameObject>();

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MoveToEntrance();
        customerRenderer = GetComponent<Renderer>();
        if (customerRenderer != null)
        {
            customerRenderer.material = highlightMaterial;
        }
        foreach (var foodPrefab in foodPrefabs)
        {
            if (foodPrefab != null)
            {
                availableFoodPrefabs.Add(foodPrefab); 
            }
            else
            {
                Debug.LogError("A food prefab is null in the array.");
            }
        }
    }

    private void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= 1f )
        {
            hasArrivedAtEntrance = true;
            
        }
        if ( hasArrivedAtEntrance && isWaiting)
        {
            waitTime += Time.deltaTime;
            if (waitTime > maxWaitTime && !alreadyUnsatisfied)
            {
                GameManager.instance.CustomerBecameUnsatisfied(this);
                alreadyUnsatisfied = true;
                Debug.Log("Customer is about to become unsatisfied!");
                
            }
        }



    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Table"))
        {
            isWaiting = false;
            waitTime = 0;
            MakeRandomOrder(); 
            alreadyUnsatisfied = false;
            isServed = false;
            Debug.Log(gameObject.name + " arrived at the table.");
        }

        if (other.CompareTag("Player") && isWaiting)
        {
            HighlightCustomer();
        }

        if (other.CompareTag("Entrance"))
        {
            isWaiting = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RemoveHighlight();
        }

    }

    public void HighlightCustomer()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            if (r.gameObject.tag != "Eyes" && highlightMaterial)
            {
                r.material = highlightMaterial;
            }
        }
    }

    public void RemoveHighlight()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            if (r.gameObject.tag != "Eyes" && normalMaterial)
            {
                r.material = normalMaterial;
            }
        }
    }

    void MoveToEntrance()
    {
        if (entranceTarget != null)
        {
            agent.SetDestination(entranceTarget.position);
        }
        else
        {
            Debug.LogError("Entrance target not set for customer!");
        }
    }

    public void SetTablePosition(Transform tablePosition)
    {
        if (tablePosition != null)
        {
            sitPosition = tablePosition;
            agent.SetDestination(sitPosition.position);
        }
        else
        {
            Debug.Log("Table position is null.");
        }
    }

    void MakeRandomOrder()
    {
        var foodsNotOrdered = availableFoodPrefabs.Where(food => !CustomerManager.orderedFoods.Contains(food)).ToList();
        if (foodsNotOrdered.Count > 0)
        {
            int randomIndex = Random.Range(0, foodsNotOrdered.Count);
            orderedFood = foodsNotOrdered[randomIndex]; // Store the prefab that represents their order
            CustomerManager.orderedFoods.Add(orderedFood); // Add this food to the list of ordered foods
            customerOrder = orderedFood; // Ensure this is set for checking when the order is delivered
            Debug.Log(gameObject.name + " Ordered: " + orderedFood.name);
            isWaitingForOrder = true; // Customer has made an order and is waiting
            isServed = false;
        }
        else
        {
            // If there are no available food prefabs, log a warning.
            Debug.LogWarning(gameObject.name + ": No available food prefabs to order.");
        }
    }

    public bool CheckOrder(GameObject deliveredFood)
    {
        if (deliveredFood == orderedFood) // changed from customerOrder to orderedFood
        {
            Debug.Log("Delivered correct food!");
            return true;
        }
        else
        {
            Debug.Log("Delivered wrong food!");
            return false;
        }
    }
    public void ServeOrder(GameObject food)
    {
        Debug.Log($"ServeOrder called with food: {food.name}, isServed: {isServed}");
        if (isWaitingForOrder && !isServed)
        {
            if (food.name == orderedFood.name)
            {
                // Correct food
                Destroy(customerOrder); // Destroy the customer's current order
                customerOrder = Instantiate(food, transform.position, Quaternion.identity); // Serve the actual food
                customerOrder.SetActive(true); // Make the food visible
                isWaitingForOrder = false;
                isServed = true;
                GameManager.instance.CustomerSatisfied(this); // Now the customer is satisfied
                Debug.Log("Customer served with correct order: " + customerOrder.name);
            }
            else
            {
                // Wrong food, the customer becomes unsatisfied
                GameManager.instance.CustomerBecameUnsatisfied(this);
                Debug.Log("Customer served with wrong order: " + food.name);
            }
        }
        else
        {
            Debug.Log("Customer is not waiting for an order or already served.");
        }
    }

    public void ResetAvailableFood()
    {
        availableFoodPrefabs.Clear();
        foreach (var foodPrefab in foodPrefabs)
        {
            availableFoodPrefabs.Add(foodPrefab);
        }
    }
}
