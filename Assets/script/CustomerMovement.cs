using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform entranceTarget; // Set this in the inspector to the entrance position
    public float wanderRadius = 10.0f; // Radius around the spawn point where the customer may wander
    public float chanceToVisit = 0.05f; // 5% chance for a wandering customer to decide to visit.

    private bool isWandering = false; // To check if a customer is wandering

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Decide if the customer should wander or move directly to the entrance
        float chance = Random.Range(0f, 1f); // Generate a random number between 0 and 1
        if (chance < 0.1f) // 10% chance
        {
            MoveToEntrance();
        }
        else
        {
            StartWandering();
        }
    }

    void MoveToEntrance()
    {
        if (entranceTarget != null)
        {
            agent.SetDestination(entranceTarget.position);
            isWandering = false; // Ensure that the wandering state is turned off.
        }
        else
        {
            Debug.LogError("Entrance target not set for customer!");
        }
    }

    void StartWandering()
    {
        isWandering = true;
        StartCoroutine(Wander());
    }

    IEnumerator Wander()
    {
        while (isWandering)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            yield return new WaitForSeconds(5); // Wait for 5 seconds

            // Introduce the chance for the customer to decide to visit the restaurant
            if (Random.Range(0f, 1f) < chanceToVisit)
            {
                MoveToEntrance();
                isWandering = false; // This will break out of the while loop.
            }
        }
    }

    Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    float waitTime = 0f;
    const float maxWaitTime = 60f; // Time in seconds

    void Update()
    {
        if (!isWandering && !agent.pathPending && agent.remainingDistance <= 1f)
        {
            // The customer reached the destination and is waiting
            waitTime += Time.deltaTime;
            if (waitTime > maxWaitTime)
            {
                LeaveRestaurant();
            }
        }
    }

    void LeaveRestaurant()
    {
        Debug.Log("I've waited too long! I'm leaving.");
        Destroy(this.gameObject); // Destroy the customer game object
    }
}
