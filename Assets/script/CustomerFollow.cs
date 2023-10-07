using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerFollow : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public float followDistance = 2.0f;  // Distance to maintain while following
    public float sitDistance = 1.0f;  // Distance to check if the customer should sit
    private Transform tablePosition;  // This is where the customer will sit. Set in inspector or dynamically

    private bool isFollowing = false;

    void Update()
    {
        if (isFollowing)
        {
            FollowPlayer();
        }

        // If the player brings the customer close to a table
        if (Vector3.Distance(transform.position, tablePosition.position) < sitDistance)
        {
            SitDown();
        }
    }

    public void ToggleFollowingStatus()
    {
        isFollowing = !isFollowing;
        if (!isFollowing)
        {
            // Stop customer and wait for further interaction
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    void FollowPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) > followDistance)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            transform.position += directionToPlayer * Time.deltaTime;
            transform.LookAt(player);  // Make the customer face the player while following
        }
    }

    public void SetTablePosition(Transform tablePosition)
    {
        this.tablePosition = tablePosition;
    }

    void SitDown()
    {
        isFollowing = false;
        transform.position = tablePosition.position;  // This would be the chair position in front of the table
        transform.LookAt(tablePosition.position + Vector3.forward);  // Make customer face forward
        // Further enhancements: Play a sitting animation if you have one
    }

   
}
