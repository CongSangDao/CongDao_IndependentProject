using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class CustomerMovement : MonoBehaviour
{
    // Fields from Customer
    public float waitTime = 0;
    public float maxWaitTime = 10;
    private bool alreadyUnsatisfied = false;

    // Fields from CustomerFollow
    public Transform player;
    private NavMeshAgent agent;
    private bool isFollowing = false;
    private Transform stopPosition;

    // Fields from CustomerBehavior
    public Transform entranceTarget;
    public Transform sitPosition;
    private bool isWaiting = false;
    public Material normalMaterial;
    public Material highlightMaterial;
    private Renderer customerRenderer;
    private bool hasArrivedAtEntrance = false;
    private bool isServed = false;
    


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MoveToEntrance();
        customerRenderer = GetComponent<Renderer>();
        customerRenderer.material = highlightMaterial;
        


    }

    private void Update()
    {
        if (isFollowing)
        {
            agent.SetDestination(player.position);
        }

        if (!agent.pathPending && agent.remainingDistance <= 0.1f)
        {
            if (!hasArrivedAtEntrance && entranceTarget && agent.destination == entranceTarget.position)
            {
                hasArrivedAtEntrance = true;
            }

            if (!isWaiting && hasArrivedAtEntrance)
            {
                isWaiting = true;
            }
        }

        // Check for unsatisfied customers
        if (isWaiting && !isFollowing && !isServed)
        {
            waitTime += Time.deltaTime;

            if (waitTime > maxWaitTime && !alreadyUnsatisfied)
            {
                GameManager.instance.CustomerBecameUnsatisfied();
                alreadyUnsatisfied = true;
                Debug.Log("Customer is about to become unsatisfied!");
            }
        }


    }

    public void ToggleFollowingStatus()
    {
        isFollowing = !isFollowing;

        if (isFollowing)
        {
            waitTime = 0;
            alreadyUnsatisfied = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Table"))
        {
            Debug.Log("Customer arrived at the table.");
            isFollowing = false;
            isWaiting = false;
            waitTime = 0;
            alreadyUnsatisfied = false;
            isServed = true;
            GameManager.instance.CustomerSatisfied(); // Call this method here
            stopPosition = other.transform;
            agent.SetDestination(stopPosition.position);
            Debug.Log("Customer reached the table.");
            
        }

        if (other.CompareTag("Player"))
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

    public void SitAtTable(Transform tablePosition)
    {
        Debug.Log("Moving to: " + tablePosition.position);
        agent.SetDestination(tablePosition.position);
    }
}
